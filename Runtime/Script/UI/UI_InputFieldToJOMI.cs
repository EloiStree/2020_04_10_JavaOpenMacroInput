using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputFieldToJOMI : UI_ItemWithDrowdownToJOMI
{
    public InputField m_from;
    public void PushText()
    {
        PushText(m_from.text);
    }
}
