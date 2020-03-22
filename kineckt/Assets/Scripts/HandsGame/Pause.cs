using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool check1 = false;
    public GameObject canvas1;
    public GameObject canvas2;
    public Scrollbar scroll;
    private float oldValue = 0f;
    public static bool pause = false;
    public float mw;
    // Start is called before the first frame update
    void Start()
    {
        canvas1.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            if (canvas2.transform.GetChild(0).gameObject.activeSelf == false) { canvas2.transform.GetChild(0).gameObject.SetActive(true); pause = true; }
            else { canvas2.transform.GetChild(0).gameObject.SetActive(false); pause = false; }


            if (oldValue < scroll.value)
            {
                Kinect.velocityX += scroll.value * 100f;
                Kinect.velocityY += scroll.value * 100f;
            }
            else
            {
                Kinect.velocityX -= scroll.value * 100f;
                Kinect.velocityY -= scroll.value * 100f;
            }
            oldValue = scroll.value;
        }
    }

    private void OnGUI()
    {
        
    }



}
