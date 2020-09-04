using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{

    public delegate void SomeAction(int argument);
    public event SomeAction GameStartAction;

    void Start()
    {
        GameStartAction?.Invoke(10000);
    }
}
