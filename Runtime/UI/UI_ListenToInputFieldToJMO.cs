using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ListenToInputFieldToJMO : UI_ItemWithDrowdownToJOMI
{
    public bool m_listenToChange;
    public bool m_listenToEndEdit;

    public InputField m_linkField;
    private void OnEnable()
    {
        if (m_listenToChange)
            m_linkField.onValueChanged.AddListener(Push);
        if (m_listenToEndEdit)
            m_linkField.onEndEdit.AddListener(Push);
    }
    private void OnDisable()
    {
        if (m_listenToChange)
            m_linkField.onValueChanged.RemoveListener(Push);

        if (m_listenToEndEdit)
            m_linkField.onEndEdit.RemoveListener(Push);

    }

    private void Push(string arg0)
    {
        PushText(arg0);
    }

    public void Reset()
    {
        m_linkField = GetComponent<InputField>();
    }
}
