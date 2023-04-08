using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        var player = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity);

        var cameraController = Camera.GetComponent<CameraController>();
        cameraController.target = player.transform;

        var playerMovement = player.GetComponent<CharacterMovement>();
        playerMovement.Camera = Camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
