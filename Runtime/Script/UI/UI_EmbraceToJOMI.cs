using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EmbraceToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    [TextArea(0, 5)]
    public string m_leftEmbrace;
    [TextArea(0, 5)]
    public string m_rightEmbrace;
    public enum EmbraceType { AroundText, PerLine}
    public EmbraceType m_embraceType = EmbraceType.AroundText;

    public void PushCommand() {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            if(m_embraceType ==EmbraceType.AroundText)
                item.EmbracePast(m_leftEmbrace, m_rightEmbrace);
            if (m_embraceType == EmbraceType.PerLine)
                item.EmbracePerLinePast(m_leftEmbrace, m_rightEmbrace);

        }
    }
}
