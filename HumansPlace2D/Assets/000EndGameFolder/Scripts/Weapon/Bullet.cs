using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public float Damage;
    public LayerMask Obstacle;


    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, Distance, Obstacle);

    }
}