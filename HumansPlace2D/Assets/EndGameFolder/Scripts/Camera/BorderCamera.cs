using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCamera : MonoBehaviour
{
    private static float border = 0;
    
    public static float Border
    {
        get
        {
            if (border == 0)
            {
                var mainCamera = Camera.main;
                border = mainCamera.aspect * mainCamera.orthographicSize;
            }
            return border;
        }
        private set { }
    }

}
