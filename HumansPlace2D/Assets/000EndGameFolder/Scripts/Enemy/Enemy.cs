using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData data;
    bool isShaking = false;
    float shake = 2f;
    Vector2 position;

    public void Init(EnemyData data)
    {
        this.data = data;
        GetComponent<SpriteRenderer>().sprite = data.MainSprite;
    }
    public float Attack
    {
        get { return data.Attack; }
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
        
        if (isShaking)
        {
            position = transform.position;
            transform.position = position + UnityEngine.Random.insideUnitCircle * shake;
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
}
