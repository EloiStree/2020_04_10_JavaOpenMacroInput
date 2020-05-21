using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CursorToString : MonoBehaviour
{
    public InputField m_x;
    public InputField m_y;
    public Dropdown m_valueType;
    public Dropdown m_moveType;
    public Button m_push;
    public InputField m_tagetInput;
    
    public void OnEnable()
    {
        m_push.onClick.AddListener(Push);
    }
    private void OnDisable()
    {

        m_push.onClick.RemoveListener(Push);
    }

    void Push()
    {
        m_tagetInput.text += string.Format("[[m{0}:{1}{2}:{3}{4}]]"
            , m_moveType.value == 0 ? 'm' : 'a'
            , m_x.text, m_valueType.value == 0 ? "" : "p"
            , m_y.text, m_valueType.value == 0 ? "" : "p");
    }
}
