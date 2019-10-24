using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class HandALL : MonoBehaviour
{
    float currCountdownValue;
    public static float RtimeLeft = 1.5f, LtimeLeft = 1.5f;
    public static float RprogressBar = 0, LprogressBar = 0;
    public static Image RprogressBarIm, LprogressBarIm;
    public static bool rCheck = false, lCheck = false;
    public GameObject HandR, HandL;
    public GameObject HANDPOSR, HANDPOSL;

    // Точность
    public GameObject lBubble, rBubble;
    public float rlong, llong;
    private float L_TimeToComb, R_TimeToComb;

    private int count;
    public bool screenon = false;
    public bool checkScreenshot = false;
    public Camera captureCamera;
    private int previewRAND = 0;
    public int pos = 0;
    private int POSnum1 = 0;
    private int POSnum2 = 0;
    private int POSnum3 = 0;
    private int POSnum4 = 0;
    private int POSnum5 = 0;
    //static pos
    public List<Vector2> LPoinList;
    public List<Vector2> RPoinList;

    // Start is called before the first frame update
    void Start()
    {
        LPoinList = new List<Vector2>(5) { new Vector2(-103, 84), new Vector2(-200, 84), new Vector2(-227, 0), new Vector2(-170, -80), new Vector2(-102, -109) };
        RPoinList = new List<Vector2>(5) { new Vector2(220, -50), new Vector2(232, -5), new Vector2(116, -50), new Vector2(116, 81), new Vector2(249, -8) };
        RprogressBarIm = HandR.transform.GetChild(0).GetComponent<Image>();
        LprogressBarIm = HandL.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(HandL.transform.position, lBubble.transform.position));
        //Debug.Log(Vector2.Distance(HandR.transform.position, rBubble.transform.position));

        if (lCheck == false)
        {
            L_TimeToComb += Time.deltaTime;
        }

        if (rCheck == false)
        {
            R_TimeToComb += Time.deltaTime;
        }

        if (CheckL.CheckLH)
        {
            LtimeLeft -= Time.deltaTime;
            LprogressBar += Time.deltaTime;
            LprogressBarIm.fillAmount = LprogressBar;
            if (LtimeLeft < 0)
            {
                lCheck = true;
                LtimeLeft = 1.5f;
            }
        }

        if (CheckR.CheckRH)
        {
            RtimeLeft -= Time.deltaTime;
            RprogressBar += Time.deltaTime;
            RprogressBarIm.fillAmount = RprogressBar;
            if (RtimeLeft < 0)
            {
                rCheck = true;
                RtimeLeft = 1.5f;
            }
        }

        if(lCheck && rCheck && !screenon)
        {
            Save();
            screenon = true;
        }

        if (lCheck == true && rCheck == true && screenon)
        {
            {
                rlong = Vector2.Distance(HandR.transform.position, rBubble.transform.position);
                llong = Vector2.Distance(HandL.transform.position, lBubble.transform.position);

                while (pos == previewRAND)
                {
                    pos = (int)Random.Range(0, 5);
                    
                }
                
                previewRAND = pos;
                HANDPOSR.transform.localPosition = RPoinList[pos];
                HANDPOSL.transform.localPosition = LPoinList[pos];
                RprogressBarIm.fillAmount = RprogressBar = LprogressBarIm.fillAmount = LprogressBar = 0;
                //cameratransform
                
                // Точность
                if ((rlong > 1.15) && (llong > 1.15)) Kinect.score += 30;
                if (rlong > 1.15 && llong <= 1.15 && llong > 0.7) Kinect.score += 45;
                if (rlong > 1.15 && llong <=0.7) Kinect.score += 65;
                if (rlong <=1.15 && rlong >0.7 && llong >1.15) Kinect.score += 45;
                if (rlong <= 1.15 && rlong > 0.7 && llong <= 1.15 && llong > 0.7) Kinect.score += 60;
                if (rlong <= 1.15 && rlong > 0.7 && llong <= 0.7) Kinect.score += 80;
                if (rlong <= 0.7 && llong > 1.15) Kinect.score += 65;
                if (rlong <= 0.7 && llong <= 1.15 && llong > 0.7) Kinect.score += 80;
                if (rlong <= 0.7 && llong <= 0.7) Kinect.score += 100;

                //Debug.Log("Left Time: " + L_TimeToComb);
                //Debug.Log("Right Time: " + R_TimeToComb);
                L_TimeToComb = R_TimeToComb = 0;

                screenon = false;
                SaveTrial.checkScreenshot = false;
                lCheck = false;
                rCheck = false;
            }
        }
    }

    
    public void Save()
    {
        int width = this.captureCamera.pixelWidth;
        int height = this.captureCamera.pixelHeight;
        Texture2D texture = new Texture2D(width, height);

        RenderTexture targetTexture = RenderTexture.GetTemporary(width, height);

        this.captureCamera.targetTexture = targetTexture;
        this.captureCamera.Render();

        RenderTexture.active = targetTexture;

        Rect rect = new Rect(0, 0, width, height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        Object.Destroy(texture);

        if (pos == 0) { File.WriteAllBytes(Application.dataPath + "/Gallery/pos1/SavedScreen" + POSnum1 +".png", bytes); POSnum1++; }
        if (pos == 1) { File.WriteAllBytes(Application.dataPath + "/Gallery/pos2/SavedScreen" + POSnum2 +".png", bytes); POSnum2++; }
        if (pos == 2) { File.WriteAllBytes(Application.dataPath + "/Gallery/pos3/SavedScreen3.png", bytes); POSnum3++; }
        if (pos == 3) { File.WriteAllBytes(Application.dataPath + "/Gallery/pos4/SavedScreen4.png", bytes); POSnum4++; }
        if (pos == 4) { File.WriteAllBytes(Application.dataPath + "/Gallery/pos5/SavedScreen5.png", bytes); POSnum5++; }
    }
}
