using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonToString : MonoBehaviour
{
    public string m_textToPush="";
    public Button m_linkedButton;
    public InputField m_inputField;
    public bool m_useTextLabel;

    private void OnEnable()
    {
        m_linkedButton.onClick.AddListener(Push);
    }
    private void OnDisable()
    {
        m_linkedButton.onClick.RemoveListener(Push);

    }

    private void Push()
    {
        if (m_inputField == null)
            return;
        if (m_useTextLabel)
        {
            if (m_linkedButton != null) { 
                Text t = m_linkedButton.GetComponentInChildren<Text>();
                if (t != null) { 
                    m_textToPush = t.text;
                }
            }
        }
        m_inputField.text += m_textToPush;
    }

    private void Reset()
    {
        m_linkedButton = GetComponent<Button>();
       
        Text t = GetComponentInChildren<Text>();
        if(t!=null)
        m_textToPush = t.text;
    }
}
