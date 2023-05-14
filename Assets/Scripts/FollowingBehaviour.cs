using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject FollowingObject;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = FollowingObject.transform.position;
    }
}
