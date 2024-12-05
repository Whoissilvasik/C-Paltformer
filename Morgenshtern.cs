using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMover : MonoBehaviour
{
    [SerializeField] private float radius = 5f; 
    [SerializeField] private float speed = 1f; 
    [SerializeField] private int damageAmount = 1; 
    [SerializeField] private float knockbackForce = 5f; 

    private float angle; 
    private Vector3 initialPosition; 

    private void Start()
    {
      
        initialPosition = transform.position;
    }

    private void Update()
    {
        MoveInCircle();
    }

    private void MoveInCircle()
    {
        angle += speed * Time.deltaTime; 
        float x = Mathf.Cos(angle) * radius; 
        float y = Mathf.Sin(angle) * radius;

  
        transform.position = initialPosition + new Vector3(x, y, 0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            hero.GetDamage(damageAmount); 
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized; 
            hero.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); 
        }
    }
}
