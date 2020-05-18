using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ImageUrlToClipboardJOMI : MonoBehaviour
{
    public string m_imageUrlLink = "https://avatars1.githubusercontent.com/u/20149493?s=460&u=3f3c9e84bec37d51edfbb88659b0da1f2c58518e&v=4";
    public UI_ServerDropdownJavaOMI m_targets;
    public bool m_usePast = true;
    public float m_timeBeforePasting = 0.5f;
    public bool m_useEnter = true;
    public float m_timeBeforeValidation = 1f;
    public bool m_useMarkDown;


    public void PushImage()
    {
        if (m_useMarkDown)
            PushImageAskMarkdown(m_imageUrlLink);
        else
            PushImage(m_imageUrlLink);
        if (m_usePast && !m_useMarkDown)
            Invoke("Past", m_timeBeforePasting);
        if(m_useEnter)
            Invoke("Validate", m_timeBeforeValidation);
    }

    private void PushImageAskMarkdown(string imageUrlLink)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.PastText(string.Format("<img src = \"{0}\" />",imageUrlLink));
        }
        
    }

    public void PushImage(string url)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.ImageUrlToClipboard(url);
        }
    }
    public void Past()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.Past(true);
        }
    }
    public void Validate()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.Keyboard(JavaOpenMacroInput.JavaKeyEvent.VK_ENTER, JavaOpenMacroInput.PressType.Stroke);
        }
    }
}
