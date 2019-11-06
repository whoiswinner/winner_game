using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BScore : MonoBehaviour
{
    static bool checkSkill;
    static bool checkShield;

    public int Bscore;
    Text t;

    private pla controller;



    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        controller = GetComponentInParent<Canvas>().GetComponentInParent<pla>();

    }

    // Update is called once per frame
    void Update()
    {
        Bscore = controller.Decibel;
        textScore();
    }

    public void Init()
    {
        Bscore = 0;
    }


    void textScore()
    {
        t.text = "B의 Score" + Bscore;

        if (Input.GetKeyDown(KeyCode.B))
        {
            Bscore += 1;
            t.text = "B의 Score" + Bscore;

        }
        if (Bscore >= 80)
        {
            
            controller.Finish_Competition(0);
        }





    }
}
