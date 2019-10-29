using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameObject button;

    public void changeMaterial()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;

    }
}