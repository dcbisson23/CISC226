using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearActivator : MonoBehaviour
{
    public Collider2D playerCollider;
    public bool isOnline;
    // Start is called before the first frame update
    void Start()
    {
        isOnline = false;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other == playerCollider && Input.GetAxis("Vertical") <= -0.05f)
        {
            isOnline = true;
        }
    }
}
