using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_ListenToButtonToJOMI : UI_ItemWithDrowdownToJOMI
{
    [TextArea(0,4)]
    public string m_textToPush = "";
    public Button m_linkedButton;
    private void OnEnable()
    {
        m_linkedButton.onClick.AddListener(Push);
    }
    private void OnDisable()
    {
        m_linkedButton.onClick.RemoveListener(Push);

    }

    public void Push()
    {
        if (m_textToPush == "") {
            Text t = GetComponentInChildren<Text>();
            if (t != null)
                m_textToPush = t.text;
        }
        PushText(m_textToPush);
    }

    public void Reset()
    {
        m_linkedButton = GetComponent<Button>();

        Text t = GetComponentInChildren<Text>();
        if (t != null)
            m_textToPush = t.text;
    }


}
