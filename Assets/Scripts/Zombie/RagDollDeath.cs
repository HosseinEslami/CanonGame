using System;
using UnityEngine;

public class RagDollDeath : MonoBehaviour
{ 
    private Animator _animator;
    private Rigidbody[] _ragDollRigidbodies;
    private Collider[] _ragDollColliders;

    private void OnEnable()
    {
        if(_animator) ToggleRagDoll(false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _ragDollColliders = GetComponentsInChildren<Collider>();
        _ragDollRigidbodies = GetComponentsInChildren<Rigidbody>();
        
        ToggleRagDoll(false);
    }

    public void Die(float force, Vector3 explosionPos, float range)
    {
        ToggleRagDoll(true);

        foreach (var rb in _ragDollRigidbodies)
        {
            rb.AddExplosionForce(force, explosionPos, range, 0f, ForceMode.Impulse);
        }
    }

    public void ToggleRagDoll(bool isDead, bool activeRigid = true)
    {
        if(activeRigid)
        {
            _animator.enabled = !isDead;
            
            foreach (var rb in _ragDollRigidbodies)
            {
                rb.isKinematic = !isDead;
            }
        }

        foreach (var col in _ragDollColliders)
        {
            if(!col.gameObject.CompareTag("Exploder"))
                col.enabled = isDead;
        }
    }
}
