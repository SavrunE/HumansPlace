using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyCharacter data;
    bool isShaking = false;
    float shake = 2f;
    Vector2 position;
    private float speed = 1f;
    public void Init(EnemyCharacter data)
    {
        this.data = data;
        //GetComponent<GameObject>() = data.Character;
    }
    public float Attack
    {
        get { return data.AttackDamage; }
        protected set { }
    }
    public static Action<GameObject> OnEnemyOverFly;
   
    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {

    }
    void Update()
    {
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (isShaking)
        {
            position = transform.position;
            transform.position = position + UnityEngine.Random.insideUnitCircle * shake;
            transform.position = position;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("212");
            isShaking = true;
            Invoke("StopShaking", 0.5f);
        }

    }
    void StopShaking()
    {
        isShaking = false;
    }

    public void TakeDamage(float damage)
    {
        data.Health -= damage;
        if (data.Health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
