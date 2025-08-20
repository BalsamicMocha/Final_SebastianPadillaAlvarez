using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int maxHealth = 100;   // Vida m�xima
    private int currentHealth;    // Vida actual

    [Header("Death Settings")]
    public GameObject deathEffect;   // Prefab de part�culas (explosi�n, humo, etc.)
    public AudioClip deathSound;     // Sonido de muerte
    private AudioSource audioSource;

    void Awake()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo p�blico para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Qu� pasa cuando el enemigo muere
    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        
        Destroy(gameObject);  // Destruye al enemigo
    }
}

