using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorColorChange : MonoBehaviour
{
    public GearActivator pSwitch;
    public Color baseColor = Color.clear;
    public Color activeColor;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Color", baseColor);

    }

    // Update is called once per frame
    void Update()
    {
        if (pSwitch.isOnline)
        {
            material.SetColor("_Color", activeColor);
        }
    }
}
