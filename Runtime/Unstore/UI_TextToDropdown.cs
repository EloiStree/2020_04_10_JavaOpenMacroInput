using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextToDropdown : MonoBehaviour
{

    [TextArea(0,5)]
    public string m_text="";
    public Dropdown m_linked;

    private void OnValidate()
    {
        if (m_linked == null)
            return;
        m_linked.ClearOptions();
        m_linked.AddOptions(m_text.Split('\n').ToList());
    }
    private void Reset()
    {
        m_linked = GetComponent<Dropdown>();
    }
}
