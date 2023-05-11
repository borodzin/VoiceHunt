using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoiceManager : MonoBehaviourPunCallbacks
{
    [SerializeField] float VoiceRange = 2f;
    [SerializeField] float MinVolume = 0.1f;

    public GameObject LocalPlayer { get; set; }

    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            var viewId = (int)player.CustomProperties["ViewID"];
            var playerObject = PhotonView.Find(viewId).gameObject;

            var distance = GetDistanceBetweenPlayers(LocalPlayer, playerObject);
            var audioSource = playerObject.GetComponentInChildren<AudioSource>();
            //Debug.Log(distance);

            if (distance > VoiceRange)
            {
                audioSource.volume = 0f;
            }
            else
            {
                var volume = Mathf.Lerp(MinVolume, 1f, 1 - (distance / VoiceRange));
                audioSource.volume = volume;
            }

            var characterUIBehaviour = playerObject.GetComponentInChildren<CharacterUIBehaviour>();
            var isSpeaking = (bool)player.CustomProperties["IsSpeaking"];
            characterUIBehaviour.ToggleSpeakingIcon(isSpeaking);
        }
    }

    private float GetDistanceBetweenPlayers(GameObject playerOne, GameObject playerTwo)
    {
        return Vector3.Distance(playerOne.transform.position, playerTwo.transform.position);
    }
}