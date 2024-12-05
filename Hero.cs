using System;
using UnityEngine;
using UnityEngine.UI;

// IComparable, IEquatable, IFormattable implementacijos
public interface IDamageable
{
    void GetDamage(int amount);
    void Heal(int amount);
}

// Hero klasė
public class Hero : MonoBehaviour, IComparable<Hero>, IEquatable<Hero>, IFormattable, IDamageable
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private Text healthText;

    private bool isGrounded = false;
    private bool isCrouching = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dashTimer = 0f;
    private Vector3 initialPosition;

    public static Hero Instance { get; private set; }

    private States State
    {
        get => (States)anim.GetInteger("state");
        set => anim.SetInteger("state", (int)value);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        initialPosition = transform.position;
        UpdateHealthUI();
    }

    private void FixedUpdate() => CheckGround();

    private void Update()
    {
        if (lives <= 0)
        {
            DieAndRespawn();
            return;
        }

        if (isGrounded && !isCrouching)
            State = States.idle;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            Run();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.F))
            Attack();

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0f)
            Dash();

        if (Input.GetKeyDown(KeyCode.S))
            Crouch(true);
        else if (Input.GetKeyUp(KeyCode.S))
            Crouch(false);

        UpdateHealthUI();
    }

    private void Run()
    {
        if (isGrounded) State = States.run;
        float moveInput = Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : 0f;

        Vector3 dir = transform.right * moveInput;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = moveInput < 0.0f;// ->W<-
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = States.jump;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        isGrounded = collider.Length > 1;
    }

    public void GetDamage(int amount)
    {
        lives -= amount;
        Debug.Log(lives);
        if (lives <= 0) DieAndRespawn();
    }

    public void Heal(int amount)
    {
        lives += amount;
        Debug.Log($"Restored {amount} health.");
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);
        if (hit.collider != null)
        {
            WalkingMonster monster = hit.collider.GetComponent<WalkingMonster>();
            if (monster != null)
            {
                monster.TakeDamage(1);
                Debug.Log("Attack dealt 1 damage to the monster!");
            }
        }
    }

    private void Dash()
    {
        Vector2 dashDirection = transform.right * dashDistance;
        rb.AddForce(dashDirection, ForceMode2D.Impulse);
        dashTimer = dashCooldown;
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"Health: {lives}";
    }

    private void LateUpdate()
    {
        if (dashTimer > 0f) dashTimer -= Time.deltaTime;
    }

    private void Crouch(bool crouching)
    {
        isCrouching = crouching;
        State = isCrouching ? States.crouch : States.idle;
    }

    private void DieAndRespawn()
    {
        Debug.Log("Hero has died!");
        transform.position = initialPosition;
        lives = 5;
        UpdateHealthUI();
    }

    public int CompareTo(Hero other)
    {
        if (other == null) return 1;
        return lives.CompareTo(other.lives);
    }

    public bool Equals(Hero other)
    {
        if (other == null) return false;
        return lives == other.lives;
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return format switch
        {
            "L" => $"Lives: {lives}",
            "S" => $"Speed: {speed}",
            _ => ToString()
        };
    }

    // Dekonstruktor
    public void Deconstruct(out int heroLives, out float heroSpeed)
    {
        heroLives = lives;
        heroSpeed = speed;
    }
}

// Naudojimo pavyzdys
public class ExampleUsage : MonoBehaviour
{
    void Start()
    {
        Hero hero = new Hero();
        var (lives, speed) = hero;
        Debug.Log($"Hero lives: {lives}, Speed: {speed}");
    }
}

// Abstrakti klasė
public abstract class Character : MonoBehaviour
{
    public abstract void Move();
}

// Enum States
public enum States
{
    idle,
    run,
    jump,
    crouch
}
