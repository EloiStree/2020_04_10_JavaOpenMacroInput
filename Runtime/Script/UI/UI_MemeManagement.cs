using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MemeManagement : MonoBehaviour
{
    public UI_ImageUrlToClipboardJOMI[] m_memes;

    public void ToggleAllPast(bool value)
    {
        for (int i = 0; i < m_memes.Length; i++)
        {
            m_memes[i].m_usePast = value;
        }
    }
    public void ToggleAllEnter(bool value)
    {
        for (int i = 0; i < m_memes.Length; i++)
        {
            m_memes[i].m_useEnter = value;
        }
    }
    public void ToggleAllAsMarkdown(bool value)
    {
        for (int i = 0; i < m_memes.Length; i++)
        {
            m_memes[i].m_useMarkDown = value;
        }
    }


}
