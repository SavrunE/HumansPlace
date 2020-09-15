using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5f;
    public float LifeTime = 3f;
    public float Distance = 15f;
    public float Damage = 5f;
    public LayerMask Obstacle;


    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, Distance, Obstacle);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}