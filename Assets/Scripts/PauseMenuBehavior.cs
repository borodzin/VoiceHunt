using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject PhoneGui;

    public CharacterInputBehaviour CharacterInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseButtonClicked()
    {
    }

    public void OnResumeButtonClicked()
    {
        if (IsPhonePlatfrom())
        {
            PhoneGui.SetActive(true);
        }

        CharacterInput.IsPaused = false;
        this.gameObject.SetActive(false);

    }

    public void OnQuitButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Connecting");
    }

    private bool IsPhonePlatfrom()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.Android)
        {
            return true;
        }

        return false;
    }
}
