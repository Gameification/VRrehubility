using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrial : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask mask2;
    public bool screenon = false;
    public static bool checkScreenshot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void Save(LayerMask mask2)
    {
            Camera.main.cullingMask = mask2;
            ScreenCapture.CaptureScreenshot("SomeLevel.png");
            checkScreenshot = true;
    }
}
