using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject AvatarMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClicked()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void OnAvatarButtonClicked()
    {
        MainMenu.SetActive(false);
        AvatarMenu.SetActive(true);
    }

    public void OnAvatarBackButtonClicked()
    {
        MainMenu.SetActive(true);
        AvatarMenu.SetActive(false);
    }

    public void OnCreateGameButtonClicked()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            IsOpen = true,
        };

        PhotonNetwork.CreateRoom("gafh", roomOptions);
    }

    public void OnJoinGameButtonClicked()
    {
        PhotonNetwork.JoinRoom("gafh");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
