using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.UI;

public class GameSpaceX : MonoBehaviour
{
    KinectSensor _kinect;
    BodyFrameSource _bodyFrameSource;
    BodyFrameReader _bodyFrameReader;
    Body[] _bodies;
    Body _currentBody = null;
    public static int score = 0;
    float currCountdownValue;
    public Text textscore;
    public static float timeLeft = 0.5f;
    public GameObject handleft;
    public GameObject Head;
    public GameObject Neck1;
    public GameObject handrigth;
    public GameObject left_shoulder1;
    public GameObject right_shoulder1;
    public GameObject right_hip1;
    public GameObject left_hip1;
    public GameObject handTip_Left1;
    public GameObject handTip_Right1;
    public GameObject knee_Left1;
    public GameObject Knee_Right1;
    public GameObject spineBase1;
    public GameObject spineMid1;
    public GameObject ankle_Right1;
    public GameObject ankle_Left1;
    public GameObject elbow_Left1;
    public GameObject elbow_rigth1;
    public GameObject foot_Right1;
    public GameObject foot_left1;
    public GameObject thumb_Right1;
    public GameObject thumb_Left1;
    // Start is called before the first frame update
    void Start()
    {
        InitKinect();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void InitKinect()
    {
        _bodies = new Windows.Kinect.Body[6];
        _kinect = Windows.Kinect.KinectSensor.GetDefault();
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
                if (body != null && body.IsTracked)
                {
                    Windows.Kinect.Joint head = body.Joints[JointType.Head];
                    Windows.Kinect.Joint neck = body.Joints[JointType.Neck];
                    Windows.Kinect.Joint left_shoulder = body.Joints[JointType.ShoulderLeft];
                    Windows.Kinect.Joint right_shoulder = body.Joints[JointType.ShoulderRight];
                    Windows.Kinect.Joint right_hip = body.Joints[JointType.HipRight];
                    Windows.Kinect.Joint left_hip = body.Joints[JointType.HipLeft];
                    Windows.Kinect.Joint handTip_Left = body.Joints[JointType.HandTipLeft];
                    Windows.Kinect.Joint handTip_Right = body.Joints[JointType.HandTipRight];
                    Windows.Kinect.Joint knee_Left = body.Joints[JointType.KneeLeft];
                    Windows.Kinect.Joint Knee_Right = body.Joints[JointType.KneeRight];
                    Windows.Kinect.Joint spineBase = body.Joints[JointType.SpineBase];
                    Windows.Kinect.Joint spineMid = body.Joints[JointType.SpineMid];
                    Windows.Kinect.Joint spineShoulder = body.Joints[JointType.SpineShoulder];
                    Windows.Kinect.Joint thumb_Left = body.Joints[JointType.ThumbLeft];
                    Windows.Kinect.Joint thumb_Right = body.Joints[JointType.ThumbRight];
                    Windows.Kinect.Joint wrist_Left = body.Joints[JointType.WristLeft];
                    Windows.Kinect.Joint wrist_Right = body.Joints[JointType.WristRight];
                    Windows.Kinect.Joint ankle_Left = body.Joints[JointType.AnkleLeft];
                    Windows.Kinect.Joint ankle_Right = body.Joints[JointType.AnkleRight];
                    Windows.Kinect.Joint elbow_Left = body.Joints[JointType.ElbowLeft];
                    Windows.Kinect.Joint elbow_Right = body.Joints[JointType.ElbowRight];
                    Windows.Kinect.Joint foot_Left = body.Joints[JointType.FootLeft];
                    Windows.Kinect.Joint foot_Right = body.Joints[JointType.FootRight];
                    Windows.Kinect.Joint hand_Left = body.Joints[JointType.HandLeft];
                    Windows.Kinect.Joint hand_Right = body.Joints[JointType.HandRight];

                    Debug.Log(hand_Right.Position.X + "Y" + hand_Right.Position.Y);
                    handleft.transform.localPosition = new Vector3(hand_Left.Position.X * 150f, hand_Left.Position.Y * 150f, 0);
                    Head.transform.localPosition = new Vector3(head.Position.X*150f, head.Position.Y* 150f, 0);

                    handrigth.transform.localPosition = new Vector3(hand_Right.Position.X, hand_Right.Position.Y, 0);
                    left_shoulder1.transform.localPosition = new Vector3(left_shoulder.Position.X, left_shoulder.Position.Y, 0);
                    right_shoulder1.transform.localPosition = new Vector3(right_shoulder.Position.X, right_shoulder.Position.Y, 0);

                    right_hip1.transform.localPosition = new Vector3(right_hip.Position.X, right_hip.Position.Y, 0);
                    left_hip1.transform.localPosition = new Vector3(left_hip.Position.X, left_hip.Position.Y, 0);

                    knee_Left1.transform.localPosition = new Vector3(knee_Left.Position.X, knee_Left.Position.Y, 0);
                    Knee_Right1.transform.localPosition = new Vector3(Knee_Right.Position.X, Knee_Right.Position.Y, 0);

                    foot_Right1.transform.localPosition = new Vector3(foot_Right.Position.X * 150f, foot_Right.Position.Y * 150f, 0);
                    foot_left1.transform.localPosition = new Vector3(foot_Left.Position.X * 150f, foot_Left.Position.Y * 150f, 0);

                    spineBase1.transform.localPosition = new Vector3(spineBase.Position.X, spineBase.Position.Y, 0);
                    spineMid1.transform.localPosition = new Vector3(spineMid.Position.X, spineMid.Position.Y, 0);

                    ankle_Left1.transform.localPosition = new Vector3(ankle_Left.Position.X * 150f, ankle_Left.Position.Y * 150f, 0);
                    ankle_Right1.transform.localPosition = new Vector3(ankle_Right.Position.X * 150f, ankle_Right.Position.Y * 150f, 0);

                    elbow_rigth1.transform.localPosition = new Vector3(elbow_Right.Position.X, elbow_Right.Position.Y, 0);
                    elbow_Left1.transform.localPosition = new Vector3(elbow_Left.Position.X, elbow_Left.Position.Y, 0);

                    thumb_Right1.transform.localPosition = new Vector3(thumb_Right.Position.X, thumb_Right.Position.Y, 0);
                    thumb_Left1.transform.localPosition = new Vector3(thumb_Left.Position.X, thumb_Left.Position.Y, 0);

                    Neck1.transform.localPosition = new Vector3(neck.Position.X, neck.Position.Y, 0);
                   

                    _currentBody = body;
                    break;
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
