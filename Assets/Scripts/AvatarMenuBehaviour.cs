using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AvatarMenuBehaviour : MonoBehaviour
{
    private LinkedList<GameObject> _characters;
    private LinkedListNode<GameObject> _currentCharacter;

    [SerializeField] GameObject SelectedCharacter;
    [SerializeField] GameObject SceneCharacter;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] SettingsManager SettingsManager;
    [SerializeField] MainMenuSceneManagerBehaviour MainMenuSceneManagerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        var playerCharacterName = SettingsManager.PlayerSettings.Character;

        SelectedCharacter = MainMenuSceneManagerBehaviour.GetCharacter(playerCharacterName);
        SceneCharacter = MainMenuSceneManagerBehaviour.PlayerAvatar;

        _characters = new LinkedList<GameObject>(MainMenuSceneManagerBehaviour.Characters);
        _currentCharacter = _characters.Find(SelectedCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextButtonPressed()
    {
        Destroy(SceneCharacter);

        if (_currentCharacter.Next != null)
        {
            _currentCharacter = _currentCharacter.Next;
        }
        else
        {
            _currentCharacter = _characters.First;
        }

        SelectedCharacter = _currentCharacter.Value;
        SceneCharacter = MainMenuSceneManagerBehaviour.InstatiateCharacter(SelectedCharacter.name, SpawnPoint);
    }

    public void OnPreviousButtonPressed()
    {
        Destroy(SceneCharacter);

        if (_currentCharacter.Previous != null)
        {
            _currentCharacter = _currentCharacter.Previous;
        }
        else
        {
            _currentCharacter = _characters.Last;
        }

        SelectedCharacter = _currentCharacter.Value;
        SceneCharacter = MainMenuSceneManagerBehaviour.InstatiateCharacter(SelectedCharacter.name, SpawnPoint);
    }

    public void Save()
    {
        SettingsManager.SavePlayerCharacter(SelectedCharacter.name);
    }
}
