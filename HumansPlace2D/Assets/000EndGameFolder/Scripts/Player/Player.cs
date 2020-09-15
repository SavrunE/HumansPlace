using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player Singleton { get; private set; }

    public ControllerType Controller;
    public enum ControllerType { PC, Android }
    public Joystick JoystickMove;
    public Joystick JoystickAttack;

    [Range(1f, 5f)]
    [SerializeField] private float speed = 1f;
    public float Health { get; set; }
    public int Score { get; set; }

    public DamageStrongParameters DamageStrongParameters;
    public event OnCoinTake CoinTake;
    public delegate void OnCoinTake(int totalCoins);

    private Rigidbody2D PlayersRigidbody;
    private Animator animator;

    public float attackDelay = 1f;//заменить на класс атаки или ченеть типо того
    [SerializeField] private int countOfAttacks;
    [SerializeField] private GameObject attackHitBox;

    private int experience;
    public int Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
        }
    }
    public int Level
    {
        get
        {
            return experience / 1000;
        }
        set
        {
            experience = value * 1000;
        }
    }

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public bool LookOnLeft;



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
        attackHitBox.SetActive(false);
        animator = GetComponent<Animator>();
        PlayersRigidbody = GetComponent<Rigidbody2D>();

        //Say.OnHit += delegate ()
        //{
        //    Debug.Log("OnHit");
        //};


        //transform.position = startPosition;
    }
    private void Update()
    {
        //float HorizontalInput;
        //float VerticalInput;


        if (Controller == ControllerType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            moveInput = new Vector2(JoystickMove.Horizontal, JoystickMove.Vertical);
        }






        if (LookOnLeft == true && moveInput.x < 0)
            Flip();
        else if (LookOnLeft == false && moveInput.x > 0)
            Flip();
        if (moveInput != Vector2.zero)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);
    }
    private void FixedUpdate()
    {
        PlayersRigidbody.MovePosition(PlayersRigidbody.position + moveInput * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q) && !isAttacking)
        {
            isAttacking = true;

            int randomAttack = UnityEngine.Random.Range(1, countOfAttacks);
            animator.Play("Attack" + randomAttack);

            StartCoroutine(DoAttack());
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


        //что-то с этим сделать нужно
        if (collision.gameObject.TryGetComponent(out EnemySpawner enemy) && EnemySpawner.Enemies.ContainsKey(attacker))
        {
            float hit = EnemySpawner.Enemies[attacker].Attack;
            Health -= hit;
            if (hit <= DamageStrongParameters.Low)
                Debug.Log("HAHAHA low damage can't stop me now! -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Normal)
                Debug.Log("I DONT CARE! -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Hard)
                Debug.Log("OMG WTF !@#$% -" + hit + "HP");
            else if (hit <= DamageStrongParameters.Terrible)
                Debug.Log("My life is over, die is coming, soon. -" + hit + "HP");

            if (Health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("!@#$%, ты проиграл!");
            }
            if (hit <= DamageStrongParameters.Terrible)
                Debug.Log("!!!THAT Was a JOKE!!!");
        }

    }


    void EnemyTakeHit()
    {
        Score++;
        Debug.Log(Score);
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


    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        attackHitBox.SetActive(false);
        isAttacking = false;
    }



}
