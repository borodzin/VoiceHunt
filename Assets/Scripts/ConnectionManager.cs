using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Instantiate(cube, new Vector3(0, 2, 0), Quaternion.identity);
        //var roomOptions = new RoomOptions
        //{
        //    IsOpen = true,
        //    IsVisible = true,
        //    MaxPlayers = 3,
        //};

        //PhotonNetwork.CreateRoom("gafh", roomOptions);
        PhotonNetwork.JoinRoom("gafh");
    }

    public override void OnJoinedRoom()
    {
        Instantiate(cube, new Vector3(0, -2, 0), Quaternion.identity);
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
