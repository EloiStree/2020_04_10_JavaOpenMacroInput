using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavaOmiMono : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public void PushTextAsPast(string textToPast)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.PastText(textToPast);
        }
    }
    public void PushRawCommand(string rawCommand)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SendRawCommand(rawCommand);
        }
    }
    public void PushShortcutCommand(string shortcutCommand)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SendShortcutCommands(shortcutCommand);
        }
    }


}
