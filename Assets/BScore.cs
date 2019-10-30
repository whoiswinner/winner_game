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




    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        textScore();
    }


    void textScore()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bscore += 1;
            t.text = "B의 Score" + Bscore;



        }
        if (Bscore == 50)
        {

            SceneChange.checkShield = true;

            if (SceneChange.checkShield)
            {

                SceneManager.UnloadScene("Soundsc");
            }


        }





    }
}
