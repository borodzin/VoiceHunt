using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PreyUiBehaviour : MonoBehaviour
{
    private List<GameObject> _hearts;
    private Canvas _thisCanvas;

    [SerializeField]
    GameObject Heart;

    public event Action HeartsAreEnded;

    void Start()
    {
        _thisCanvas = GetComponent<Canvas>();
        _hearts = new List<GameObject>();
    }

    public void CreateHearts(int count, float startHeartPosX, float startHeartPosY, float posXOffset)
    {
        for (int index = 0, posX = (int)startHeartPosX; index < count; index++, posX -= (int)posXOffset)
        {
            var heartObject = Instantiate(Heart, _thisCanvas.transform);
            heartObject.transform.localPosition = new Vector3(posX, startHeartPosY, 0);

            _hearts.Add(heartObject);
        }
    }

    public void RemoveHeart()
    {
        var lastHeart = _hearts.LastOrDefault();
        _hearts.Remove(lastHeart);
        Destroy(lastHeart);

        if (_hearts.Count == 0)
        {
            HeartsAreEnded.Invoke();
        }
    }
}
