using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageView : MonoBehaviour
{
    public Control_Depth m_Control_Depth;
    public MultiSourceManager m_MultiSource;

    public RawImage m_RawImage;
    public RawImage m_RawDepth;
    void Update()
    {
        m_RawImage.texture = m_MultiSource.GetColorTexture();

        m_RawDepth.texture = m_Control_Depth.m_DepthTexture;
    }
}
