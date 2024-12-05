using UnityEngine;

public class Fan : MonoBehaviour
{
    
    public float upwardForce = 5f;

    public float maxLiftHeight = 10f;
    public float horizontalForce = 3f;
    
    public bool blowUpwards = true; // Vėjo kryptis (į viršų ar į šoną)

    
    private void OnTriggerStay2D(Collider2D other)
    {
       
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

      
        if (rb != null)
        {
           
            if (blowUpwards)
            {
               
                if (other.transform.position.y < maxLiftHeight)  // Jei objekto aukštis mažesnis nei maksimalus aukštis
                {
                    // Pakeliame personažą sklandžiai
                    float newVelocityY = upwardForce;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, newVelocityY);
                }
                else
                {
                    
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Apribojame judėjimą pagal aukštį, nustatydami vertikalią greitį į 0
                }
            }
            else
            {

                float newVelocityX = horizontalForce;
                rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        
        if (rb != null && !blowUpwards)
        {
           
            float newVelocityX = horizontalForce;
            rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
        }
    }
}
