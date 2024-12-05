using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity
{
    [SerializeField] private int lives = 3;
    [SerializeField] private int damage = 1; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject == Hero.Instance.gameObject)
        {
           
            ContactPoint2D contact = collision.contacts[0];
            Vector2 hitPoint = contact.point; 

          
            if (IsTopCollision(hitPoint))
            {
              
                TakeDamageFromHero();
            }
            else if (IsSideCollision(hitPoint))
            {
               
                Hero.Instance.GetDamage(damage);
                lives--;
                Debug.Log("Worm " + lives);
            }
        }

        
        if (lives < 1)
        {
            Die();
        }
    }

    private bool IsTopCollision(Vector2 hitPoint)
    {
       
        Vector2 wormPosition = transform.position;

        
        return hitPoint.y > wormPosition.y + 0.1f; 
    }

    private bool IsSideCollision(Vector2 hitPoint)
    {
       
        Vector2 wormPosition = transform.position;

       
        return hitPoint.y <= wormPosition.y + 0.1f; 
    }

    private void TakeDamageFromHero()
    {
      
        lives--;
        Debug.Log("Worm hit! Lives left: " + lives);
    }
}
