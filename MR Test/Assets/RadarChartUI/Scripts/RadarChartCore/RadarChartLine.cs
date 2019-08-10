using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// draw the radar chart background line
public class RadarChartLine : Graphic
{
  // draw radius
  const float RADIUS = 0.5f;

  private int m_verticesCount = 5;
  private float m_maxValue = 1f;
  private bool m_drawMajorGrid = true;
  private float m_majorGridInterval = 0.2f;
  private float m_lineWidth = 0.02f;

  public RadarChart chartManager;

  public void SetParameters()
  {
    m_verticesCount = chartManager._verticesCount;
    m_maxValue = chartManager._maxValue;
    m_drawMajorGrid = chartManager._drawMajorGrid;
    m_majorGridInterval = chartManager._majorGridInterval;
    m_lineWidth = chartManager._lineWidth;

    SetVerticesDirty();
  }

  public List<UIVertex> GetVerticesPosition()
  {
    float vol = this.m_maxValue + 0.6f;
    List<UIVertex> _vertexList = new List<UIVertex>();

    _vertexList.Clear();

    var v = UIVertex.simpleVert;
    v.color = Color.red;

    // every vertex
    for (int i = 0; i < m_verticesCount; i++)
    {
      float deg = (360f / m_verticesCount) * 0.5f;
      float offset = (this.m_lineWidth / Mathf.Cos(deg * Mathf.Deg2Rad)) / 2f;
      float rad = (90f - (360f / (float)m_verticesCount) * i) * Mathf.Deg2Rad;

      float x = 0.5f + Mathf.Cos(rad) * (RADIUS * vol + offset);
      float y = 0.5f + Mathf.Sin(rad) * (RADIUS * vol + offset);

      Vector2 p = CreatePos(x, y);
      v.position = p;

      _vertexList.Add(v);
    }
    return _vertexList;
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {

    vh.Clear();

    var v = UIVertex.simpleVert;
    v.color = color;


    // Outer frame
    this.DrawFrame(vh, this.m_maxValue);

    // Axis
    this.DrawAxis(vh, this.m_maxValue);

    // Major Grid
    if (this.m_drawMajorGrid && this.m_majorGridInterval < this.m_maxValue)
    {
      int numOfGrid = (int)(this.m_maxValue / this.m_majorGridInterval);
      for (int i = 0; i < numOfGrid; i++)
      {
        this.DrawFrame(vh, i * this.m_majorGridInterval);
      }
    }
  }

  // 座標
  private Vector2 CreatePos(float x, float y)
  {
    Vector2 p = Vector2.zero;
    p.x -= rectTransform.pivot.x;
    p.y -= rectTransform.pivot.y;
    p.x += x;
    p.y += y;
    p.x *= rectTransform.rect.width;
    p.y *= rectTransform.rect.height;
    return p;
  }

  // Draw outer frame
  private void DrawFrame(VertexHelper vh, float vol)
  {
    int currentVectCount = vh.currentVertCount;

    var v = UIVertex.simpleVert;
    v.color = color;

    // every vertice
    for (int i = 0; i < m_verticesCount; i++)
    {
      float deg = (360f / m_verticesCount) * 0.5f;
      float offset = (this.m_lineWidth / Mathf.Cos(deg * Mathf.Deg2Rad)) / 2f;

      float rad = (90f - (360f / (float)m_verticesCount) * i) * Mathf.Deg2Rad;

      float x1 = 0.5f + Mathf.Cos(rad) * (RADIUS * vol - offset);
      float y1 = 0.5f + Mathf.Sin(rad) * (RADIUS * vol - offset);
      float x2 = 0.5f + Mathf.Cos(rad) * (RADIUS * vol + offset);
      float y2 = 0.5f + Mathf.Sin(rad) * (RADIUS * vol + offset);

      Vector2 p1 = CreatePos(x1, y1);
      Vector2 p2 = CreatePos(x2, y2);

      v.position = p1;
      vh.AddVert(v);

      v.position = p2;
      vh.AddVert(v);

      vh.AddTriangle(
          (((i + 0) * 2) + 0) % (m_verticesCount * 2) + currentVectCount,
          (((i + 0) * 2) + 1) % (m_verticesCount * 2) + currentVectCount,
          (((i + 1) * 2) + 0) % (m_verticesCount * 2) + currentVectCount);

      vh.AddTriangle(
          (((i + 1) * 2) + 0) % (m_verticesCount * 2) + currentVectCount,
          (((i + 0) * 2) + 1) % (m_verticesCount * 2) + currentVectCount,
          (((i + 1) * 2) + 1) % (m_verticesCount * 2) + currentVectCount);
    }

  }

  // Draw axis
  private void DrawAxis(VertexHelper vh, float vol)
  {
    int currentVertCount = vh.currentVertCount;

    var v = UIVertex.simpleVert;
    v.color = color;

    for (int i = 0; i < m_verticesCount; i++)
    {
      float halfwidthDeg = 90 * this.m_lineWidth / (Mathf.PI * RADIUS * vol);

      float rad1 = (90f - halfwidthDeg - (360f / (float)m_verticesCount) * i) * Mathf.Deg2Rad;
      float rad2 = (90f + halfwidthDeg - (360f / (float)m_verticesCount) * i) * Mathf.Deg2Rad;

      float x3 = 0.5f + Mathf.Cos(rad1) * RADIUS * vol;
      float y3 = 0.5f + Mathf.Sin(rad1) * RADIUS * vol;
      float x4 = 0.5f + Mathf.Cos(rad2) * RADIUS * vol;
      float y4 = 0.5f + Mathf.Sin(rad2) * RADIUS * vol;

      float x1 = 0.5f + (x3 - x4) / 2f;
      float y1 = 0.5f + (y3 - y4) / 2f;
      float x2 = 0.5f + (x4 - x3) / 2f;
      float y2 = 0.5f + (y4 - y3) / 2f;

      Vector2 p1 = CreatePos(x1, y1);
      Vector2 p2 = CreatePos(x2, y2);
      Vector2 p3 = CreatePos(x3, y3);
      Vector2 p4 = CreatePos(x4, y4);

      v.position = p1;
      vh.AddVert(v);

      v.position = p2;
      vh.AddVert(v);

      v.position = p3;
      vh.AddVert(v);

      v.position = p4;
      vh.AddVert(v);

      vh.AddTriangle(
          ((i * 4) + 0) + currentVertCount,
          ((i * 4) + 3) + currentVertCount,
          ((i * 4) + 2) + currentVertCount
      );

      vh.AddTriangle(
          ((i * 4) + 0) + currentVertCount,
          ((i * 4) + 1) + currentVertCount,
          ((i * 4) + 3) + currentVertCount
      );
    }
  }
}