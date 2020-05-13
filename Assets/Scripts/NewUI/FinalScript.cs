using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.UI;

public class FinalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ColorSourceManager colorsource;
    public RawImage image;

    // Update is called once per frame
    void Update()
    {
        image.texture = colorsource.GetColorTexture();
    }
}
