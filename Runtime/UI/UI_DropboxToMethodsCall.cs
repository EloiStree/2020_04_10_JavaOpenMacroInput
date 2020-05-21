using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_DropboxToMethodsCall : MonoBehaviour, I_TextSavable

{
    public Dropdown m_dropdown;
    public MonoBehaviour m_sendTarget;
    public string m_dropdownTitle="Method call ...";
    [TextArea(0,10)]
    public string m_textToMethods = "";
    public string [] m_lines;
    public bool m_useDebug;
    void OnValidate()
    {
        SetFromStrings(m_textToMethods);
    }
    private void Start()
    {
        m_dropdown.onValueChanged.RemoveListener(CallMethod);
        m_dropdown.onValueChanged.AddListener(CallMethod);
    }
    private void OnEnable()
    {
        m_dropdown.onValueChanged.RemoveListener(CallMethod);
        m_dropdown.onValueChanged.AddListener(CallMethod);
    }
    private void OnDisable()
    {
        m_dropdown.onValueChanged.RemoveListener(CallMethod);
    }

    private void CallMethod(int arg0)
    {
        string toCall = m_dropdown.options[arg0].text.Trim();
        if(m_useDebug)
            Debug.Log(toCall);
        m_sendTarget.SendMessage(toCall);
    }
    public string GetSavableText()
    {
        return string.Join("\n", m_dropdown.options.Select(k => k.text).ToArray());
    }

    public string GetSavableDefaultText()
    {
        return "None";
    }

    public void SetTextFromLoad(string text)
    {
        SetFromStrings(text);
    }

    private void SetFromStrings(string text)
    {
        m_lines = (m_dropdownTitle + "\n" + text).Split('\n');
        m_dropdown.ClearOptions();
        m_dropdown.AddOptions(m_lines.ToList());
    }
}
