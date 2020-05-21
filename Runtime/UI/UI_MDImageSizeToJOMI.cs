using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MDImageSizeToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public InputField m_width;
    public InputField m_height;

    public void PushImageAsHTML(bool useEmbbed) {
        bool useWidth, useHeight;
        int width = 1, height = 1;
        useWidth = int.TryParse(m_width.text, out width);
        if (!useWidth)
            width = 1;
        useHeight = int.TryParse(m_height.text, out height);
        if (!useHeight)
            height = 1;
        string imgWidth = string.Format(" width =\"{0}\" ", width);
        string imgheight = string.Format(" height =\"{0}\" ", height);
        string imgCodeStart ="<img src=\"";
        string imgCodeEnd = string.Format("\" alt=\"-\" {0} {1} />", useWidth ? imgWidth : "", useHeight ? imgheight : "");

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            if (useEmbbed)
                item.EmbracePast(imgCodeStart, imgCodeEnd);
            else
                item.PastText(imgCodeStart+"IMAGELINK"+imgCodeEnd);
        }

    }
}
