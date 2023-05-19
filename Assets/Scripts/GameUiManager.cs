using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PhoneMenu;

    [SerializeField] GameObject ShapeButton;
    [SerializeField] GameObject UnshapeButton;

    [SerializeField] GameObject PunchButton;

    // Start is called before the first frame update
    void Start()
    {
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

    public void EnableGameUi()
    {
        if (IsPhonePlatfrom())
        {
            PhoneMenu.SetActive(true);
        }
        else
        {
            PhoneMenu.SetActive(false);
        }
    }

    public void EnablePreyUi()
    {
        ShapeButton.SetActive(true);
        UnshapeButton.SetActive(true);
    }

    public void EnableHunterUi()
    {
        PunchButton.SetActive(true);
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
