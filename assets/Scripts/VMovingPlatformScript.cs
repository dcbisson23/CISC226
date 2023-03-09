using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMovingPlatformScript : TimeControlled
{

    public int direction = 1;

    private void Start() {
        speedMultiplier = 1;
    } 
    public override void TimeUpdate()
    {
    
        velocity.y = speedMultiplier * direction;

        Vector2 pos = transform.position;
        pos.y += velocity.y * Time.deltaTime;
        transform.position = pos;

        if (pos.y >= 7){direction = -1;}
        if (pos.y <= 2){direction = 1;}
   }
}
