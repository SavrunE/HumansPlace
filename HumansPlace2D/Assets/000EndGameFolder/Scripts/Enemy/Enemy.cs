using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData data;

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
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * data.Speed * Time.fixedDeltaTime);
        if (transform.position.y < -10 && OnEnemyOverFly != null)
            OnEnemyOverFly(gameObject);

    }
}
