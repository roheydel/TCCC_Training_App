using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private readonly List<Vector2> _points = new();

    public List<Vector2> Points { get => _points; }
    public int PointCount = 0;
    public LineRenderer Renderer;

    [SerializeField]
    private float _minDistance = 0.1f;

    public void AddPoint(Vector2 point)
    {
        if (PointCount >= 1 && Vector2.Distance(point, GetLastPoint()) < _minDistance)
            return;

        _points.Add(point);
        PointCount++;
        Renderer.positionCount = PointCount;
        Renderer.SetPosition(PointCount - 1, point);
    }

    public Vector2 GetLastPoint()
    {
        return Renderer.GetPosition(PointCount - 1);
    }

    public void SetColorRed()
    {
        SetColor(Color.red);
    }

    public void SetColorBlack()
    {
        SetColor(Color.black);
    }

    private void SetColor(Color color)
    {
        Renderer.startColor = color;
        Renderer.endColor = color;
    }
}
