using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static float currentTime = 0.0f;
    public static float notcurrentTime = 0.0f;
    public int needCurrentTime;
    public int needNotCurrentTime;
    public GameObject TryAgain;
    private float time = 0.0f;

    private bool IsPause = false;
    public GameObject MenuGood;

    void Update(){

        if(IsPause){
            currentTime = 0.0f;
            notcurrentTime = 0.0f;
            time += Time.deltaTime;
            if(time > 3) {
                time = 0.0f;
                notcurrentTime = 0.0f;
                IsPause = false;
                TryAgain.SetActive(false);
            }
        }
    } 
    private void OnTriggerStay2D(Collider2D other)
    {

        if(other.gameObject.name == SelectPart.currentPart)
        {
            currentTime += Time.deltaTime;

            notcurrentTime = 0.0f;
            if(currentTime > needCurrentTime)
            {
                currentTime = 0.0f;
                MenuGood.SetActive(true);
                SelectPart.finish = true;
            }
        }else{
            notcurrentTime += Time.deltaTime;
            if(notcurrentTime > needNotCurrentTime)
            {
                notcurrentTime = 0.0f;
                SelectPart.mistakes += 1;
                IsPause = true;
                TryAgain.SetActive(true);


            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name != SelectPart.currentPart){
            notcurrentTime = 0.0f;
        }
    }
}
