using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float baseSpeed;
    public GearActivator pSwitch;
    private Rigidbody2D rb2d;
    private TimeControllerScript timeDilator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timeDilator = GameObject.Find("TimeController").GetComponent<TimeControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeDilation = 0;
        if (pSwitch != null && pSwitch.isOnline || pSwitch == null)
        {
                timeDilation = timeDilator.timeDilation;
        }

        rb2d.angularVelocity = baseSpeed * timeDilation;

    }
}
