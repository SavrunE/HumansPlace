using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float health = 100f;
    [Range(0.1f, 5f)]
    [SerializeField] private float speed = 1f;
    public DamageStrongParameters DamageStrongParameters;
    private void Start()
    {
        transform.position = startPosition;
        var Fliper = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -CameraBorder.Border)
        {
            transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < CameraBorder.Border)
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime); 
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
