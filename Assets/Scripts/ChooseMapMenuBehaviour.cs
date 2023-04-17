using Assets.Scripts.Constants;
using UnityEngine;

public class ChooseMapMenuBehaviour : MonoBehaviour
{
    public string ChoosenMapName { get; private set; }

    public void ChooseNature()
    {
        ChoosenMapName = SceneNames.Nature;
    }

    public void ChooseCity()
    {
        ChoosenMapName= SceneNames.City;
    }
}
