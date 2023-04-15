using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Voice Hunt Assets/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public GameObject Character;
}
