using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GoogleForGifToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public InputField m_sentenceToFind;
    public bool m_insertImageFirst=true;
    public int m_size=64;

    public void SearchForGIF()
    {
        SearchForGIF(m_sentenceToFind.text);
    }

    public void SearchForGIF(string keyword)
    {
        if (keyword == null || keyword.Length <= 0)
            keyword = "";
        if (m_insertImageFirst) { 
            string imgCode = string.Format("<img src=\"IMAGELINK\" alt=\"{2}\" width =\"{0}\" height =\"{1}\" />", m_size, m_size, keyword);
            foreach (var item in m_targets.GetJavaOMISelected())
            {
                item.PastText(imgCode);
            }
        }

            keyword = keyword.Replace(" ", "%20");

        string gifSearch = "https://www.google.com/search?q=" + keyword + "&tbm=isch&ved=2ahUKEwjnjajj-7HpAhUDtqQKHdVFCLQQ2-cCegQIABAA&oq=meme&gs_lcp=CgNpbWcQDFAAWABg0ecCaABwAHgAgAEAiAEAkgEAmAEAqgELZ3dzLXdpei1pbWc&sclient=img&ei=O3-8XqeEIIPskgXVi6GgCw&bih=1211&biw=2379&safe=off&tbs=ic%3Atrans%2Citp%3Aanimated%2Cisz%3Ai&hl=en";

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SendRawCommand("url:" + gifSearch);
        }
    }
}
