using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputFieldToJOMI : UI_ItemWithDrowdownToJOMI
{
    public InputField m_from;
    public string m_defautlIfReset;
    public void PushText()
    {
        PushText(m_from.text);
    }
    public void ResetText()
    {
        m_from.SetTextWithoutNotify(m_defautlIfReset);
    }

    public void SetText(string m_gitCommand)
    {
        throw new NotImplementedException();
    }

    private void OnValidate()
    {
        ResetText();
    }

    public string GetText()
    {
        return m_from.text;
    }
}
