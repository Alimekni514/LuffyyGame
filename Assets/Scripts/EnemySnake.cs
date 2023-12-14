using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private Animator animator;
    private Rigidbody2D myBody;
    public float speed;
    private bool isStopped = false;
    private bool isAlive = true;
    private GameObject SacCoins;
    [SerializeField]
    private GameObject SacCoinsReference;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
      
     
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
           
            if (!isStopped)
            {
               
                // Move the snake using Rigidbody2D or transform
                myBody.velocity = new Vector2(speed, myBody.velocity.y);

            }
            else
            {
             
                myBody.velocity = Vector2.zero; // Stop movement
                animator.SetTrigger("Stop");
            }
        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

        // Implement logic for hurt animation, effects, etc.
        // Trigger hurt animation
      
            // Trigger hurt animation if not already in the hurt state
            animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        // Implement logic for death, such as playing death animation, dropping items, etc.
        Destroy(gameObject,0.1f);
        SacCoins = Instantiate(SacCoinsReference);
        // Right Side
        SacCoins.transform.position = transform.position;
        SacCoins.transform.position=new Vector3(transform.position.x, transform.position.y - 0.3f, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the snake when it collides with the player
            isStopped = true;
            animator.SetTrigger("Stop");
            // Optionally, you can add more logic here, such as hurting the player.
        }
    }
}
