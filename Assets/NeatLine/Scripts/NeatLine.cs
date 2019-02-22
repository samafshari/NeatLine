using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
[RequireComponent(typeof(Material))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class NeatLine : MonoBehaviour
{
    bool isDirty = false;
    [HideInInspector]
    public Vector2[] points = new Vector2[2]
    {
        new Vector2(-0.5f, -0.5f),
        new Vector2(0.5f, 0.5f)
    };

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

    [HideInInspector] public float _thickness = 0.1f;
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

    [HideInInspector] public Color _color = Color.white;
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

        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        if (material == null)
        {
            material = Instantiate(Resources.Load<Material>("NeatLineMaterial"));
        }
        meshRenderer.material = material;

        isDirty = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirty)
        {
            isDirty = false;
            Rebuild();
        }
    }

    void Rebuild()
    {
        var mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = GetPolygon();
        mesh.triangles = new[] { 0, 1, 2, 1, 2, 3 };
        mesh.colors = new[] { Color, Color, Color, Color }; 
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    Vector3[] GetPolygon()
    {
        var vec = TailLocalPosition - HeadLocalPosition;
        var unit = vec.normalized;
        var halfCross = new Vector3(unit.y, -unit.x, 0) * 0.5f * Thickness;

        return new[]
        {
            new Vector3(HeadLocalPosition.x, HeadLocalPosition.y) - halfCross,
            new Vector3(HeadLocalPosition.x, HeadLocalPosition.y) + halfCross,
            new Vector3(TailLocalPosition.x, TailLocalPosition.y) - halfCross,
            new Vector3(TailLocalPosition.x, TailLocalPosition.y) + halfCross
        };
    }

    private void OnDrawGizmos()
    {
        if (meshRenderer == null) return;
        Gizmos.color = Color.clear;
        Gizmos.DrawCube(meshRenderer.bounds.center, meshRenderer.bounds.size + Vector3.forward);
    }
}
