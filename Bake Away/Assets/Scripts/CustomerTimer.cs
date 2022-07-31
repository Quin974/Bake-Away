using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTimer : MonoBehaviour
{
    public static CustomerTimer instance;
    private float timeWaiting = 15.0f;
    private bool timeRunning = true;
    private int chancesNumb = 0;
    private int maxChances = 3;
    private Animator anim;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = this.transform.Find("Clock").GetComponent<Animator>();
        anim.enabled = true;
        //anim.Play("ClockAnim");
    }

    void Update()
    {
        if (gameObject != null)
        {
            timeCountDown();
        }
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
                Spawning.instance.angryCustomerLeaving(gameObject);
            }
        }
    }

    public void stopClock()
    {
        anim.enabled = false;
    }

    public void chancesCheck(GameObject obj)
    {
        if (chancesNumb > maxChances)
        {
            anim.enabled = false;
            Spawning.instance.angryCustomerLeaving(obj);
        }
    }

    public void newTry()
    {
        chancesNumb++;
    }
}
