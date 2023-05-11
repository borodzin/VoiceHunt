using Assets.Scripts.Constants;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementBehaviour))]
[RequireComponent(typeof(CharacterInputBehaviour))]
public class PreyBehaviour : MonoBehaviour
{
    private CharacterMovementBehaviour _characterBehavior;
    private CharacterInputBehaviour _characterInputBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        _characterBehavior = GetComponent<CharacterMovementBehaviour>();
        _characterInputBehaviour = GetComponent<CharacterInputBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.collider.gameObject.tag;

        if (tag == "Fist")
        {
            _characterBehavior.Knock();

            var customProperties = new ExitGames.Client.Photon.Hashtable
            {
                { NetworkPropertiesKeys.PlayerShapePrefabName, _characterInputBehaviour.PlayerPrefabName }
            };

            PhotonNetwork.SetPlayerCustomProperties(customProperties);
        }
    }
}
