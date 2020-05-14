using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_UTF8GridToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public Button[] m_charToPush;
    public InputField m_lastPush;
    public UI_UnicodeBuilder m_userPref;
    public int m_minValue = 20;
    public int m_maxValue = 1200;
    public int m_value = 20;
    public string m_lastValue = "";


    public void SetAllButtonFrom(float valueInPct)
    { 
        SetAllButtonFrom( (int)((valueInPct*(m_maxValue+m_minValue))-m_minValue) );
    }


    public void NextPage()
    {
        SetAllButtonFrom( m_value + m_charToPush.Length);
    }
    public void PreviousPage()
    {
        SetAllButtonFrom( m_value - m_charToPush.Length);
    }

    private void SetAllButtonFrom(int value) {
        if (value < 20)
            value = 20;
        m_value = value;
        foreach (var item in m_charToPush)
        {
            if (value < m_maxValue)
                item.GetComponentInChildren<Text>().text = ""+Convert.ToChar(value);
            else
                item.GetComponentInChildren<Text>().text = " ";
            value++;
        }

    }

    public void SaveLastValue(){
        m_userPref.Add(m_lastValue);
    }
    private void OnValidate()
    {
        SetAllButtonFrom(m_value);
    }

    void OnEnable()
    {
        foreach (var item in m_charToPush)
        {
            item.onClick.AddListener(delegate { PushAction(item); });
        }
    }
    void OnDisable()
    {
        foreach (var item in m_charToPush)
        {
            item.onClick.RemoveAllListeners();
        }
    }

    private void PushAction(Button item)
    {
        m_lastValue = item.GetComponentInChildren<Text>().text;
        foreach (var t in m_targets.GetJavaOMISelected())
        {
            t.PastText(m_lastValue);
        }
        m_lastPush.text = m_lastValue + m_lastPush.text;
        if (m_lastPush.text.Length > 300)
            m_lastPush.text = m_lastPush.text.Substring(0, 300);
    }

}
