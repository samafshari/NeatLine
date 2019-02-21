using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeatLine : MonoBehaviour
{
    bool isDirty = false;
    readonly Vector2[] points = new Vector2[2];

    public Vector2 HeadLocalPosition
    {
        get => points[0];
        set
        {
            points[0] = value;
            isDirty = true;
        }
    }

    public Vector2 TailLocalPosition
    {
        get => points[1];
        set
        {
            points[1] = value;
            isDirty = true;
        }
    }
    
    public float Thickness = 0.1f;
    public Color Color = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirty) Rebuild();
    }

    void Rebuild()
    {
        isDirty = false;
    }
}
