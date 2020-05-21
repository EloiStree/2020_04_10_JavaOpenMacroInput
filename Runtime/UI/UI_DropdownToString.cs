using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_DropdownToString : MonoBehaviour
{
    public Dropdown m_linked;

    public NameToPush[] m_canBePush;
    public StringEvent m_onSelected;

    [System.Serializable]
    public class NameToPush {
        public string m_name="";
        public string m_toPush="";
    }
    
    void OnEnable()
    {
        m_linked.ClearOptions();
        m_linked.AddOptions(m_canBePush.Select(k=>k.m_name).ToList());
        m_linked.onValueChanged.AddListener(Push);
    }

   

    private void OnDisable()
    {

        m_linked.onValueChanged.RemoveListener(Push);
    }

    private void Push(int arg0)
    {
        m_onSelected.Invoke(m_canBePush[arg0].m_toPush);
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }

    private void Reset()
    {
        m_linked = GetComponent<Dropdown>();
        List<string>str=m_linked.options.Select(k => k.text).ToList();
        m_canBePush = new NameToPush[str.Count];
        for (int i = 0; i < str.Count; i++)
        {
            m_canBePush[i] = new NameToPush() { m_name = str[i], m_toPush=str[i]+"↕" };
        }
    }
}
