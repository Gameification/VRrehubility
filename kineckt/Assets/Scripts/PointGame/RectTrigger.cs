using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTrigger : MonoBehaviour
{
    [Range(0, 1000)]
    public int mSensevity = 5;

    public bool mIsTriggered = false;

    private Camera mCamera = null;

    private RectTransform mRectTransform = null;
    private Image mImage = null;

    private void Awake()
    {
       Control_Depth.OnTriggerPoints += OnTriggerPoints;

        mCamera = Camera.main;
        mRectTransform = GetComponent<RectTransform>();
        mImage = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        Control_Depth.OnTriggerPoints -= OnTriggerPoints;
    }

    private void OnTriggerPoints(List<Vector2> triggerPoints)
    {
        if (!enabled)
            return;

        int count = 0;

        foreach (Vector2 point in triggerPoints)
        {
            Vector2 flippedY = new Vector2(point.x,  point.y);
       
            if (RectTransformUtility.RectangleContainsScreenPoint(mRectTransform, flippedY))
                count++;
        }
        
        Debug.Log(count);

        if (count > mSensevity)
        {

            mIsTriggered = true;
            mImage.color = Color.red;
        }
        else
        {
            mIsTriggered = false;
            mImage.color = Color.gray;
        }
    }
}
