using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_floating : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform m_RectTransform;
    float x, y;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        x = m_RectTransform.anchoredPosition.x;
        y = m_RectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        m_RectTransform.anchoredPosition = new Vector2(x, y + Mathf.Sin(Time.time) * 50f * (-1f));
    }
}
