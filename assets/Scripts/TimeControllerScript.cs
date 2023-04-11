using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerScript : MonoBehaviour
{

    public float timeDilation = 1;
    public float deltaTimeDilation = 0;

    private static List<float> locks = new List<float>() {-2f, -1f, -0.5f, 0f, 0.5f, 1f, 2f};
    private static float delta2TimeDilation = 0.01f;
    private static float maxDeltaTimeDilation = 5f;
    private static float maxTimeDilation = 100f;
    private static float lockWindow = 0.25f;
    private static float lockWidth = 0.1f;
    private static float delta2Deceleration = 0.1f;
    private float targetDeltaTimeDilation = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTimeDirection = Input.GetAxis("Fire1");

        float isDecelerating = 0;
        if (Mathf.Abs(deltaTimeDirection) <=0.05f)
        {
            targetDeltaTimeDilation = 0;
            isDecelerating = 1;
        }
        if (deltaTimeDirection < -0.05 && timeDilation >= -maxTimeDilation)
        {
            targetDeltaTimeDilation = -maxDeltaTimeDilation;
        }
        else if (deltaTimeDirection > 0.05 && timeDilation <= maxTimeDilation)
        {
            targetDeltaTimeDilation = maxDeltaTimeDilation;
        }

        deltaTimeDilation = Mathf.Clamp(deltaTimeDilation, -maxDeltaTimeDilation, maxDeltaTimeDilation);
        if (Mathf.Sign(targetDeltaTimeDilation) != Mathf.Sign(deltaTimeDilation))
        {
            isDecelerating = 1;
        }

        if (deltaTimeDilation != targetDeltaTimeDilation)
        {
            deltaTimeDilation = Mathf.MoveTowards(deltaTimeDilation, targetDeltaTimeDilation, delta2TimeDilation + isDecelerating * delta2Deceleration);
        }
        timeDilation = Mathf.Clamp(timeDilation + deltaTimeDilation * Time.deltaTime, -maxTimeDilation, maxTimeDilation);


    }
}