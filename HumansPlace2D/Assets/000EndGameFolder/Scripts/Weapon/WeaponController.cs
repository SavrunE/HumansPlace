using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float Offset;

    //получать тип оружия нужно как-то, его характеристики должны передаваться сюда, 
    //изображения, параметры стрельбы, дальность атаки если мили и прочее
    public bool CanShoot;
    public bool ShootingNow;
    public GameObject Bullet;
    public Transform ShotPoint;

    public float DelayToAttack;
    private float CheckDelayToAttack;


    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + Offset);


    }
    private void FixedUpdate()
    {
        if (CheckDelayToAttack <= 0)
        {
            StartCoroutine(RangeWeaponAttack());


        }
        CheckDelayToAttack -= Time.fixedDeltaTime;
    }
    IEnumerator RangeWeaponAttack()
    {
        if (Input.GetMouseButton(0))
        {
            float timeToWait = 1f;
            StartAnimationRangeAttack(timeToWait);
            yield return new WaitForSeconds(timeToWait);

            Instantiate(Bullet, ShotPoint.position, transform.rotation);
            CheckDelayToAttack = DelayToAttack;
        }
    }
    // тут у нас будет запуск анимации
    void StartAnimationRangeAttack(float time)
    {

    }
}
