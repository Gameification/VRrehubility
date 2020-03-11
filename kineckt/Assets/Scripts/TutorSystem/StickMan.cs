using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class StickMan : MonoBehaviour
{
    Body[] _bodies;
    Body currentBody;
    KinectSensor _kinect;
    BodyFrameSource _bodyFrameSource;
    BodyFrameReader _bodyFrameReader;
    public JointType Joint1;
    public JointType Joint2;
    public GameObject handRigth;
    public GameObject handLeft;
    private static float velocityX = 70f;
    private static float velocityY = 70f;
    private Vector3 pos;
    private Vector3 oldPos;
    private Vector3 oldPos2;
    public float smoothing;

    public EventHand _eventSender = null;
    public float velosity = 4.5f;
    public float x = 5, y = 5;
    public GameObject pref;
    public LineRenderer line;
    private List<GameObject> list = new List<GameObject>(25);
    private Folower _folower = null;

    // -------------------------------------------------
    void Start()
    {
        _folower = Camera.main.GetComponent<Folower>();
        line.enabled = false;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.positionCount = 37;
        y = 1f;
        x = 0.5f;
        velosity = 5.5f;
        for (int i = 0; i < 25; i++)
        {
            list.Add(new GameObject(i.ToString()));
        }
        Transform StickMan = GameObject.Find("StickMan").transform;
        foreach (GameObject jkaem in list)
        {
            Instantiate(jkaem, StickMan);
            Instantiate(pref, jkaem.transform);
        }
        InitKinect();
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
        using (var multiSourceFrame = e.FrameReference.AcquireFrame())
        {
            multiSourceFrame.GetAndRefreshBodyData(_bodies);

            foreach (var body in _bodies)
            {
                if (body != null && body.IsTracked)
                {
                    SetPos(body);
                    if(_folower.HandsOn){
                        float x = body.Joints[Joint1].Position.X;
                        float y = body.Joints[Joint1].Position.Y;
                        handRigth.transform.localPosition = Vector3.Slerp(oldPos, new Vector3(x * velocityX, y * velocityY, -1), smoothing * Time.deltaTime);
                        oldPos = new Vector3(x * velocityX, y * velocityY, -1);
                        x = body.Joints[Joint2].Position.X;
                        y = body.Joints[Joint2].Position.Y;
                        handLeft.transform.localPosition = Vector3.Slerp(oldPos2, new Vector3(x * velocityX, y * velocityY, -1), smoothing * Time.deltaTime);
                        oldPos2 = new Vector3(x * velocityX, y * velocityY, -1);
                    }
                    currentBody = body;
                    if (currentBody.HandLeftState == HandState.Closed)
                    {
                        _eventSender.LeftHandStateD = true;
                    }
                    else
                    {
                        _eventSender.LeftHandStateD = false;
                    }

                    if (currentBody.HandRightState == HandState.Closed)
                    {
                        _eventSender.RigthHandStateD = true;
                    }
                    else
                    {
                        _eventSender.RigthHandStateD = false;
                    }

                    break;
                }
                else
                {
                    //line.GetComponent<LineRenderer>().enabled = false;
                }
            }
        }
    }

    private void SetPos(Body body)
    {
        int i = 0;
        foreach (JointType joint in Enum.GetValues(typeof(JointType)))
        {

            list[i].transform.localPosition = new Vector3(body.Joints[joint].Position.X * velosity + x, body.Joints[joint].Position.Y * velosity + y, 0);
            i++;
        }
        LineRenderer();
    }

    // 3, 2, 20 (Head + Neck) | 8, 9, 10, 11, 23, 11, 10, 9, 8, 20 (LeftHnd) | 4, 5, 6, 7, 21, 7, 6, 5, 4, 20 (RightHnd) | 1, 0 (Body) | 
    // 16, 17, 18, 19, 18, 17, 16, 0 (LeftLeg) | 12, 13, 14, 15, 14, 13, 12, 0 (RightLeg)
    private void LineRenderer()
    {
        line.SetPosition(0, GameObject.Find("3").transform.position);
        line.SetPosition(1, GameObject.Find("2").transform.position);
        line.SetPosition(2, GameObject.Find("20").transform.position);
        line.SetPosition(3, GameObject.Find("8").transform.position);
        line.SetPosition(4, GameObject.Find("9").transform.position);
        line.SetPosition(5, GameObject.Find("10").transform.position);
        line.SetPosition(6, GameObject.Find("11").transform.position);
        line.SetPosition(7, GameObject.Find("23").transform.position);
        line.SetPosition(8, GameObject.Find("11").transform.position);
        line.SetPosition(9, GameObject.Find("10").transform.position);
        line.SetPosition(10, GameObject.Find("9").transform.position);
        line.SetPosition(11, GameObject.Find("8").transform.position);
        line.SetPosition(12, GameObject.Find("20").transform.position);
        line.SetPosition(13, GameObject.Find("4").transform.position);
        line.SetPosition(14, GameObject.Find("5").transform.position);
        line.SetPosition(15, GameObject.Find("6").transform.position);
        line.SetPosition(16, GameObject.Find("7").transform.position);
        line.SetPosition(17, GameObject.Find("21").transform.position);
        line.SetPosition(18, GameObject.Find("7").transform.position);
        line.SetPosition(19, GameObject.Find("6").transform.position);
        line.SetPosition(20, GameObject.Find("5").transform.position);
        line.SetPosition(21, GameObject.Find("4").transform.position);
        line.SetPosition(22, GameObject.Find("20").transform.position);
        line.SetPosition(23, GameObject.Find("1").transform.position);
        line.SetPosition(24, GameObject.Find("0").transform.position);
        line.SetPosition(25, GameObject.Find("16").transform.position);
        line.SetPosition(26, GameObject.Find("17").transform.position);
        line.SetPosition(27, GameObject.Find("18").transform.position);
        line.SetPosition(28, GameObject.Find("19").transform.position);
        line.SetPosition(29, GameObject.Find("18").transform.position);
        line.SetPosition(30, GameObject.Find("17").transform.position);
        line.SetPosition(31, GameObject.Find("16").transform.position);
        line.SetPosition(32, GameObject.Find("0").transform.position);
        line.SetPosition(33, GameObject.Find("12").transform.position);
        line.SetPosition(34, GameObject.Find("13").transform.position);
        line.SetPosition(35, GameObject.Find("14").transform.position);
        line.SetPosition(36, GameObject.Find("15").transform.position);
    }

    public void EnableLineRenderer(bool check)
    {
        line.enabled = check;
    }


}

