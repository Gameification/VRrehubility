using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public float GameTime = 2;
    public float RepeateTime = 2;

    public delegate void OnGameTimeChange(float newVal);
    public event OnGameTimeChange GameTimeChange;

    public delegate void OnRepeateTimCehange(float newVal);
    public event OnRepeateTimCehange RepeateTimCehange;

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

    public float RepeateTimeD
    {
        get
        {
            return RepeateTime;
        }
        set
        {
            if (RepeateTime == value) return;
            RepeateTime = value;
            RepeateTimCehange?.Invoke(RepeateTime);
        }
    }
}
