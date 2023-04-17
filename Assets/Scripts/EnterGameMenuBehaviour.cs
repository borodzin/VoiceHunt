using TMPro;
using UnityEngine;

public class EnterGameMenuBehaviour : MonoBehaviour
{
    [SerializeField] TMP_InputField GameNameInput;

    public string GameName => GameNameInput.text;
}
