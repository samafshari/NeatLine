using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class NeatLine : MonoBehaviour
{
    bool isDirty = false;
    readonly Vector2[] points = new Vector2[2];

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Material material;

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

    float _thickness = 0.1f;
    public float Thickness
    {
        get => _thickness;
        set
        {
            if (value > 0)
            {
                _thickness = value;
            }
            else
            {
                _thickness = 0.1f;
            }
            isDirty = true;
        }
    }

    Color _color = Color.white;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            isDirty = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        material = GetComponent<Material>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null) meshFilter = gameObject.AddComponent<MeshFilter>();
        if (meshRenderer == null) meshRenderer = gameObject.AddComponent<MeshRenderer>();
        if (material != null)
        {
            meshRenderer.material = material;
        }
        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirty) Rebuild();
    }

    void Rebuild()
    {
        isDirty = false;

        var mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = GetPolygon();

        Triangulator tr = new Triangulator(mesh.vertices);
        mesh.triangles = tr.Triangulate();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    Vector3[] GetPolygon()
    {
        return new[]
        {
            new Vector3(HeadLocalPosition.x, HeadLocalPosition.y),
            new Vector3(HeadLocalPosition.x, HeadLocalPosition.y) + Vector3.up,
            new Vector3(TailLocalPosition.x, TailLocalPosition.y),
            new Vector3(TailLocalPosition.x, TailLocalPosition.y) + Vector3.up
        };
    }
}
