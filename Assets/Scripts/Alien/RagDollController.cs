using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Animator animator;
    
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();  
        animator = GetComponent<Animator>();
        RagDollOff();
    }

    public void RagDollOff() {
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
    }

    public void RagDollOn() {
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
    } 

    public void AddForce(Vector3 force) {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();   
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
