using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObstacle : MonoBehaviour
{
    public int damage = 1; // Damage, 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage(damage); 
        }
    }
}
