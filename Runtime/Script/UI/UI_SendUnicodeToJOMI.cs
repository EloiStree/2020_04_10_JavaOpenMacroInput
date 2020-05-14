using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SendUnicodeToJOMI : MonoBehaviour
{
    public Button m_linkedButton;
    public UI_ServerDropdownJavaOMI m_targets;
    public char m_unicodeChar='A';

    public void PushUnicode(string specificValue)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.PastText(specificValue);
        }

    }
    public void PushUnicode()
    {
        PushUnicode(""+m_unicodeChar);
    }

    private void OnValidate()
    {
        SetUnicode(m_unicodeChar);
    }
    private void Reset()
    {
        m_linkedButton = GetComponent<Button>();
    }

    public void SetUnicode(char c)
    {
        m_unicodeChar = c; 
        Text t = m_linkedButton.GetComponentInChildren<Text>();
        if (t != null)
            t.text = "" + m_unicodeChar;
    }

    public void SetTargets(UI_ServerDropdownJavaOMI targets)
    {
        m_targets = targets;
    }
}
