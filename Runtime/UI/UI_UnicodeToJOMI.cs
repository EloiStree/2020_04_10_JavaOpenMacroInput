using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UnicodeToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public void PushUnicode(string hexa)
    {
        if (m_targets)
            foreach (var item in m_targets.GetJavaOMISelected())
            {
                item.Unicode(hexa);
            }
    }
    public void PushUnicode(int index)
    {
        if (m_targets)
            foreach (var item in m_targets.GetJavaOMISelected())
            {
                item.Unicode(index);
            }
    }

}
