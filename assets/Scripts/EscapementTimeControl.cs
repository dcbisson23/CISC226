using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapementTimeControl : MonoBehaviour
{
    public float baseSpeed;
    public float stopDegreeThreshold;
    private float baseDirection;
    private Rigidbody2D rb2d;
    private TimeControllerScript timeDilator;
    public float degreeCounter;
    public bool isStopped;
    private float counterDirection;

    public bool fastFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timeDilator = GameObject.Find("TimeController").GetComponent<TimeControllerScript>();
        
        isStopped = true;
        baseDirection = Mathf.Sign(baseSpeed);
        counterDirection = 1;
        degreeCounter = 0;
    }


    // Update is called once per frame
    void Update()
    {
        float timeDilation = timeDilator.timeDilation;
        float angVel = baseSpeed * timeDilation;
        if (Mathf.Abs(angVel) * Time.fixedDeltaTime >= stopDegreeThreshold / 2)
        {
            rb2d.angularVelocity = angVel;
            fastFlag = true;
            return;
        }
        fastFlag = false;
        degreeCounter += angVel * baseDirection * counterDirection * Time.deltaTime;
        if (degreeCounter >= stopDegreeThreshold || degreeCounter < 0)
        {
            isStopped = !isStopped;
            counterDirection = Mathf.Sign(timeDilation);
            degreeCounter = Mathf.Abs(degreeCounter % stopDegreeThreshold);
            angVel -= degreeCounter * counterDirection;
        }
        if (isStopped == false)
        {
            rb2d.angularVelocity = angVel;
        }
        else
        {
            rb2d.angularVelocity = 0;
        }
    }
}
