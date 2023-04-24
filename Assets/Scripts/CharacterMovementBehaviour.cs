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

    public void Move(Vector3 motion, bool isSprinting)
    {
        var isWalking = motion.magnitude > 0;
        _animator.SetBool("IsWalking", motion.magnitude > 0);
        _animator.SetBool("IsSprinting", isSprinting && isWalking);

        var speed = isSprinting ? 4 : 2;
        transform.Translate(motion.normalized * Time.deltaTime * speed, Space.World);

        if (motion.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(motion, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8 * Time.deltaTime);
        }
    }

    public void Punch()
    {
        _animator.SetBool("IsPunching", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleFistCollision(collision);
    }

    private void HandleFistCollision(Collision collision)
    {
        if (collision.gameObject.tag != "Fist")
        {
            return;
        }

        Debug.Log("Knocked!");
    }
}
