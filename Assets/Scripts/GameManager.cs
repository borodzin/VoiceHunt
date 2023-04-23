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
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject VoiceManager;
    [SerializeField] PlayerSettings PlayerSettings;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] PauseMenuBehavior PauseMenu;
    [SerializeField] TurnskinBehaviour TurnskinBehaviour;

    // Start is called before the first frame update
    void Start()
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
        playerMovement.TurnskinBehaviour = TurnskinBehaviour;

        PauseMenu.CharacterInput = playerMovement;

        cameraController.CharacterInput = playerMovement;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;
    }
}