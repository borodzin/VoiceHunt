using Assets.Scripts.Constants;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterNetworkBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField]
    ShapeContainer ShapeContainer;

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        var viewId = (int)targetPlayer.CustomProperties[NetworkPropertiesKeys.ViewId];
        var playerObject = PhotonView.Find(viewId).gameObject;

        HandleChangingShape(targetPlayer, playerObject, changedProps);
        HandlePunch(targetPlayer, playerObject, changedProps);
        HandleKnock(targetPlayer, playerObject, changedProps);
    }

    private void HandleChangingShape(Player targetPlayer, GameObject targetPlayerObject, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.CustomProperties.ContainsKey(NetworkPropertiesKeys.PlayerShapePrefabName))
        {
            return;
        }

        var playerPrefabName = (string)targetPlayer.CustomProperties[NetworkPropertiesKeys.PlayerNativeShapePrefabName];
        var playerNativeShape = targetPlayerObject.transform.Find(playerPrefabName).gameObject;
        var playerCurrentShape = targetPlayerObject.transform.Find("Current Shape")?.gameObject;
        var targetShapePrefabName = (string)targetPlayer.CustomProperties[NetworkPropertiesKeys.PlayerShapePrefabName];

        if (playerCurrentShape != null)
        {
            Destroy(playerCurrentShape);
        }

        if (targetShapePrefabName == playerNativeShape.name)
        {
            playerNativeShape.SetActive(true);
            return;
        }

        var shape = ShapeContainer.Shapes[targetShapePrefabName];
        var currentShape = Instantiate(shape, targetPlayerObject.transform);
        currentShape.name = "Current Shape";

        var shapingSmokes = targetPlayerObject.transform.Find("ShapingSmokes").gameObject.GetComponent<ParticleSystem>();
        shapingSmokes.Play();

        if (playerNativeShape.activeSelf)
        {
            playerNativeShape.SetActive(false);
        }
    }

    private void HandlePunch(Player targetPlayer, GameObject targetPlayerObject, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.CustomProperties.ContainsKey(NetworkPropertiesKeys.IsKicking))
        {
            return;
        }

        var isKicking = (bool)targetPlayer.CustomProperties[NetworkPropertiesKeys.IsKicking];

        if (!isKicking)
        {
            return;
        }

        var punchEffectObject = targetPlayerObject.GetComponentInChildren<Mark>();
        var punchEffect = punchEffectObject.gameObject.GetComponent<ParticleSystem>();

        punchEffect.Play();

        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { NetworkPropertiesKeys.IsKicking, false }
        };

        targetPlayer.SetCustomProperties(customProperties);
    }

    private void HandleKnock(Player targetPlayer, GameObject targetPlayerObject, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!targetPlayer.CustomProperties.ContainsKey(NetworkPropertiesKeys.IsKnocked))
        {
            return;
        }

        var isKnocked = (bool)targetPlayer.CustomProperties[NetworkPropertiesKeys.IsKnocked];

        if (!isKnocked)
        {
            return;
        }

        var knockEffect = targetPlayerObject.transform.Find("KnockSmokes").gameObject.GetComponent<ParticleSystem>();
        knockEffect.Play();

        var customProperties = new ExitGames.Client.Photon.Hashtable
        {
            { NetworkPropertiesKeys.IsKnocked, false }
        };

        targetPlayer.SetCustomProperties(customProperties);
    }

    private void HandlePreyDie(Player targetPlayer, GameObject targetPlayerObject, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }
}
