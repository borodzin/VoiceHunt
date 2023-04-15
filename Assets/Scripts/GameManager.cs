using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject VoiceManager;
    [SerializeField] PlayerSettings PlayerSettings;

    // Start is called before the first frame update
    void Start()
    {
        var characterPrefabName = PlayerSettings.Character.name;
        var player = PhotonNetwork.Instantiate(characterPrefabName, new Vector3(0f, 0f, 0f), Quaternion.identity);
        var photonView = player.GetComponent<PhotonView>();
        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { "ViewID", photonView.ViewID }
        };
        PhotonNetwork.SetPlayerCustomProperties(customProperties);

        var cameraController = Camera.GetComponent<CameraController>();
        cameraController.target = player.transform;

        var playerMovement = player.GetComponent<CharacterInputBehaviour>();
        playerMovement.Camera = Camera;

        var voiceManagerComponent = VoiceManager.GetComponent<VoiceManager>();
        voiceManagerComponent.LocalPlayer = player;
    }
}
