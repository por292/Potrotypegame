using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
//using UnityEngine.Debug;
//using System.Diagnostics.Debug;

public class Target : MonoBehaviour
{

    public float MaxHealth = 20f;
    private float CurrentHealth;
    //[SerializeField] public AudioClip damageSoundC;
    //
    //  [SerializeField] public AudioClip deathSoundC;

    public bool HasTakenDamage { get; set; }
    public float health = 50f;

    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
  
        Destroy(gameObject);
    }
    private void Update()
    {
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}
