using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuSceneManagerBehaviour : MonoBehaviour
{
    private Dictionary<GameObject, GameObject> _gameCharactersAndMenuAvatars;

    [SerializeField]
    public List<GameObject> GameCharacters;

    [SerializeField]
    public List<GameObject> MenuAvatars;

    [SerializeField]
    public SettingsManager SettingsManager;

    [SerializeField]
    public GameObject PlayerAvatar;

    [SerializeField]
    public GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (GameCharacters.Count() != MenuAvatars.Count())
        {
            throw new ArgumentException("Counts of game characters and menu avatars should be equal.");
        }

        _gameCharactersAndMenuAvatars = new Dictionary<GameObject, GameObject>();

        for (var index = 0; index < GameCharacters.Count(); index++)
        {
            _gameCharactersAndMenuAvatars.Add(GameCharacters[index], MenuAvatars[index]);
        }

        var playerCharacter = SettingsManager.PlayerSettings.Character;

        PlayerAvatar = InstatiateMenuAvatar(playerCharacter, SpawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject InstatiateMenuAvatar(GameObject gameCharacter, GameObject spawnPoint)
    {
        var avatarToInstatiate = _gameCharactersAndMenuAvatars[gameCharacter];
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;

        return Instantiate(avatarToInstatiate, position, rotation);
    }
}
