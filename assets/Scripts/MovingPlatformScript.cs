using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : TimeControlled
{
    private int direction = 1; 
    public override void TimeUpdate()
   {
        if (Input.GetKey(KeyCode.RightArrow)){velocity.x = 3 * direction;}

        else{velocity.x = 1 * direction;}

        Vector2 pos = transform.position;
        pos.x += velocity.x * Time.deltaTime;
        transform.position = pos;

        if (pos.x >= 16){direction = -1;}
        if (pos.x <= 0.5){direction = 1;}
   }
}
