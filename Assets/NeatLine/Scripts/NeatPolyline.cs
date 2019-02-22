using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
[System.Serializable]
[RequireComponent(typeof(Material))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class NeatPolyline : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Material material;

    public bool MakeDirty() => isDirty = true;

    bool isDirty = false;

    [HideInInspector]
    public Vector2[] _points = new Vector2[2]
    {
        new Vector2(-0.5f, -0.5f),
        new Vector2(0.5f, 0.5f)
    };

    [HideInInspector]
    public float[] _thicknesses = new float[] { 0.1f, 0.1f };

    [HideInInspector]
    public Color[] _colors = new Color[] { Color.white, Color.white };

    [HideInInspector]
    public float _thicknessMultiplier = 1.0f;

    public float ThicknessMultiplier
    {
        get => _thicknessMultiplier;
        set
        {
            _thicknessMultiplier = value;
            isDirty = true;
        }
    }

    public void SetVector(int index, Vector2 value)
    {
        _points[index] = value;
        isDirty = true;
    }

    public void SetThickness(int index, float value)
    {
        _thicknesses[index] = value;
        isDirty = true;
    }

    public void SetColor(int index, Color value)
    {
        _colors[index] = value;
        isDirty = true;
    }

    public void RemoveLast()
    {
        RemoveAt(_points.Length - 1);
    }

    public void RemoveAt(int index)
    {
        if (_points.Length < index)
            throw new System.IndexOutOfRangeException("Not enough points to delete.");

        if (_points.Length == 2)
            throw new System.IndexOutOfRangeException("Too few points left.");

        var newPoints = new Vector2[_points.Length - 1];
        var newThicknesses = new float[_points.Length - 1];
        var newColors = new Color[_points.Length - 1];
        int shift = 0;
        for (int i = 0; i < _points.Length; i++)
        {
            if (i == index)
            {
                shift--;
                continue;
            }
            newPoints[i + shift] = _points[i];
            newThicknesses[i + shift] = _thicknesses[i];
            newColors[i + shift] = _colors[i];
        }
        _points = newPoints;
        _thicknesses = newThicknesses;
        _colors = newColors;
        isDirty = true;
    }

    public void Add()
    {
        Add(_points.Last() + Vector2.one);
    }

    public void Add(Vector2 point, float? thickness = null, Color? color = null)
    {
        float newThickness = thickness ?? _thicknesses.Last();
        Color newColor = color ?? _colors.Last();

        var newPoints = new Vector2[_points.Length + 1];
        var newThicknesses = new float[_points.Length + 1];
        var newColors = new Color[_points.Length + 1];
        for (int i = 0; i < _points.Length; i++)
        {
            newPoints[i] = _points[i];
            newThicknesses[i] = _thicknesses[i];
            newColors[i] = _colors[i];
        }

        var lastIndex = _points.Length;

        _points = newPoints;
        _thicknesses = newThicknesses;
        _colors = newColors;

        _points[lastIndex] = point;
        _thicknesses[lastIndex] = newThickness;
        _colors[lastIndex] = newColor;

        isDirty = true;
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

        var vertices = new Vector3[2 * _points.Length];
        var colors = new Color[2 * _points.Length];
        var triangles = new int[(2 * _points.Length - 2) * 3];

        int vertexIndex = 0;
        Vector3 cross = Vector3.zero;
        for (int i = 0; i < _points.Length - 1; i++)
        {
            var head = _points[i];
            var unit = (_points[i + 1] - head).normalized;
            cross = new Vector3(unit.y, -unit.x, 0) * 0.5f;
            var headCross = cross * _thicknesses[i] * ThicknessMultiplier;

            colors[vertexIndex] = _colors[i];
            vertices[vertexIndex++] = new Vector3(head.x, head.y) - headCross;

            colors[vertexIndex] = _colors[i];
            vertices[vertexIndex++] = new Vector3(head.x, head.y) + headCross;
        }

        var tail = _points[_points.Length - 1];
        var tailCross = cross * _thicknesses[_points.Length - 1] * ThicknessMultiplier;

        colors[vertexIndex] = _colors[_points.Length - 1];
        vertices[vertexIndex++] = new Vector3(tail.x, tail.y) - tailCross;

        colors[vertexIndex] = _colors[_points.Length - 1];
        vertices[vertexIndex] = new Vector3(tail.x, tail.y) + tailCross;

        mesh.vertices = vertices;
        mesh.colors = colors;

        for (int i = 0; i < triangles.Length / 3; i++)
        {
            triangles[i * 3 + 0] = i;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void OnDrawGizmos()
    {
        if (meshRenderer == null) return;
        Gizmos.color = Color.clear;
        Gizmos.DrawCube(meshRenderer.bounds.center, meshRenderer.bounds.size + Vector3.forward);
    }
}
