using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeWaiting = 15.0f;
    private bool timeRunning = true;
    public GameObject customer;

    private void Start()
    {
        //timeRunning = true;
    }

    private void Update()
    {
        //timeCountDown();
    }

    private void timeCountDown()
    {
        if (timeRunning)
        {
            if (timeWaiting > 0)
            {
                timeWaiting -= Time.deltaTime;
            }

            else
            {
                timeWaiting = 0;
                timeRunning = false;
                Debug.Log("End");
            }
        }
    }

    private void changeEmotion()
    {
        if (timeWaiting < 5)
        {
            Spawning.instance.angryWaiting(customer);
        }
    }
}
