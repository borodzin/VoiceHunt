using Assets.Scripts.Constants;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableObjectBehaviour : MonoBehaviourPunCallbacks
{
    private void OnCollisionEnter(Collision collision)
    {
        HandlePunch(collision);
    }

    private void HandlePunch(Collision collision)
    {
        if (collision.collider.gameObject.tag != "Fist")
        {
            return;
        }

        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { NetworkPropertiesKeys.IsKicking, true }
        };

        collision.gameObject.GetPhotonView().Owner.SetCustomProperties(customProperties);
    }
}
