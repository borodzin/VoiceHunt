using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterMovementBehaviour : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 motion)
    {
        _animator.SetBool("IsWalking", motion.magnitude > 0);
        transform.Translate(motion.normalized * Time.deltaTime, Space.World);

        if (motion.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(motion, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8 * Time.deltaTime);
        }
    }
}
