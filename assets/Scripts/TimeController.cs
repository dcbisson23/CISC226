using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{   
    public static float gravity = -100;

    public struct RecordedData
    {
        public Vector2 pos;
        public Vector2 vel;
    }

    RecordedData[,] recordedData; 
    int recordMax = 100000; 
    int recordCount; 
    int recordIndex; 
    bool wasRewinding = false;

    TimeControlled[] timeObjects;

    private void Awake() 
    {
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();
        recordedData = new RecordedData[timeObjects.Length,recordMax];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool pause = Input.GetKey(KeyCode.UpArrow);
        bool fforward = Input.GetKey(KeyCode.RightArrow);
        bool rewind = Input.GetKey(KeyCode.LeftArrow);

        if (rewind) 
        {   
            wasRewinding = true;

            if (recordIndex > 0)
            {
                recordIndex --; 

                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel; 
                }
            }
        }
        else if (pause && fforward) 
        {
            wasRewinding = true;

            if(recordIndex < recordCount -1)
            {
                recordIndex++;

                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel; 
                }
            }
        }
        else if (!pause && !rewind)
        {   
            if (wasRewinding)
            {
                recordCount = recordIndex;
                wasRewinding = false;
            }

            for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
            {
                TimeControlled timeObject = timeObjects[objectIndex];
                RecordedData data = new RecordedData();
                data.pos = timeObject.transform.position;
                data.vel = timeObject.velocity;
                recordedData[objectIndex, recordCount] = data;
            }
            recordCount++;
            recordIndex = recordCount; 

            foreach(TimeControlled timeObject in timeObjects)
            {
                timeObject.TimeUpdate();
            }
        }

        }

    
}
