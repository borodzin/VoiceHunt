using Photon.Pun;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using static UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

[RequireComponent(typeof(Recorder))]
public class CharacterChattingBehaviour : MonoBehaviourPunCallbacks
{
    private Recorder _recorder;

    // Start is called before the first frame update
    void Start()
    {
        _recorder = GetComponent<Recorder>();

        var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "IsSpeaking", false }
            };

        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    // Update is called once per frame
    void Update()
    {

        if (CrossPlatformInputManager.GetButtonDown("Speak"))
        {
            Debug.Log("I'm speaking");

            var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "IsSpeaking", true }
            };
            PhotonNetwork.SetPlayerCustomProperties(customProperties);

            _recorder.TransmitEnabled = true;
        }

        if (CrossPlatformInputManager.GetButtonUp("Speak"))
        {
            Debug.Log("I'm quiet");

            var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "IsSpeaking", false }
            };
            PhotonNetwork.SetPlayerCustomProperties(customProperties);

            _recorder.TransmitEnabled = false;
        }
    }
}
