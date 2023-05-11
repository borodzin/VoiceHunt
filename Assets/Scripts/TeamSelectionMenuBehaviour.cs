using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectionMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    GameManager GameManager;

    public void OnHuntTeamButtonClicked()
    {
        GameManager.StartAsHunter();
        this.gameObject.SetActive(false);
    }

    public void OnHideTeamButtonClicked()
    {
        GameManager.StartAsHider();
        this.gameObject.SetActive(false);
    }
}
