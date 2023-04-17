using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinGameMenuBehaviour : MonoBehaviour
{
    [SerializeField] TMP_InputField GameNameInput;

    public string GameName => GameNameInput.text;
}
