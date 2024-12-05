using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int healAmount = 1; // Amount of health to restore

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hero hero = other.GetComponent<Hero>();
        if (hero != null)
        {
            Debug.Log("Hero collected a fruit!");
            hero.Heal(healAmount); // Restore health
            Destroy(gameObject); // Destroy the pickup after collection
        }
    }
}
