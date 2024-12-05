using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool isFalling = false; 
    private Vector3 originalPosition; 

    private void Start()
    {
        originalPosition = transform.position; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero")) 
        {
            if (!isFalling) 
            {
                StartCoroutine(FallAndReturn()); 
            }
        }
    }

    private IEnumerator FallAndReturn()
    {
        isFalling = true; 

       
        yield return new WaitForSeconds(2f);

       
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false; 
        rb.gravityScale = 1; 

       
        yield return new WaitForSeconds(5f);

        
        rb.isKinematic = true; 
        rb.gravityScale = 0; 
        transform.position = originalPosition; 

        isFalling = false; 
    }
}

