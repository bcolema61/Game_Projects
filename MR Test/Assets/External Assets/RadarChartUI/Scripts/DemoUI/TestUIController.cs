using UnityEngine;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
  public RadarChart _radar;
  public Slider _radarChartSideSlider;
  public GameObject _chartSideTitlePref;
  public GameObject _chartSideValuePref;
  public GameObject _chartSideTitleRoot;
  public GameObject _chartSideValueRoot;

  private bool m_canUpdateValue = false;
  public void OnAxisTitleUpdate()
  {
    //Debug.Log("haha");
    for (int i = 0; i < _chartSideTitleRoot.transform.childCount; ++i)
    {
      Text t = _chartSideTitleRoot.transform.GetChild(i).GetComponentInChildren<Text>();
      // Assuming both lists have same size
      _radar._axisLabels[i] = t.text;
    } 
  }
  
  public void OnAxisValueUpdate()
  {
    if (!m_canUpdateValue)
      return;
    for (int i = 0; i < _chartSideValueRoot.transform.childCount; ++i)
    {
      Slider s = _chartSideValueRoot.transform.GetChild(i).GetComponentInChildren<Slider>();
      // Assuming both lists have same size
      _radar._valueList[i] = s.value;
    } 
  }
  
  public void OnChartSideValueUpdate()
  {
    // Update count
    _radar._verticesCount = (int)_radarChartSideSlider.value;
    // Wait a bit for the update
    Invoke("UpdateTitleList", 0.5f);
    Invoke("UpdateValueList", 0.5f);
    // Clean childs of side titles
    //UpdateTitleList();
  }
  
  private void OnEnable()
  {
    _radarChartSideSlider.value = _radar._verticesCount;
    UpdateTitleList();
    UpdateValueList();
  }
  
  private void UpdateTitleList()
  {
    DestroyChildren(_chartSideTitleRoot.transform);
    // Add titles to root
    //Debug.Log("size: " + _radar._axisLabels.Count);
    foreach(string axisTitle in _radar._axisLabels)
    {
      GameObject obj = Instantiate(_chartSideTitlePref, _chartSideTitleRoot.transform);
      InputField input = obj.GetComponent<InputField>();
      input.text = axisTitle;
    }
  }
  
  private void UpdateValueList()
  {
    m_canUpdateValue = false;
    DestroyChildren(_chartSideValueRoot.transform);
    // Add titles to root
    //Debug.Log("size: " + _radar._axisLabels.Count);
    foreach(float value in _radar._valueList)
    {
      GameObject obj = Instantiate(_chartSideValuePref, _chartSideValueRoot.transform);
      Slider s = obj.GetComponent<Slider>();
      s.value = value;
    }
    m_canUpdateValue = true;
  }
  
  private void DestroyChildren(Transform t)
  {
    for (int i = t.childCount - 1; i >= 0; --i)
    {
      //Debug.Log("Destroy: " + t.GetChild(i).gameObject);
      GameObject.Destroy(t.GetChild(i).gameObject);
    }

    t.DetachChildren();
  }
}