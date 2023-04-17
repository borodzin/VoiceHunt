using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PhoneMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (IsPhonePlatfrom())
        {
            PhoneMenu.SetActive(true);
        }
        else
        {
            PhoneMenu.SetActive(false);
        }

        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Pause"))
        {
            PauseMenu.SetActive(true);
            PhoneMenu.SetActive(false);
        }
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
