using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsEvent : MonoBehaviour
{
    public float GameTime = 120f;

    public delegate void OnGameTimeChange(float newVal);
    public event OnGameTimeChange GameTimeChange;

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
}
