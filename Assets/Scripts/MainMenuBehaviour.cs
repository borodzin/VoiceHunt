using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviourPunCallbacks
{
    private EnterGameMenuBehaviour _enterGameNameBehaviour;
    private ChooseMapMenuBehaviour _chooseMapMenuBehaviour;
    private JoinGameMenuBehaviour _joinGameMenuBehaviour;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject AvatarMenu;
    [SerializeField] GameObject CreateGameMenu;
    [SerializeField] GameObject ChooseMapMenu;
    [SerializeField] GameObject JoinGameMenu;

    // Start is called before the first frame update
    void Start()
    {
        _enterGameNameBehaviour = CreateGameMenu.GetComponent<EnterGameMenuBehaviour>();
        _chooseMapMenuBehaviour = ChooseMapMenu.GetComponent<ChooseMapMenuBehaviour>();
        _joinGameMenuBehaviour = JoinGameMenu.GetComponent<JoinGameMenuBehaviour>();
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

    public void OnMapButtonClicked()
    {
        ChooseMapMenu.SetActive(false);
        CreateGameMenu.SetActive(true);
    }

    public void OnGoCreateGameButtonClicked()
    {
        ChooseMapMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void OnGoJoinGameButtonClicked()
    {
        JoinGameMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void OnBackButtonInPlayMenuClicked()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
    }

    public void OnBackButtonInChooseMapMenuClicked()
    {
        ChooseMapMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void OnBackButtonInEnterGameMenuClicked()
    {
        CreateGameMenu.SetActive(false);
        ChooseMapMenu.SetActive(true);
    }

    public void OnBackButtonInJoinGameMenuClicked()
    {
        JoinGameMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void OnCreateGameButtonClicked()
    {
        var gameName = _enterGameNameBehaviour.GameName;
        var mapName = _chooseMapMenuBehaviour.ChoosenMapName;

        if (gameName.Length < 1)
        {
            return;
        }

        var roomOptions = new RoomOptions
        {
            IsOpen = true,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "SceneName", mapName },
            },
        };

        PhotonNetwork.CreateRoom(gameName, roomOptions);
    }

    public void OnJoinGameButtonClicked()
    {
        var gameName = _joinGameMenuBehaviour.GameName;

        PhotonNetwork.JoinRoom(gameName);
    }

    public override void OnJoinedRoom()
    {
        var sceneName = (string)PhotonNetwork.CurrentRoom.CustomProperties["SceneName"];
        PhotonNetwork.LoadLevel(sceneName);
    }
}
