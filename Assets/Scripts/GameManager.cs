using Assets.Scripts.Constants;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviourPunCallbacks
{
    private GameObject _player;

    [SerializeField] CharacterNetworkBehaviour CharacterNetworkBehaviour;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject VoiceManager;
    [SerializeField] PlayerSettings PlayerSettings;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] PauseMenuBehavior PauseMenu;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject GhostPrefab;
    [SerializeField] GameUiManager GameUiManager;

    [SerializeField] GameObject TimerPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartAsHunter()
    {
        var characterPrefabName = PlayerSettings.Character;
        var spawnPoint = SpawnPoint.transform.position;
        var player = PhotonNetwork.Instantiate(characterPrefabName, spawnPoint, Quaternion.identity);
        var photonView = player.GetComponent<PhotonView>();
        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { NetworkPropertiesKeys.ViewId, photonView.ViewID },
            { NetworkPropertiesKeys.PlayerNativeShapePrefabName, characterPrefabName },
        };
        PhotonNetwork.SetPlayerCustomProperties(customProperties);

        var cameraController = Camera.GetComponent<CameraController>();
        cameraController.target = player.transform;

        var playerMovement = player.GetComponent<CharacterInputBehaviour>();
        playerMovement.Camera = Camera;
        playerMovement.IsHunter = true;

        PauseMenu.CharacterInput = playerMovement;

        cameraController.CharacterInput = playerMovement;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;

        GameUiManager.EnableGameUi();
        GameUiManager.EnableHunterUi();
    }

    public void StartAsHider()
    {
        var characterPrefabName = PlayerSettings.Character;
        var spawnPoint = SpawnPoint.transform.position;
        var player = PhotonNetwork.Instantiate(characterPrefabName, spawnPoint, Quaternion.identity);
        var photonView = player.GetComponent<PhotonView>();
        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { NetworkPropertiesKeys.ViewId, photonView.ViewID },
            { NetworkPropertiesKeys.PlayerNativeShapePrefabName, characterPrefabName },
        };
        PhotonNetwork.SetPlayerCustomProperties(customProperties);

        var turnskinBehaviour = Camera.AddComponent<TurnskinBehaviour>();
        var preyBehaviour = player.AddComponent<PreyBehaviour>();
        preyBehaviour.GhostPrefab = GhostPrefab;

        var cameraController = Camera.GetComponent<CameraController>();
        cameraController.target = player.transform;

        var playerMovement = player.GetComponent<CharacterInputBehaviour>();
        playerMovement.Camera = Camera;

        PauseMenu.CharacterInput = playerMovement;

        cameraController.CharacterInput = playerMovement;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;

        var preyUiBehaviuor = GameUI.GetComponent<PreyUiBehaviour>();
        preyUiBehaviuor.CreateHearts(3, 740, 400, 160);
        preyBehaviour.PreyUiBehaviour = preyUiBehaviuor;
        preyBehaviour.TurnskinBehaviour = turnskinBehaviour;
        preyBehaviour.PlayerNativePrefabName = characterPrefabName;
        playerMovement.ChangeShape += preyBehaviour.ChangeShape;
        playerMovement.ReturnShape += preyBehaviour.ReturnShape;
        preyUiBehaviuor.HeartsAreEnded += preyBehaviour.Die;
        preyUiBehaviuor.HeartsAreEnded += PreyHasBeenDied;

        _player = player;

        var timer = Instantiate(TimerPrefab, GameUI.GetComponent<Canvas>().transform);
        timer.transform.localPosition = new Vector3(720, 0, 0);
        var timerBehaviour = timer.GetComponent<TimerBehaviour>();

        CharacterNetworkBehaviour.ShapeChanged += timerBehaviour.StartTimer;
        CharacterNetworkBehaviour.ShapeChanged += timerBehaviour.RestartTimer;
        playerMovement.ReturnShape += timerBehaviour.EndTimer;
        timerBehaviour.TimerTick += preyBehaviour.ReturnShape;

        var playerChattingBehaviour = player.GetComponent<CharacterChattingBehaviour>();
        playerChattingBehaviour.Speak += () => timerBehaviour.ChangeTimerDirection(false);
        playerChattingBehaviour.Quite += () => timerBehaviour.ChangeTimerDirection(true);
        preyUiBehaviuor.HeartsAreEnded += timerBehaviour.EndTimer;

        GameUiManager.EnableGameUi();
        GameUiManager.EnablePreyUi();
    }

    private void PreyHasBeenDied()
    {
        Destroy(_player.GetComponent<PreyBehaviour>());
        Destroy(Camera.GetComponent<TurnskinBehaviour>());
        _player.GetComponent<CharacterInputBehaviour>().IsDied = true;
    }
}