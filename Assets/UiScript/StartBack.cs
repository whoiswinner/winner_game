using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBack : MonoBehaviour
{
    // Start is called before the first frame update

    public void GameBack()
    {
        SceneManager.LoadScene("MainUi");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
