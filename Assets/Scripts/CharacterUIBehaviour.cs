using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject SpeakingIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSpeakingIcon(bool isSpeaking)
    {
        SpeakingIcon.SetActive(isSpeaking);
    }
}
