using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class LinesDrawer : MonoBehaviour
{
    private const string _blackColorSaved = "RGBA(0.000, 0.000, 0.000, 1.000)";

    private Line _currentLine;

    private List<Line> _lines = new();

    public GameObject LinePrefab;

    private void Start()
    {
        _lines = new();
        //AppGlobal.OnRestartGlobalEvent += Clear;
        SaveLoadSystem.OnSaveGlobalEvent += OnSave;
        SaveLoadSystem.OnLoadGlobalEvent += OnLoad;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }

        if (_currentLine != null)
        {
            Draw();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
    }

    private void BeginDraw()
    {
        _currentLine = Instantiate(LinePrefab, this.transform).GetComponent<Line>();
    }

    private void Draw()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _currentLine.AddPoint(position);
    }

    public void EndDraw()
    {
        if (_currentLine != null)
        {
            if (_currentLine.PointCount == 1)
            {
                Destroy(_currentLine.gameObject);
            }
            else
            {
                _lines.Add(_currentLine);
            }
            _currentLine = null;
        }
    }

    public void Undo()
    {
        if (_lines.Count != 0)
        {
            var lastLine = _lines.Last();
            _lines.Remove(lastLine);
            Destroy(lastLine.gameObject);
        }
    }

    private void Clear()
    {
        foreach(var line in _lines)
        {
            Destroy(line.gameObject);
        }
        _lines.Clear();
    }

    private void OnSave(SaveLoadSystem _)
    {
        foreach (var renderedLine in _lines)
        {
            if (renderedLine.PointCount > 1)
            {
                var line = new InputLine
                {
                    Color = renderedLine.Renderer.startColor.ToString(),
                    Points = renderedLine.Points.Select(x => new LinePoint { X = x.x, Y = x.y })
                    .ToList(),
                };
                SaveLoadSystem.LoadSaveObject.InputLines.Add(line);
            }
        }
    }

    private void OnLoad(SaveLoadSystem _)
    {
        Clear();

        var lines = SaveLoadSystem.LoadSaveObject.InputLines;
        foreach (var loadLine in lines)
        {
            var renderLine = Instantiate(LinePrefab, this.transform).GetComponent<Line>();

            renderLine.SetColorRed();
            if (loadLine.Color == _blackColorSaved)
            {
                renderLine.SetColorBlack();
            }

            foreach (var point in loadLine.Points)
            {
                var v3 = new Vector3 { x = point.X, y = point.Y, z = 0 };
                renderLine.AddPoint(v3);
            }
            _lines.Add(renderLine);
        }
    }
}
