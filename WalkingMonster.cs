using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    private float speed = 0.5f;
    private Vector3 direction;
    private SpriteRenderer sprite;

    [SerializeField] private int health = 2; 
    [SerializeField] private int damage = 1; 

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); 
    }

    private void Start() 
    {
        direction = transform.right; 
    }

    private void Move() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * direction.x * 0.7f, 0.1f);

        if (colliders.Length > 0) direction *= -1f; 
        transform.Translate(direction * speed * Time.deltaTime);
        sprite.flipX = direction.x > 0.0f; 
    }

    private void Update() //
    {
        Move(); 
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject == Hero.Instance.gameObject) 
        {
            Hero.Instance.GetDamage(damage); 
        }
    }

    public void TakeDamage(int damage) 
    {
        health -= damage; 
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() 
    {
        Destroy(gameObject); // Naikiname objektą
    }
}
