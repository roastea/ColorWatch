using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    UnityEditor.EditorApplication.isPlaying = false;
        //else
        //    Application.Quit();
    }

}
