using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;           
    public float flightDistance = 5f;  
    public float fixedHeight = 1f;     
    private Vector3 startPosition;    
    private bool movingRight = true;  

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        
        float traveledDistance = Mathf.Abs(transform.position.x - startPosition.x);

       
        if (traveledDistance >= flightDistance)
        {
           
            movingRight = !movingRight;
        }

       
        float direction = movingRight ? 1 : -1;

        // Move the platform only x, fixed y
        transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, fixedHeight, transform.position.z);
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.collider is BoxCollider2D)
        {
            
            movingRight = !movingRight;
        }
    }
}
