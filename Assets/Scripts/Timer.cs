using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float elapsedTime;
    public float maxTime;
    public float t;

   public bool timerStarted;

    public AllBidItemsManager manager;
    public Slider timer;
    // Start is called before the first frame update
    void Start()
    {
        t = maxTime;
    }
    void SliderSetting()
    {
        timer.minValue = 0;
        timer.maxValue = maxTime;
        timer.value = t;

    }
    // Update is called once per frame
    void Update()
    {
        if (manager.timesBid <= 2)
        {
            maxTime = 5;

        }
        else if (manager.timesBid > 2 && manager.timesBid <= 4)
        {
            maxTime = 4;
        }
        else if(manager.timesBid > 4 && manager.timesBid <= 6)
        {
            maxTime = 3;
        }
        else if(manager.timesBid > 6 && manager.timesBid <= 8)
        {
            maxTime = 2;
        }
        else
        {
            maxTime = 1;
        }
        if (timerStarted == true)
        {
            elapsedTime += Time.deltaTime;
            if (t <= 0)
            {
                //roundOver
                Debug.Log("RoundEnded");
                manager.StartCoroutine(manager.soldRoutine());
            }
            else
            {
                t = maxTime - elapsedTime;
            }
        }
        SliderSetting();

    }
    public void resetTimer()
    {
        t = maxTime;
        elapsedTime = 0;

    }
}
