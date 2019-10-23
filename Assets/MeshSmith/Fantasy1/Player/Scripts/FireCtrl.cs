using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Fire();
    }

    void Fire()
    {
        CreateBullet();
    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
