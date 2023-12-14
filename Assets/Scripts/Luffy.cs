using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class Luffy : MonoBehaviour
{
    public delegate void BossSpawnedEventHandler();
    public static event BossSpawnedEventHandler OnBossSpawned;
    public delegate void FighterSpawnedEventHandler();
    public static event FighterSpawnedEventHandler OnFighterSpawned;

    // Reference to the tree object
    public GameObject tree;
    public GameObject treasureMap;
    public GameObject GameOver;
    // Distance threshold to trigger the event
    public float triggerDistance = 1f;
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    private float movementX;
    [SerializeField] 
    private Rigidbody2D myBody;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public TextMeshProUGUI keyText;
    public HealthBar healthBar;
    public int key = 0;

    private string WALK_ANIMATION = "Walk";
    private bool ISGROUNDED;
    private string GROUND_TAG = "Ground";
    // Tag for the Enenmy to detect the collission with the Enenmy 
    private string ENEMY_TAG = "Enemy";
    //The Score of the Player
    public int score = 0;
  
    // Start is called before the first frame update
    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.2f;
    public LayerMask enemyLayers;
    public LayerMask BossGolem;
    public LayerMask Scorpionn;
    public LayerMask fighter;
    public int attackDamage = 20;
    public int attackDamage2 = 15;
    public int FireAttackDamage = 30;
    public float specialCooldown = 5f; // Adjust the cooldown time as needed
    private bool canUseSpecial = true;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    private int LuffyHealth =100;
    public GameObject boxlocked;
    public Sprite boxunlocked;
    // Reference to the AudioSource component
    public AudioSource deathAudioSource;
    public AudioSource coinAudioSource;
    public AudioSource attackAudioSource;
    public AudioSource GolemAudioSource;
    public AudioSource laughAudioSource;
    public AudioSource keyAudioSource;
    public AudioSource HealAudioSource;
    public AudioSource Attack2AudioSource;
    public AudioSource FireAttackAudioSource;
    public AudioSource ScreamAudioSource;
    public AudioSource SacOfCoinsAudioSource;



    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }
    void Start()
    {
        GameManager.Instance.SetPlayerCharacter(gameObject);
        healthBar.SetMaxHealth(LuffyHealth);
        

    }

    // Update is called once per frame
    void Update()
    {
       
                PlayerMoveKeyboard();
                AnimatePlayer();
                PlayerJump();
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                MeeleAttack();
                nextAttackTime = Time.time + 1f / attackRate;
                Debug.Log("Time: " + Time.time);
                Debug.Log("Next Attack Time: " + nextAttackTime);
                Debug.Log("Attack Rate: " + 1f / attackRate);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                attack2();
                nextAttackTime=Time.time + 1f / attackRate;
                Debug.Log("Time: " + Time.time);
                Debug.Log("Next Attack Time: " + nextAttackTime);
                Debug.Log("Attack Rate: " + 1f / attackRate);
            }

        }
        if (Input.GetKeyDown(KeyCode.H) && canUseSpecial)
        {

            StartCoroutine(PlaySpecialAnimation());
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            Heal();
        }

        if (transform.position.y <= -5)
        {

            Destroy(gameObject);
            SceneManager.LoadScene("ForestScene");
        }
        


    }
    public void Heal()
    {
        if(score >0 && LuffyHealth < 100 )
        {
            score -= 10;
            anim.SetTrigger("Heal");
            HealAudioSource.Play();
            scoreText.text = "Score: " + score.ToString();
            LuffyHealth += 10;
            healthBar.SetHealth(LuffyHealth);
          
        }
      
    } 
    public void MeeleAttack()
    {
        anim.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] bossG = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossGolem);
        Collider2D[] Scorpions= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Scorpionn);
        Collider2D[] Fighter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, fighter);
        // For the Snakes
        foreach (Collider2D enemy in hitEnemies)
        {
                enemy.GetComponent<EnemySnake>().TakeDamage(attackDamage);
                attackAudioSource.Play();
                Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Boss
        foreach (Collider2D enemy in bossG)
        {
            enemy.GetComponent<GolemBoss>().TakeDamage(attackDamage);
            if(enemy.GetComponent<GolemBoss>().bossHealth<=0)
            {
                laughAudioSource.Play();

            }
            attackAudioSource.Play();
            Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Scorpion
        foreach  (Collider2D enemy in Scorpions)
        {
            enemy.GetComponent<Scorpion>().TakeDamage(attackDamage);
            
        }
        // for tyhe fighter 
        foreach (Collider2D enemy in Fighter)
        {
            enemy.GetComponent<Fighter>().TakeDamage(attackDamage);

        }

    }
    public void attack2()
    {
        anim.SetTrigger("attack2");
        Attack2AudioSource.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] bossG = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossGolem);
        Collider2D[] Scorpions = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Scorpionn);
        Collider2D[] Fighter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, fighter);
        // For the Snakes
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemySnake>().TakeDamage(attackDamage2);
            Attack2AudioSource.Play();
            Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Boss
        foreach (Collider2D enemy in bossG)
        {
            enemy.GetComponent<GolemBoss>().TakeDamage(attackDamage2);
            if (enemy.GetComponent<GolemBoss>().bossHealth <= 0)
            {
                laughAudioSource.Play();

            }
            Attack2AudioSource.Play();
            Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Scorpion
        foreach (Collider2D enemy in Scorpions)
        {
            enemy.GetComponent<Scorpion>().TakeDamage(attackDamage2);
                Attack2AudioSource.Play();

        }
        // for tyhe fighter 
        foreach (Collider2D enemy in Fighter)
        {
            enemy.GetComponent<Fighter>().TakeDamage(attackDamage2);
            Attack2AudioSource.Play();

        }

    }
    public void FireAttack()
    {
        anim.SetTrigger("FireAttack");
        FireAttackAudioSource.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] bossG = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossGolem);
        Collider2D[] Scorpions = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Scorpionn);
        Collider2D[] Fighter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, fighter);
        // For the Snakes
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemySnake>().TakeDamage(FireAttackDamage);
            FireAttackAudioSource.Play();
            Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Boss
        foreach (Collider2D enemy in bossG)
        {
            enemy.GetComponent<GolemBoss>().TakeDamage(FireAttackDamage);
            if (enemy.GetComponent<GolemBoss>().bossHealth <= 0)
            {
                laughAudioSource.Play();

            }
            FireAttackAudioSource.Play();
            Debug.Log("Attacking enemy: " + enemy.name);
        }
        // For the Scorpion
        foreach (Collider2D enemy in Scorpions)
        {
            enemy.GetComponent<Scorpion>().TakeDamage(FireAttackDamage);
            FireAttackAudioSource.Play();

        }
        // for tyhe fighter 
        foreach (Collider2D enemy in Fighter)
        {
            enemy.GetComponent<Fighter>().TakeDamage(FireAttackDamage);
            FireAttackAudioSource.Play();

        }

    }
    IEnumerator PlaySpecialAnimation()
    {
        // Set the flag to prevent triggering the special animation while it's playing
        canUseSpecial = false;
        FireAttack();

        // Wait for the duration of the special animation
        yield return new WaitForSeconds(specialCooldown);

        // Reset the flag after the cooldown period
        canUseSpecial = true;
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }
    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        
        transform.position += new Vector3(movementX, 0f, 0f)* Time.deltaTime*moveForce;
    }
    void AnimatePlayer ()

    {
        // We are going to the right side
        if(movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }else if(movementX < 0)
        {
            //We are going to the left side 

            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

    }
    void PlayerJump()
    {
      if(Input.GetButtonDown("Jump") && ISGROUNDED)
        {
            ISGROUNDED = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
                if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            ISGROUNDED = true;
            
        }
          
        else if (collision.gameObject.CompareTag("Coin"))
        {
            UpdateScoreUI();
            coinAudioSource.Play();
            Destroy(collision.gameObject); // Destroy the coin when the player collects it
        }else if (collision.gameObject.CompareTag("Key"))
        {
            keyAudioSource.Play();
            UpdateKeyUI();
            Destroy(collision.gameObject) ;
        }else if (collision.gameObject.CompareTag("Box"))
        {
           
            boxlocked.GetComponent<SpriteRenderer>().sprite = boxunlocked;
            treasureMap.SetActive(true);
            treasureMap.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f,2f);
        

        }else if(collision.gameObject.CompareTag("SharpShards"))
        {
            TakeDamage(3);
            healthBar.SetHealth(LuffyHealth);
            anim.SetTrigger("Hurt");
        }
       
        else if(collision.gameObject.CompareTag("TreasureMap"))
        {
           
            Invoke("LoadSceneDelayed", 1f);
        }else if(collision.gameObject.CompareTag("Treasure"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Open");
        }
  

    }
    void LoadSceneDelayed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }
    void UpdateKeyUI()
    {
        if (keyText != null)
        {
            key++;
            keyText.text = key.ToString();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.CompareTag("Mosquee"))
            OnFighterSpawned();
        if (collision.CompareTag("TreeGolem"))
        {
            OnBossSpawned();
            GolemAudioSource.Play();
        }
        if (collision.gameObject.CompareTag("Axe"))
        {
            TakeDamage(3);
            healthBar.SetHealth(LuffyHealth);
            anim.SetTrigger("Hurt");
        }
        if(collision.gameObject.CompareTag("RetractingSpikes"))
        {
            TakeDamage(5);
            healthBar.SetHealth(LuffyHealth);
            anim.SetTrigger("Hurt");
        }
        if(collision.gameObject.CompareTag("SacOfCoins"))
        {
            SacOfCoinsAudioSource.Play();
            Destroy(collision.gameObject);
            score = score + 10;
            scoreText.text = "Score: " + score.ToString();

        }
    }
    public void TakeDamage(int damage)
    {
        LuffyHealth -= damage;
        healthBar.SetHealth(LuffyHealth);
        ScreamAudioSource.Play();
        GolemBoss golembooss = FindObjectOfType<GolemBoss>();
        print("LuffyHeath" + LuffyHealth);
        // Implement logic for boss taking damage
        if (LuffyHealth <= 0)
        {
            // Play the death sound
            if (deathAudioSource != null && deathAudioSource.clip != null && golembooss != null && golembooss.notIsBossDead())
            {
                deathAudioSource.Play();
                // Wait for the duration of the audio clip before loading the death scene
                Invoke("ForestScene", deathAudioSource.clip.length);
                
            }
    
           
            // Boss defeated logic, e.g., destroy the boss
            Destroy(gameObject,0.5f);
            GameOver.SetActive(true);


        }
    }
}
