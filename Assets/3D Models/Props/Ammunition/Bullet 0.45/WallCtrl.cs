using UnityEngine;
using System.Collections;

public class WallCtrl : MonoBehaviour
{
    void OnCallisionEnter(Collision col)
    {
        if (col.collider.tag == "Bullet") Destroy(col.gameObject);
    }
}
