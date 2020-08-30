using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Standart Enemy", fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { }
    }
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { }
    }
    [SerializeField] private float attack;
    public float Attack
    {
        get { return attack; }
        set { }
    }
}
