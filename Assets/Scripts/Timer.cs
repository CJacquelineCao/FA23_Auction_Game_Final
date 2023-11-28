using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float elapsedTime;
    public float maxTime;
    public float t;

   public bool timerStarted;

    public AllBidItemsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        t = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted == true)
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

    }
    public void resetTimer()
    {
        t = maxTime;
        elapsedTime = 0;
    }
}
