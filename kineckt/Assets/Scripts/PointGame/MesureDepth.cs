using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class MesureDepth : MonoBehaviour
{
    public delegate void NewTriggerPoints(List<Vector2> triggerPoints);
    public static event NewTriggerPoints OnTriggerPoints = null;

    public MultiSourceManager mMultiSource;
    public Texture2D mDepthTexture;

    //CutOffs
    [Range(0, 0.1f)]
    public float mDepthSensevity = 1;
    [Range(-10, 10f)]
    public float mWallDepth = -10;

    [Header("Top and Bottom")]
    [Range(-1, 1f)]
    public float mTopCutOff = 1;
    [Range(-1, 1f)]
    public float mBottomCutOff = -1;

    [Header("Left and Rigth")]
    [Range(-1, 1f)]
    public float mLeftCutOff = -1;
    [Range(-1, 1f)]
    public float mRigthCutOff = 1;

    //Depth
    private ushort[] mDepthData = null;
    private CameraSpacePoint[] mCameraSapcePoints = null;
    private ColorSpacePoint[] mColorSpacePoints = null;
    private List<ValidPoint> mValidPoints = null;
    private List<Vector2> mTriggerPoints = null;

    //Kinect
    private KinectSensor mSenor = null;
    private CoordinateMapper mMapper = null;
    private Camera mCamera = null;

    private readonly Vector2Int mDepthResolution = new Vector2Int(512, 424);
    private Rect mRect;
    private void Awake()
    {
        mSenor = KinectSensor.GetDefault();
        mMapper = mSenor.CoordinateMapper;
        mCamera = Camera.main;

        int arraySize = mDepthResolution.x * mDepthResolution.y;

        mCameraSapcePoints = new CameraSpacePoint[arraySize];
        mColorSpacePoints = new ColorSpacePoint[arraySize];
    }

    private void Update()
    {
        mValidPoints = DepthToColor();

        mTriggerPoints = FillterToTrigger(mValidPoints);

        if (OnTriggerPoints != null && mTriggerPoints.Count != 0)
            OnTriggerPoints(mTriggerPoints);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mRect = CreateRect(mValidPoints);
            mDepthTexture = CreateTexture(mValidPoints);
        }
    }

    private void OnGUI()
    {
       // GUI.Box(mRect, "");

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
        List<ValidPoint> ValidPoints = new List<ValidPoint>();

        //Get depth
        mDepthData = mMultiSource.GetDepthData();

        mMapper.MapDepthFrameToCameraSpace(mDepthData, mCameraSapcePoints);
        mMapper.MapDepthFrameToColorSpace(mDepthData, mColorSpacePoints);

        //Filter
        for(int i = 0; i<mDepthResolution.x / 8; i++)
        {
            for (int j = 0; j < mDepthResolution.y / 8; j++)
            {
                int sampleIndex = (j * mDepthResolution.x) + i;
                sampleIndex *= 8;

                //CutoffTests
                if (mCameraSapcePoints[sampleIndex].X < mLeftCutOff)
                    continue;
                if (mCameraSapcePoints[sampleIndex].X > mRigthCutOff)
                    continue;
                if (mCameraSapcePoints[sampleIndex].Y > mTopCutOff)
                    continue;
                if (mCameraSapcePoints[sampleIndex].Y < mBottomCutOff)
                    continue;

                ValidPoint newPoint = new ValidPoint(mColorSpacePoints[sampleIndex], mCameraSapcePoints[sampleIndex].Z);

                if (mCameraSapcePoints[sampleIndex].Z >= mWallDepth)
                    newPoint.mWithinWallDepth = true;

                ValidPoints.Add(newPoint);
            }
        }
        return ValidPoints;
    }
    private List<Vector2> FillterToTrigger (List<ValidPoint> validPoints)
    {
        List<Vector2> triggerPoints = new List<Vector2>();
        
        foreach(ValidPoint point in validPoints)
        {
            if (!point.mWithinWallDepth)
            {
                if (point.z < mWallDepth * mDepthSensevity)
                {
                    Vector2 screenPoint = ScreenToCamera(new Vector2(point.colorSpace.X, point.colorSpace.Y));

                    triggerPoints.Add(screenPoint);
                }
            }
        }
        return triggerPoints;
    }

    private Texture2D CreateTexture(List<ValidPoint> validPoints)
    {
        Texture2D newTexture = new Texture2D(1920, 1080, TextureFormat.Alpha8, false);

        for(int x = 0; x < 1920; x++)
        {
            for(int y = 0; y < 1080; y++)
            {
                newTexture.SetPixel(x, y, Color.clear);
            }
        }

        foreach (ValidPoint point in validPoints)
        {
            newTexture.SetPixel((int)point.colorSpace.X, (int)point.colorSpace.Y, Color.black);
        }

        newTexture.Apply();

        return newTexture;
    }

    #region RectCreate

    private Rect CreateRect(List<ValidPoint> points)
    {
        if (points.Count == 0)
            return new Rect();

        //Get corners of rect
        Vector2 topLeft = GetTopLeft(points);
        Vector2 bottomRigth = GetBottomRigth(points);

        //Translate to viewport

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

        foreach(ValidPoint point in points)
        {
            if (point.colorSpace.X < topLeft.x)
                topLeft.x = point.colorSpace.X;

            if (point.colorSpace.Y < topLeft.y)
                topLeft.y = point.colorSpace.Y;
        }

        return topLeft;
    }
    private Vector2 GetBottomRigth(List<ValidPoint> points)
    {
        Vector2 bottomRigth = new Vector2(int.MinValue, int.MinValue);

        foreach (ValidPoint point in points)
        {
            if (point.colorSpace.X < bottomRigth.x)
                bottomRigth.x = point.colorSpace.X;

            if (point.colorSpace.Y < bottomRigth.y)
                bottomRigth.y = point.colorSpace.Y;
        }

        return bottomRigth;
    }
    private Vector2 ScreenToCamera(Vector2 screenPosition)
    {
        Vector2 normalizedScreen = new Vector2(Mathf.InverseLerp(0, 1920, screenPosition.x), Mathf.InverseLerp(0, 1080, screenPosition.y));

        Vector2 screenPoint = new Vector2(normalizedScreen.x * mCamera.pixelWidth, normalizedScreen.y * mCamera.pixelHeight);

        return screenPoint;
    }
    #endregion
}

public class ValidPoint
{
    public ColorSpacePoint colorSpace;
    public float z = 0.0f;

    public bool mWithinWallDepth = false;

    public ValidPoint(ColorSpacePoint newColorSpace, float newZ)
    {
        colorSpace = newColorSpace;
        z = newZ;
    }
}