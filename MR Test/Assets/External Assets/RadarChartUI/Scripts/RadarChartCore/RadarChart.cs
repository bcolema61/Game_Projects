using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RadarChart : MonoBehaviour
{
  [Header("Label Reference")]
  public Text _graphTitle;
  public RectTransform _axisLabel;
  public RectTransform _dataLabel;
  // Root for labels
  public RectTransform _labelsRoot;
  
  [Header("Radar Chart Reference")]
  public RadarChartLine _chartLine;
  public RadarChartPolygon _chartPolygon;

  [Header("Graph Parameter")]
  [SerializeField]
  [Range(3, 10)]
  public int _verticesCount = 5;

  public string _titleText = "";

  [Header("Axis Options")]
  [SerializeField]
  public List<string> _axisLabels;

  [SerializeField]
  [Tooltip("Main line")]
  public bool _drawMajorGrid = true;

  [SerializeField]
  [Range(0.01f, 1f)]
  [Tooltip("Major Line Interval")]
  public float _majorGridInterval = 0.2f;

  [SerializeField]
  [Tooltip("Line's Width")]
  [Range(0.001f, 0.03f)]
  public float _lineWidth = 0.02f;

  [Header("Data Series")]
  [SerializeField]
  [Range(0f, 1f)]
  public float _maxValue = 1f;

  [SerializeField]
  [Range(0f, 1f)]
  public float[] _valueList;

  private List<GameObject> m_gameObjectList = null;
  private int m_oldVerticesCount = 0;

  // Use this for initialization
  private void OnEnable()
  {
    m_gameObjectList = new List<GameObject>();
    _chartLine.SetParameters();
    _chartPolygon.SetParameters();
  }

  private void ShowGraph()
  {
    if (_axisLabel == null)
      return;

    if (_dataLabel == null)
      return;

    foreach (GameObject gameobj in m_gameObjectList)
    {
      Destroy(gameobj);
    }
    m_gameObjectList.Clear();
    DrawAxisLabels();
    DrawChartValueText();
  }

  private void DrawAxisLabels()
  {
    if (_axisLabels.Count <= 0)
    {
      return;
    }
    if (_chartLine == null)
    {
      return;
    }

    List<UIVertex> _vertexList = _chartLine.GetVerticesPosition();

    if (_axisLabel == null)
      return;

    if (_axisLabels.Count != _vertexList.Count)
    {
      return;
    }

    for (int i = 0; i < _vertexList.Count; i++)
    {
      RectTransform _text = Instantiate(_axisLabel);
      _text.SetParent(_labelsRoot);
      _text.gameObject.SetActive(true);
      _text.anchoredPosition = _vertexList[i].position;
      _text.GetComponent<Text>().text = _axisLabels[i];
      m_gameObjectList.Add(_text.gameObject);
    }
  }

  private void DrawChartValueText()
  {
    if (_chartLine == null)
    {
      return;
    }

    List<UIVertex> _vertexList = _chartLine.GetVerticesPosition();

    if (_dataLabel == null)
      return;

    if (_valueList.Length != _vertexList.Count)
    {
      return;
    }

    for (int i = 0; i < _vertexList.Count; i++)
    {
      RectTransform _text = Instantiate(_dataLabel);
      _text.SetParent(_labelsRoot);
      _text.gameObject.SetActive(true);
      Vector2 p = _vertexList[i].position;
      p.y = p.y - 15f;
      _text.anchoredPosition = p;
      _text.GetComponent<Text>().text = GetDataSeries(i).ToString();
      m_gameObjectList.Add(_text.gameObject);
    }
  }

  public void SetDataSeries(int idx, float value)
  {
    if (this._valueList.Length < idx)
    {
      Array.Resize(ref this._valueList, idx + 1);
    }
    this._valueList[idx] = value;
  }

  public float GetDataSeries(int idx)
  {
    if (_valueList.Length < idx)
    {
      return 0f;
    }
    float value = this._valueList[idx];
    return value > this._maxValue ? this._maxValue : value;
  }

  //private void OnValidate()
  private void Update()
  {
    _graphTitle.text = _titleText;
    if (_verticesCount != m_oldVerticesCount)
    {
      if (m_oldVerticesCount != 0)
      {
        if (_verticesCount > m_oldVerticesCount)
        {
          string axislabel = _axisLabels[m_oldVerticesCount - 1];
          float lastvalue = _valueList[m_oldVerticesCount - 1];
          Array.Resize(ref _valueList, _verticesCount);
          for (int i = m_oldVerticesCount; i < _verticesCount; i++)
          {
            _axisLabels.Add(axislabel);
            _valueList[i] = lastvalue;
          }
        }
        else
        {
          Array.Resize(ref _valueList, _verticesCount);
          for (int j = m_oldVerticesCount - 1; j >= _verticesCount; j--)
          {
            _axisLabels.RemoveAt(j);
          }
        }
      }
      m_oldVerticesCount = _verticesCount;
    }

    _chartLine.SetParameters();
    _chartPolygon.SetParameters();
    ShowGraph();
  }
}