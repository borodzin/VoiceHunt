using Assets.Scripts.Constants;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterMovementBehaviour))]
public class CharacterInputBehaviour : MonoBehaviour
{
    private CharacterMovementBehaviour _characterBehavior;
    private ParticleSystem _shapingSmokes;

    [SerializeField] PhotonView photonView;

    public GameObject Camera { get; set; }

    public TurnskinBehaviour TurnskinBehaviour { get; set; }

    // to deprecate
    public string PlayerPrefabName { get; set; }

    public bool IsPaused { get; set; }

    public bool IsHunter { get; set; }

    public bool IsDied { get; set; }

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

        if (CrossPlatformInputManager.GetButtonDown("Pause"))
        {
            IsPaused = true;
        }

        if (IsPaused)
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Shape") && !IsHunter && !IsDied)
        {
            var selectedShape = TurnskinBehaviour.SelectedShape;

            if (selectedShape != null)
            {
                var customProperties = new ExitGames.Client.Photon.Hashtable
                {
                    { NetworkPropertiesKeys.PlayerShapePrefabName, selectedShape.name }
                };

                PhotonNetwork.SetPlayerCustomProperties(customProperties);
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Unshape") && !IsHunter && !IsDied)
        {
            var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { NetworkPropertiesKeys.PlayerShapePrefabName, PlayerPrefabName }
            };

            PhotonNetwork.SetPlayerCustomProperties(customProperties);
        }

        if (CrossPlatformInputManager.GetButtonDown("Punch") && IsHunter)
        {
            _characterBehavior.Punch();
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
        var isSprinting = CrossPlatformInputManager.GetButton("Sprint");
        _characterBehavior.Move(motion, isSprinting);
    }
}
