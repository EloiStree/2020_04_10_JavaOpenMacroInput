using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_DropboxToStringUnityEvent : MonoBehaviour, I_TextSavable
{
    public Dropdown m_dropdown;
    public string m_dropdownTitle="...";
    [TextArea(0, 10)]
    public string m_textToMethods = "";
    public string[] m_lines;
    public StringUnityEvent m_selected;
    void OnValidate()
    {
        m_lines = (m_dropdownTitle+"\n"+ m_textToMethods).Split('\n');
        SetFromStrings(m_lines);
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

    public void SetFromStrings(string[] lines) {
        m_dropdown.ClearOptions();
        m_dropdown.AddOptions(lines.ToList());
    }

    private void CallMethod(int arg0)
    {
        string toCall = m_dropdown.options[arg0].text.Trim();
        m_selected.Invoke(toCall);
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
        SetFromStrings(text.Split('\n'));
    }

    [System.Serializable]
    public class StringUnityEvent : UnityEvent<string> { }
}
