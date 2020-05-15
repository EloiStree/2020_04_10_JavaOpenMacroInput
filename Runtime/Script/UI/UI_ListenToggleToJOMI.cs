using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ListenToggleToJOMI : UI_ItemWithDrowdownToJOMI
{
    [TextArea(0,4)]
    public string m_onText= "ctrl↓ k↕ h↕ ctrl↑";

    [TextArea(0, 4)] public string m_offText= "ctrl↓ k↕ h↕ ctrl↑";

    public void Push(bool onOff) {

        PushText(onOff ? m_onText : m_offText);
    
    }

}
