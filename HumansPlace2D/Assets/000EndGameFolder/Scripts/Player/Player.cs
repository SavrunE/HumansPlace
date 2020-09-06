using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player Singleton { get; private set; }
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float health = 100f;
    [Range(1f, 25f)]
    [SerializeField] private float speed = 1f;
    [SerializeField] private int Score = 0;
    public DamageStrongParameters DamageStrongParameters;
    SaySomethink Say;
    ShakeCamera screenShaker;

    public bool LookOnLeft;

    private Rigidbody2D PlayersRigidbody;

    public event OnCoinTake CoinTake;
    public delegate void OnCoinTake(int totalCoins);

    private Animator animator;

    [SerializeField] private int countOfAttacks;

    private bool isAttacking;
    private bool isDefended;
    private bool isTakeDamage;
    private bool safferingPain;
    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayersRigidbody = GetComponent<Rigidbody2D>();
        //Say.OnHit += delegate ()
        //{
        //    Debug.Log("OnHit");
        //};
        transform.position = startPosition;
    }
    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(HorizontalInput, VerticalInput);

        PlayersRigidbody.MovePosition(PlayersRigidbody.position + movement * speed * Time.deltaTime);

        if (LookOnLeft == true && HorizontalInput < 0)
            Flip();
        else if (LookOnLeft == false && HorizontalInput > 0)
            Flip();
        if (HorizontalInput != 0 || VerticalInput != 0)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q) && !isAttacking)
        {
            isAttacking = true;

            int randomAttack = UnityEngine.Random.Range(1, countOfAttacks);
            Debug.Log(randomAttack);
            animator.Play("Attack" + randomAttack);

            Invoke("RessetAttack", 0.5f);
            //animator.SetBool("HitEnemy", true);  
        }
        
        
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Score++;
            Destroy(collision.gameObject);

        }
        var attacker = collision.gameObject;
        
        if (EnemySpawner.Enemies.ContainsKey(attacker))
        {
            float hit = EnemySpawner.Enemies[attacker].Attack;
            health -= hit;
            if (hit <= DamageStrongParameters.Low)
                Debug.Log("HAHAHA low damage can't stop me now! -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Normal)
                Debug.Log("I DONT CARE! -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Hard)
                Debug.Log("OMG WTF !@#$% -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Terrible)
                Debug.Log("My life is over, die is coming, soon. -" + hit + "HP");

            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("!@#$%, ты проиграл!");
            }
            if (hit <= DamageStrongParameters.Terrible)
                Debug.Log("!!!THAT Was a JOKE!!!");
        }
        Destroy(attacker);
    }

    void Action()
    {
        Say.AY();
    }
    void EnemyTakeHit()
    {
        Score++;
        Debug.Log(Score);
    }
    void PlayerTakeHit()
    {
        health--;
        screenShaker.DoShake();
    }
    
    public void TakeCoin()
    {
        Score++;
        CoinTake?.Invoke(Score);
    }
    public void Flip()
    {
        LookOnLeft = !LookOnLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void RessetAttack()
    {
        isAttacking = false;
    }

}
