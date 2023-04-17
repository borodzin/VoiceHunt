using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using UnityEditor;
using UnityEngine;
using WebSocketSharp;

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

    public void SavePlayerCharacter(string characterName)
    {
        PlayerSettings.Character = characterName;
    }
}
