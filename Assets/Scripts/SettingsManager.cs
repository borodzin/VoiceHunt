using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    public PlayerSettings PlayerSettings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerCharacter(GameObject character)
    {
        PlayerSettings.Character = character;
    }
}
