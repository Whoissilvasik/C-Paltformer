using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination; 

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hero")) 
        {
            TeleportHero(collider.gameObject); 
        }
    }

    private void TeleportHero(GameObject hero)
    {
        hero.transform.position = teleportDestination.position; 
        Debug.Log("Hero has been teleported to: " + teleportDestination.position);
    }
}
