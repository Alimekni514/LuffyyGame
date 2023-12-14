using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GolemBoss : MonoBehaviour
{
    public int bossHealth = 100;
    public int bossAttackDamage ;
    public float attackRange = 2f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public int speed;
    private Rigidbody2D myBody;
    private Animator bossAnimator;
    public AudioSource LaughAudioSource;
    public GameObject keyObject;
    public GameObject healthBar;
    public HealthBar healthBarBoss;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        bossAnimator = GetComponent<Animator>();
        // Set the boss's initial position
        transform.position = new Vector3(45f, -2.37f, 0f);
        speed = -1;
        // Initialize the boss's state or any other properties
        bossHealth = 100;
        // Play a walking animation
        bossAnimator.SetTrigger("Walk");
        healthBar.SetActive(true);
        healthBarBoss.SetMaxHealth(bossHealth);
    }



    // Update is called once per frame
    void Update()
    {
        
        if (Time.time >= nextAttackTime)
        {
            BossBehavior();
            nextAttackTime = Time.time + 1f / attackRate;
        }
      


    }
    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }
    void BossBehavior()
    {
        // Implement logic for boss behavior

        // For simplicity, let's assume the boss attacks whenever Luffy is nearby
        Luffy luffy = FindObjectOfType<Luffy>();
        if (luffy != null)
        {
            float distanceToLuffy = Vector2.Distance(transform.position, luffy.transform.position);
            print("disatance ll luffy:" + distanceToLuffy);

            if (distanceToLuffy < attackRange)
            {
                // Boss is close to Luffy, play attack animation
                bossAnimator.SetTrigger("Attack");
                luffy.GetComponent<Animator>().SetTrigger("Hurt");
                BossAttack();
              
            }
            else
            {
                // Boss is not close to Luffy, play walk animation
                bossAnimator.SetTrigger("Walk");
            }
        }
    }
    void BossAttack()
    {
        // Implement logic for boss attacking Luffy
        // For example, check if Luffy is in range and deal damage

        // For simplicity, let's assume the boss attacks whenever Luffy is nearby
        Luffy luffy = FindObjectOfType<Luffy>();
            Debug.Log("Boss attacking Luffy!");
            luffy.TakeDamage(bossAttackDamage);
   
    }
   
    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        healthBarBoss.SetHealth(bossHealth);

        print("current health Golem egal " + bossHealth);

        // Implement logic for hurt animation, effects, etc.
        // Trigger hurt animation

        // Trigger hurt animation if not already in the hurt state
        bossAnimator.SetTrigger("Hurt");
        if (bossHealth <= 0)
        {
          
            keyObject.SetActive(true);
            Destroy(gameObject,0.2f);
            Destroy(healthBar);
        }
    }
    public bool notIsBossDead()
    {
        return bossHealth >= 0;
    }
    
}
