using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Standart Enemy", fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject Character;
   
    [SerializeField] private float speed = 1f;
    public float Speed
    {
        get { return speed; }
        set { }
    }
    [SerializeField] private float attackDamage = 2f;
    public float AttackDamage
    {
        get { return attackDamage; }
        set { }
    }
    [SerializeField] private float health = 5f;
    public float Health
    {
        get { return health; }
        set { }
    }
}
