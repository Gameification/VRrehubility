using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public MesureDepth mMesureDepth;
    public MultiSourceManager mMultiSourceManager;
    public RawImage mRawImage;
    public RawImage mRawDepth;
    void Update()
    {
        mRawImage.texture = mMultiSourceManager.GetColorTexture();
        mRawDepth.texture = mMesureDepth.mDepthTexture;
    }
}
