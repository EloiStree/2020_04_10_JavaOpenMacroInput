using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EmbraceFromInputToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public InputField m_leftSide;
    public InputField m_rightSide;

    public void Push() {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
                item.EmbracePast(m_leftSide.text, m_rightSide.text);
   
        }
    }
}
