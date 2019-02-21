using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatLine : MonoBehaviour
{
    public Vector2 HeadLocalPosition
    {
        get => points[0];
        set => points[0] = value;
    }

    public Vector2 TailLocalPosition
    {
        get => points[1];
        set => points[1] = value;
    }
    
    public float Thickness = 0.1f;
    public Color Color = Color.white;

    Vector2[] points = new Vector2[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
