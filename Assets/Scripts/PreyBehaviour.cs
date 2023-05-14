using Assets.Scripts.Constants;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementBehaviour))]
[RequireComponent(typeof(CharacterInputBehaviour))]
[RequireComponent(typeof(Animator))]
public class PreyBehaviour : MonoBehaviour
{
    private CharacterMovementBehaviour _characterBehavior;
    private CharacterInputBehaviour _characterInputBehaviour;
    private Animator _animator;

    public GameObject GhostPrefab { get; set; }
    
    public PreyUiBehaviour PreyUiBehaviour { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _characterBehavior = GetComponent<CharacterMovementBehaviour>();
        _characterInputBehaviour = GetComponent<CharacterInputBehaviour>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        var selectedShape = GhostPrefab;

        if (selectedShape != null)
        {
            var customProperties = new ExitGames.Client.Photon.Hashtable
                {
                    { NetworkPropertiesKeys.PlayerShapePrefabName, selectedShape.name },
                    { NetworkPropertiesKeys.IsPreyDied, true }
                };

            PhotonNetwork.SetPlayerCustomProperties(customProperties);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.collider.gameObject.tag;
        var player = gameObject.GetPhotonView().Owner;

        if (_animator.GetBool("IsKnocking"))
        {
            return;
        }

        if (tag == "Fist")
        {
            _characterBehavior.Knock();

            var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { NetworkPropertiesKeys.PlayerShapePrefabName, _characterInputBehaviour.PlayerPrefabName },
                { NetworkPropertiesKeys.IsKnocked, true }
            };

            player.SetCustomProperties(customProperties);

            PreyUiBehaviour.RemoveHeart();
        }
    }
}
