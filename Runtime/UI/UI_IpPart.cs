using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_IpPart : MonoBehaviour
{
    public InputField m_linked;
   
    public void MoveCursorOf(int value) {
        int i = GetIndex();
        SetIndex(i + value);
    }

    public int GetIndex() {
        int index = 0;
        int.TryParse(m_linked.text, out index);
        return index; 
    }
    public void SetIndex(int value) {
        m_linked.text = ""+Mathf.Clamp(value, 0, 255);
    }
    public void Reset() {
        m_linked = GetComponent<InputField>();
    }

    internal void SetInteractable(bool isInteractable)
    {
        m_linked.interactable = isInteractable;
    }
}
