using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Pause.pause = false;
        Kinect.score = 0;
    }
    public void GoContin()
    {
        canvas.SetActive(false);

        Pause.pause = false;
    }
}
