using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaySomethink : MonoBehaviour
{
    public event Action OnHit;
    public void AY()
    {
        Debug.Log("AY");
    }
}
