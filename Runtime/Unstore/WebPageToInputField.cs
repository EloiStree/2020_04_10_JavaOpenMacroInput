using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WebPageToInputField : MonoBehaviour
{
    public TextLoadEvent m_onLoaded;

    [System.Serializable]
    public class TextLoadEvent : UnityEvent<string>{}

    public void LoadPage(string url)
    {
        StartCoroutine(CoroutineLoadPage(url));
    }
    private IEnumerator CoroutineLoadPage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        m_onLoaded.Invoke(www.text);
    }
}
