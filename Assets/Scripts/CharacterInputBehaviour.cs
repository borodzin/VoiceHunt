using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterMovementBehaviour))]
public class CharacterInputBehaviour : MonoBehaviour
{
    private CharacterMovementBehaviour _characterBehavior;

    [SerializeField] PhotonView photonView;

    public GameObject Camera { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _characterBehavior = GetComponent<CharacterMovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 cameraForward = Camera.transform.forward;
        Vector3 cameraRight = Camera.transform.right;

        // Project the camera directions onto the xz plane
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        // Normalize the directions to get movement direction
        Vector3 motion = (cameraForward.normalized * v + cameraRight.normalized * h).normalized;
        _characterBehavior.Move(motion);
    }
}
