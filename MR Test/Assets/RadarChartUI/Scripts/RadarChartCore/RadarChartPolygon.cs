using UnityEngine;
using UnityEngine.UI;
using System;

// Draw radar chart
public class RadarChartPolygon : Graphic
{
  // Draw radius
  const float RADIUS = 0.5f;

  private int m_VerticesCount = 5;
  private float m_MaxValue = 1f;
  private float[] m_ValueList;

  public RadarChart chartManager;

  public void SetParameters()
  {
    m_VerticesCount = chartManager._verticesCount;
    m_MaxValue = chartManager._maxValue;
    if (m_ValueList == null)
    {
      m_ValueList = new float[m_VerticesCount];
    }

    for (int i = 0; i < m_VerticesCount; i++)
    {
      SetVolume(i, chartManager.GetDataSeries(i));
    }
    SetVerticesDirty();
  }

  public void SetVolume(int idx, float value)
  {
    if (this.m_ValueList.Length - 1 < idx)
    {
      Array.Resize(ref this.m_ValueList, idx + 1);
    }
    this.m_ValueList[idx] = value;
  }

  public float GetVolume(int idx)
  {
    if (this.m_ValueList == null)
      return 0;
    if (this.m_ValueList.Length - 1 < idx)
    {
      return 0;
    }

    float v = this.m_ValueList[idx];
    return v > this.m_MaxValue ? this.m_MaxValue : v;
  }


  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    var v = UIVertex.simpleVert;
    v.color = color;

    Vector2 center = CreatePos(0.5f, 0.5f);
    v.position = center;
    vh.AddVert(v);

    //every vertex
    for (int i = 1; i <= m_VerticesCount; i++)
    {
      float rad = (90f - (360f / (float)m_VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
      float x = 0.5f + Mathf.Cos(rad) * RADIUS * GetVolume(i - 1);
      float y = 0.5f + Mathf.Sin(rad) * RADIUS * GetVolume(i - 1);

      Vector2 p = CreatePos(x, y);
      v.position = p;
      vh.AddVert(v);

      vh.AddTriangle(0, i, i == m_VerticesCount ? 1 : i + 1);
    }

  }

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

}