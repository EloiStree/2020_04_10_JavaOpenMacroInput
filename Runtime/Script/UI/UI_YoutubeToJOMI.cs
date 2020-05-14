using JavaOpenMacroInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_YoutubeToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public void CopyPastFromID(ThumbnailYT value)
    {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            PastWith(item, value);
        }
    }
    public void CopyPastAllFromID() {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            PastWith(item,ThumbnailYT._0);
            PastWith(item,ThumbnailYT._1);
            PastWith(item,ThumbnailYT._2);
            PastWith(item,ThumbnailYT._3);
            PastWith(item,ThumbnailYT._default);
            PastWith(item,ThumbnailYT._hqdefault);
            PastWith(item,ThumbnailYT._mqdefault);
            PastWith(item,ThumbnailYT._sddefault);
            PastWith(item,ThumbnailYT._maxresdefault);
        } 
    }
private void PastWith(JavaOMI omi, ThumbnailYT value)
    {
        string imageType = "default";
        switch (value)
        {
            case ThumbnailYT._0: imageType= "0"; break;
            case ThumbnailYT._1: imageType = "1"; break;
            case ThumbnailYT._2: imageType = "2"; break;
            case ThumbnailYT._3: imageType = "3"; break;
            case ThumbnailYT._default: imageType = "default"; break;
            case ThumbnailYT._hqdefault: imageType = "hqdefault"; break;
            case ThumbnailYT._mqdefault: imageType = "mqdefault"; break;
            case ThumbnailYT._sddefault: imageType = "sddefault"; break;
            case ThumbnailYT._maxresdefault: imageType = "maxresdefault"; break;
            default: imageType = "default"; break;
        }

        omi.PastText("[![Youtube Video] (http://img.youtube.com/vi/PUTIDHERE");
        omi.PastText("/"+ imageType + ".jpg)](https://youtu.be/PUTIDHERE)  ");
    }

    public void JustPastAllThumbnailPossible() {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.PastText(GetAllThumbnail());
        }
    }
    private string GetAllThumbnail() {

        return "\n[![Youtube 0](https://img.youtube.com/vi/VIDEOID/0.jpg)](https://youtu.be/PUTIDHERE)  " +
        "\n[![Youtube 1](https://img.youtube.com/vi/VIDEOID/1.jpg)](https://youtu.be/PUTIDHERE)  " +
        "\n[![Youtube 2](https://img.youtube.com/vi/VIDEOID/2.jpg)](https://youtu.be/PUTIDHERE)  " +
                "\n[![Youtube 3](https://img.youtube.com/vi/VIDEOID/3.jpg)](https://youtu.be/PUTIDHERE)  " +
                "\n[![Youtube default](https://img.youtube.com/vi/VIDEOID/default.jpg)](https://youtu.be/PUTIDHERE)  " +
               "\n[![Youtube hqdefault](https://img.youtube.com/vi/VIDEOID/hqdefault.jpg)](https://youtu.be/PUTIDHERE)  " +
               "\n[![Youtube mqdefault](https://img.youtube.com/vi/VIDEOID/mqdefault.jpg)](https://youtu.be/PUTIDHERE)  " +
               "\n[![Youtube sddefault](https://img.youtube.com/vi/VIDEOID/sddefault.jpg)](https://youtu.be/PUTIDHERE)  " +
               "\n[![Youtube maxresdefault](https://img.youtube.com/vi/VIDEOID/maxresdefault.jpg)](https://youtu.be/PUTIDHERE)  ";
    }

public enum ThumbnailYT { _0 , _1 , _2 , _3 , _default , _hqdefault , _mqdefault , _sddefault ,_maxresdefault}
}
