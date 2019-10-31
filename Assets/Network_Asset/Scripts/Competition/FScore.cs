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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fscore += 1;
            t.text = "F의 Score" + Fscore;



        }
        if (Fscore == 50)
        {

            SceneChange.checkSkill = true;

            if (SceneChange.checkSkill)
            {

                SceneManager.UnloadScene("Soundsc");
            }


        }
        Debug.Log("시발시발" + SceneChange.checkSkill);






    }
}
