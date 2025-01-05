using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushStartGame()
    {
        SceneManager.LoadScene("VideoScene");
    }

    public void PushBackTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
