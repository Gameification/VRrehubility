using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class Control_Depth : MonoBehaviour
{
    public delegate void NewTriggerPoints(List<Vector2> vector2s);
    public static event NewTriggerPoints OnTriggerPoints = null;

    public MultiSourceManager m_MultiSource;
    public Texture2D m_DepthTexture;
    [Range(0, 1f)]
    public float m_DephSensevity = 1;
    [Range(-10, 10f)]
    public float m_WallDepth = -10;

    [Header("Top and Bottom")]
    [Range(-1, 1f)]
    public float m_TopCuOff = 1;
    [Range(-1, 1f)]
    public float m_BottomCutOff = -1;

    [Header("Left and Rigth")]
    [Range(-1, 1f)]
    public float m_LeftCutOff = -1;
    [Range(-1, 1f)]
    public float m_RigthCutOff = 1;

    private ushort[] m_DepthData = null;
    private CameraSpacePoint[] m_CameraSapcePoints = null;
    private ColorSpacePoint[] m_ColorSpacePoints = null;
    private List<ValidPoint> m_ValidPoints = null;
    private List<Vector2> mTriggerPoints = null;

    private KinectSensor m_Sensor = null;
    private CoordinateMapper m_Mapper = null;
    private Camera m_Camera = null;

    private readonly Vector2Int m_DepthResolution = new Vector2Int(512, 424);
    private Rect mRect;
    private void Awake()
    {
        m_Sensor = KinectSensor.GetDefault();
        m_Mapper = m_Sensor.CoordinateMapper;
        m_Camera = Camera.main;

        int arraySize = m_DepthResolution.x * m_DepthResolution.y;

        m_CameraSapcePoints = new CameraSpacePoint[arraySize];
        m_ColorSpacePoints = new ColorSpacePoint[arraySize];
    }

    private void Update()
    {
        m_ValidPoints = DepthToColor();

        mTriggerPoints = FilterToTrigger(m_ValidPoints);

        if (OnTriggerPoints != null && mTriggerPoints.Count != 0)
            OnTriggerPoints(mTriggerPoints);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mRect = CreateRect(m_ValidPoints);
            m_DepthTexture = CreateTexture(m_ValidPoints);
            Debug.Log(mRect.width + " " + mRect.height);
        }
    }

    void OnGUI()
    {
        GUI.Box(mRect, "d");
        if (mTriggerPoints == null)
            return;
        foreach(Vector2 point in mTriggerPoints)
        {
            Rect rect = new Rect(point, new Vector2(10, 10));
            GUI.Box(rect, "");
        }
    }

    private List<ValidPoint> DepthToColor()
    {
        List<ValidPoint> validPoints = new List<ValidPoint>();

        m_DepthData = m_MultiSource.GetDepthData();

        m_Mapper.MapDepthFrameToCameraSpace(m_DepthData, m_CameraSapcePoints);
        m_Mapper.MapDepthFrameToColorSpace(m_DepthData, m_ColorSpacePoints);

        for(int i = 0; i < m_DepthResolution.x / 8; i++)
        {
            for (int j = 0; j < m_DepthResolution.y / 8; j++)
            {
                int sampleIndex = (j * m_DepthResolution.x) + i;
                sampleIndex *= 8;

                //if (m_CameraSapcePoints[sampleIndex].X < m_LeftCutOff)
                //    continue;
                //if (m_CameraSapcePoints[sampleIndex].X > m_RigthCutOff)
                //    continue;
                //if (m_CameraSapcePoints[sampleIndex].Y > m_TopCuOff)
                //    continue;
                //if (m_CameraSapcePoints[sampleIndex].Y < m_BottomCutOff)
                //    continue;

                ValidPoint newPoint = new ValidPoint(m_ColorSpacePoints[sampleIndex], m_CameraSapcePoints[sampleIndex].Z);
                if (m_CameraSapcePoints[sampleIndex].Z >= m_WallDepth)
                    newPoint.m_WithinWallDepth = true;

                validPoints.Add(newPoint);
            }
        }
        Debug.Log(validPoints.Count);
        return validPoints;
    }

    private List<Vector2> FilterToTrigger(List<ValidPoint> validPoints)
    {
        List<Vector2> triggerPoints = new List<Vector2>();

        foreach (ValidPoint point in validPoints)
        {
            Vector2 screenPoint = ScreenToCamera(new Vector2(point.colorSpace.X, point.colorSpace.Y));

            if(point.z < m_WallDepth * m_DephSensevity)
            {
                triggerPoints.Add(screenPoint);
            }
        }

        return triggerPoints;
    }

    private Texture2D CreateTexture(List<ValidPoint> validPoints)
    {
        Texture2D _newTexture = new Texture2D(1920, 1080, TextureFormat.Alpha8, false);

        for(int x = 0; x < 1920; x++)
        {
            for (int y = 0; y < 1080; y++)
            {
                _newTexture.SetPixel(x, y, Color.clear);
            }
        }

        foreach(ValidPoint point in validPoints)
        {
            _newTexture.SetPixel((int)point.colorSpace.X, (int)point.colorSpace.Y, Color.black);
        }

        _newTexture.Apply();

        return _newTexture;
    }


    private Rect CreateRect(List<ValidPoint> points)
    {
        if (points.Count == 0)
            return new Rect();

        Vector2 topLeft = GetTopLeft(points);
        Vector2 bottomRigth = GetBottomRight(points);

        Vector2 screenTopLeft = ScreenToCamera(topLeft);
        Vector2 screenBottomRigth = ScreenToCamera(bottomRigth);

        int width = (int)(screenBottomRigth.x - screenTopLeft.x);
        int heigth = (int)(screenBottomRigth.y - screenTopLeft.y);

        Vector2 size = new Vector2(width, heigth);
        Rect rect = new Rect(screenTopLeft, size);

        return rect;
    }

    private Vector2 GetTopLeft(List<ValidPoint> points)
    {
        Vector2 topLeft = new Vector2(int.MaxValue, int.MaxValue);

        foreach (ValidPoint point in points)
        {
            if (point.colorSpace.X < topLeft.x)
                topLeft.x = point.colorSpace.X;

            if (point.colorSpace.Y < topLeft.y)
                topLeft.y = point.colorSpace.Y;
        }
        return topLeft;
    }

    private Vector2 GetBottomRight(List<ValidPoint> points)
    {
        Vector2 bottomRigth = new Vector2(int.MinValue, int.MinValue);

        foreach (ValidPoint point in points)
        {
            if (point.colorSpace.X > bottomRigth.x)
                bottomRigth.x = point.colorSpace.X;

            if (point.colorSpace.Y > bottomRigth.y)
                bottomRigth.y = point.colorSpace.Y;
        }
        return bottomRigth;
    }

    private Vector2 ScreenToCamera(Vector2 screenPosition)
    {
        Vector2 normalizeScreen = new Vector2(Mathf.InverseLerp(0, 1920, screenPosition.x), Mathf.InverseLerp(0, 1080, screenPosition.y));

        Vector2 screenPoint = new Vector2(normalizeScreen.x * m_Camera.pixelWidth, normalizeScreen.y * m_Camera.pixelHeight);

        return screenPoint;
    }
}

public class ValidPoint
{
    public ColorSpacePoint colorSpace;
    public float z = 0.0f;

    public bool m_WithinWallDepth = false;

    public ValidPoint(ColorSpacePoint newColorSpace, float newZ)
    {
        colorSpace = newColorSpace;
        z = newZ;
    }
}
