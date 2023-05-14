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

    [SerializeField] GameObject Camera;
    [SerializeField] GameObject VoiceManager;
    [SerializeField] PlayerSettings PlayerSettings;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] PauseMenuBehavior PauseMenu;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject GhostPrefab;

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
        playerMovement.PlayerPrefabName = characterPrefabName;
        playerMovement.IsHunter = true;

        PauseMenu.CharacterInput = playerMovement;

        cameraController.CharacterInput = playerMovement;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;
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
        playerMovement.PlayerPrefabName = characterPrefabName;
        playerMovement.TurnskinBehaviour = turnskinBehaviour;

        PauseMenu.CharacterInput = playerMovement;

        cameraController.CharacterInput = playerMovement;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;

        var preyUiBehaviuor = GameUI.GetComponent<PreyUiBehaviour>();
        preyUiBehaviuor.CreateHearts(3);
        preyBehaviour.PreyUiBehaviour = preyUiBehaviuor;
        preyUiBehaviuor.HeartsAreEnded += preyBehaviour.Die;
        preyUiBehaviuor.HeartsAreEnded += PreyHasBeenDied;

        _player = player;
    }

    private void PreyHasBeenDied()
    {
        Destroy(_player.GetComponent<PreyBehaviour>());
        Destroy(Camera.GetComponent<TurnskinBehaviour>());
        _player.GetComponent<CharacterInputBehaviour>().IsDied = true;
    }
}