using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private Animator animator;
    private Rigidbody2D myBody;
    public float speed;
    private bool isStopped = false;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        if (isAlive)
        {

            if (!isStopped)
            {
                animator.SetTrigger("Walk");
                // Move the snake using Rigidbody2D or transform
                myBody.velocity = new Vector2(speed, myBody.velocity.y);

            }
            else
            {

                myBody.velocity = Vector2.zero; // Stop movement
                
            }
        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        // Implement logic for hurt animation, effects, etc.
        // Trigger hurt animation

        // Trigger hurt animation if not already in the hurt state
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        animator.SetTrigger("Die");
        // Implement logic for death, such as playing death animation, dropping items, etc.
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the snake when it collides with the player
            isStopped = true;
            // Optionally, you can add more logic here, such as hurting the player.
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
