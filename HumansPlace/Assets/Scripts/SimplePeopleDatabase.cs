using System;
using UnityEngine;

[CreateAssetMenu]
public class SimplePeopleDatabase : ScriptableObject
{
    [SerializeField]
    private Human[] people;

    [Serializable]
    public class Human
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private Gender gender;
        [SerializeField, Tooltip("Общее количество ушей")]
        private int earedness;
    }

    public enum Gender
    {
        Undefined = 0,
        Male = 1,
        Female = 2,
    }
}