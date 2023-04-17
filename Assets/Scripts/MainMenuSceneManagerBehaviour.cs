using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuSceneManagerBehaviour : MonoBehaviour
{
    private Dictionary<string, GameObject> _characters;

    [SerializeField]
    public List<GameObject> Characters;

    [SerializeField]
    public SettingsManager SettingsManager;

    [SerializeField]
    public GameObject PlayerAvatar;

    [SerializeField]
    public GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _characters = new Dictionary<string, GameObject>();

        foreach (var character in Characters)
        {
            _characters.Add(character.name, character);
        }

        var playerCharacterName = SettingsManager.PlayerSettings.Character;

        PlayerAvatar = InstatiateCharacter(playerCharacterName, SpawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCharacter(string characterName)
    {
        return _characters[characterName];
    }

    public GameObject InstatiateCharacter(string characterName, GameObject spawnPoint)
    {
        var avatarToInstatiate = _characters[characterName];
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;

        return Instantiate(avatarToInstatiate, position, rotation);
    }
}
