using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kinect : MonoBehaviour
{
    KinectSensor _kinect;
    BodyFrameSource _bodyFrameSource;
    BodyFrameReader _bodyFrameReader;
    Body[] _bodies; 
    Body _currentBody = null;
    public JointType Joint1;
    public JointType Joint2;
    
    public Vector3 pos;
    public GameObject handRigth;
    public GameObject handLeft;
    public static float velocityX = 400f;
    public static float velocityY = 200f;
    private Vector3 oldPos;
    private Vector3 oldPos2;
    public float smoothing;
    public static int score = 0;
    float currCountdownValue;
    public Text textscore;
    public static float timeLeft = 0.5f;
    public bool gamepaused;
    public GameObject exit;
    public GameObject next;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        InitKinect();

    }


    private void OnGUI()
    {
        if(Event.current.keyCode == KeyCode.Escape)
        {
            if (!gamepaused)
            {
                exit.SetActive(true);
                next.SetActive(true);
                panel.SetActive(true);
                gamepaused = true;
            }
            if (gamepaused)
            {
                NextButton();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        textscore.text = "Score: " + score;
    }

    public void NextButton()
    {
        exit.SetActive(false);
        next.SetActive(false);
        panel.SetActive(false);
        gamepaused = false;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }

    void InitKinect()
    {
        _bodies = new Body[6];
        _kinect = KinectSensor.GetDefault();
        _kinect.Open();
        _bodyFrameSource = _kinect.BodyFrameSource;
        _bodyFrameReader = _bodyFrameSource.OpenReader();
        _bodyFrameReader.FrameArrived += _bodyFrameReader_FrameArrived;
    }

    private void _bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        var frame = e.FrameReference;
        using (var multiSourceFrame = frame.AcquireFrame())
        {
            multiSourceFrame.GetAndRefreshBodyData(_bodies); 
            _currentBody = null;
            foreach (var body in _bodies)
            {
                if (!gamepaused)
                {
                    if (body != null && body.IsTracked /*&& !Pause.pause*/)
                    {
                        float x = body.Joints[Joint1].Position.X;
                        float y = body.Joints[Joint1].Position.Y;
                        float z = 9;
                        handRigth.transform.localPosition = Vector3.Slerp(oldPos, new Vector3(x * velocityX, y * velocityY, 6), smoothing * Time.deltaTime);
                        oldPos = new Vector3(x * velocityX, y * velocityY, 6);
                        x = body.Joints[Joint2].Position.X;
                        y = body.Joints[Joint2].Position.Y;
                        z = 9;
                        handLeft.transform.localPosition = Vector3.Slerp(oldPos2, new Vector3(x * velocityX, y * velocityY, 6), smoothing * Time.deltaTime);
                        oldPos2 = new Vector3(x * velocityX, y * velocityY, 6);
                        _currentBody = body;
                        break;
                    }
                }
            }
            if (_currentBody != null)
            {
                Debug.Log("_currentBody is not null");
            }
            else
            {
                Debug.Log("_currentBody is null");
            }
        }
    }
}
