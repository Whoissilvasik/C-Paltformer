using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float baseBounceForce = 20f;
    public AudioClip bounceSound;
    public Animator trampolineAnimator;
    public float maxBounceForce = 100f;
    public float velocityMultiplier = 1.5f;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;  // SpriteRenderer added for animation control
    private Vector3 initialScale;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale; // Store the initial scale of the trampoline
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float bounceForce = CalculateBounceForce(rb);
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            PlayBounceSound();
            TriggerBounceAnimation();
        }
    }

    private float CalculateBounceForce(Rigidbody2D rb)
    {
        float fallSpeed = Mathf.Abs(rb.velocity.y);
        float bounceForce = baseBounceForce + (fallSpeed * velocityMultiplier);
        return Mathf.Min(bounceForce, maxBounceForce); // Limit the bounce force
    }

    private void PlayBounceSound()
    {
        if (audioSource != null && bounceSound != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }
    }

    private void TriggerBounceAnimation()
    {
        if (trampolineAnimator != null)
        {
            trampolineAnimator.SetTrigger("Bounce");
        }
    }

    private void Update()
    {
        AnimateTrampoline(); // Handle trampoline animation
    }


    private void AnimateTrampoline()
    {
        if (spriteRenderer != null)
        {
            // Ensuring the trampoline starts with the correct scale.
            float bounceEffect = Mathf.PingPong(Time.time * 2f, 0.05f);
            transform.localScale = new Vector3(initialScale.x, initialScale.y + bounceEffect, initialScale.z);
        }
    }
}