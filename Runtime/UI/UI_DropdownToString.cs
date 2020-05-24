using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public NameToPush(string name, string command)
        {
            m_name = name;m_toPush = command;
        }
    }
    public void SetNameAndPushFromText(string text)
    {
        string[] tokens = text.Split('\n');
        SetNameAndPushWith(tokens, tokens);
    }

    public void SetNameAndPushWith(string[] options)
    {
        SetNameAndPushWith(options, options);
    }
    public void SetNameAndPushWith(string[] options , string[] cmds)
    {
        if (options.Length != cmds.Length)
            throw new ArgumentException("Option and Cmd lenght must be the same.");
        m_linked.ClearOptions();
        m_linked.AddOptions(options.ToList());
        m_canBePush = new NameToPush[options.Length];
        for (int i = 0; i < options.Length; i++)
        {
            m_canBePush[i] = new NameToPush(options[i], cmds[i]);
        }
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
            m_canBePush[i] = new NameToPush(str[i], str[i] + "↕") ;
        }
    }
}
