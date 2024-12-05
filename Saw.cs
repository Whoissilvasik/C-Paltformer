using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField] private float speed = 2f; 
    [SerializeField] private float travelDistance = 5f; // max distance
    [SerializeField] private int damage = 1; 

    private Vector3 startPosition; 
    private bool movingRight = true; //where it comes

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Move(); 
    }

    private void Move()
    {
      
        float distance = Mathf.Abs(transform.position.x - startPosition.x);

       
        if (distance >= travelDistance)
        {
            movingRight = !movingRight; 
          
            startPosition.x = transform.position.x;
        }

      
        float direction = movingRight ? 1 : -1;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Hero hero = collision.collider.GetComponent<Hero>();
        if (hero != null)
        {
            hero.GetDamage(damage);
        }
    }
}
