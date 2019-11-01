using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FScore : MonoBehaviour
{
    static bool checkSkill;
    static bool checkShield;

    

    public int Fscore;
    Text t;

    private pla controller;
    private pla other_controller;


    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        other_controller = GameObject.Find("Player").GetComponent<pla>();

    }

    // Update is called once per frame
    void Update()
    {
        Fscore = other_controller.Decibel;
        textScore();
    }

    public void Init()
    {
        Fscore = 0;
    }


    void textScore()
    {
        t.text = "F의 Score" + Fscore;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fscore += 1;
            t.text = "F의 Score" + Fscore;
                       
        }
        if (Fscore == 50)
        {
            controller = GetComponentInParent<Canvas>().GetComponentInParent<pla>();
            controller.Finish_Competition(1);

        }
        //Debug.Log("시발시발" + SceneChange.checkSkill);






    }
}
