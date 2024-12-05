using System.Collections;
using UnityEngine;

public class FlyingMonster : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 3;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform player;

    private float attackTimer = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();

        if (IsPlayerInRange() && attackTimer <= 0f)
        {
            Attack();
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
        }
    }

    private bool IsPlayerInRange()
    {
        
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    private void Attack()
    {
     
        if (player.TryGetComponent(out IDamageable damageable))
        {
            damageable.GetDamage(1);
            Debug.Log("Flying monster attacked the player!");
            attackTimer = attackCooldown; 
        }
    }

    public void GetDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Flying monster health: {health}");
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Flying monster has died!");
        Destroy(gameObject); // Уничтожаем объект
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"Restored {amount} health to the flying monster.");
    }
}
