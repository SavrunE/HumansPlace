using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float Offset;

    //получать тип оружия нужно как-то, его характеристики должны передаваться сюда, 
    //изображения, параметры стрельбы, дальность атаки если мили и прочее
    private bool canShoot;
    public GameObject bullet;
    public Transform ShotPoint;


    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + Offset);

        if (Input.GetMouseButton(0))
        {
            Instantiate(bullet, ShotPoint.position, transform.rotation);
        }
    }

}
