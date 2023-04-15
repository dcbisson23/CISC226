using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTextHider : MonoBehaviour
{
    private Renderer renderinator;
    public GearActivator pSwitch;

    // Start is called before the first frame update
    void Start()
    {
        renderinator = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderinator.enabled = pSwitch.isOnline;
    }
}
