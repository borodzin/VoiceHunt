using System.Collections.Generic;
using UnityEngine;

public class ShapeContainer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ShapesPrefabs;

    public Dictionary<string, GameObject> Shapes { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Shapes = new Dictionary<string, GameObject>();

        foreach (var shape in ShapesPrefabs)
        {
            Shapes.Add(shape.name, shape);
        }
    }
}
