using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHand : MonoBehaviour
{
    public float GameTime = 1;
    public bool LeftHandState = false;
    public bool RigthHandState = false;

    public delegate void OnGameTimeChange(float newVal);
    public event OnGameTimeChange GameTimeChange;

    public delegate void OnLeftHandStateChange(bool newVal);
    public event OnLeftHandStateChange LeftHandStateChange;

    public delegate void OnRigthHandStateChange(bool newVal);
    public event OnRigthHandStateChange RigthHandStateChange;




    public float GameTimeD
    {
        get
        {
            return GameTime;
        }
        set
        {
            if (GameTime == value) return;
            GameTime = value;
            GameTimeChange?.Invoke(GameTime);
        }
    }

    public bool LeftHandStateD
    {
        get
        {
            return LeftHandState;
        }
        set
        {
            if (LeftHandState == value) return;
            LeftHandState = value;
            LeftHandStateChange?.Invoke(LeftHandState);
        }
    }

    public bool RigthHandStateD
    {
        get
        {
            return RigthHandState;
        }
        set
        {
            if (RigthHandState == value) return;
            RigthHandState = value;
            RigthHandStateChange?.Invoke(RigthHandState);
        }
    }
}
