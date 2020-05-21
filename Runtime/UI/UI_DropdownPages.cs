using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DropdownPages : MonoBehaviour
{
    public Dropdown m_linkedDropbox;
    public Page [] m_pages;
    [System.Serializable]
    public class Page {
        public string m_pageName;
        public int m_dropdownIndex;
        public GameObject[] m_targets;

    }

    public void Awake()
    {
        SetPageFromDropdown(m_linkedDropbox.value);

    }
    private void OnValidate()
    {
        List<string> ls = new List<string>();
        m_linkedDropbox.options.Clear();
        for (int i = 0; i < m_pages.Length; i++)
        {
            ls.Add(m_pages[i].m_pageName);
        }
        m_linkedDropbox.AddOptions(ls);

    }

    public void SetPageFromDropdown(int index) {
        Page selected=null;
        for (int i = 0; i < m_pages.Length; i++)
        {
            if (index == m_pages[i].m_dropdownIndex) {
                selected = m_pages[i];
            
            }
        }
        SelectPage(selected);
    }

    public void SetPage(string pageName) {
        Page selected = null;
        for (int i = 0; i < m_pages.Length; i++)
        {
            if (pageName == m_pages[i].m_pageName)
            {
                selected = m_pages[i];

            }
        }
        SelectPage(selected);
    }

    public void SelectPage(Page page) {
        for (int i = 0; i < m_pages.Length; i++)
        { 
                for (int j = 0; j < m_pages[i].m_targets.Length; j++)
                {
                    m_pages[i].m_targets[j].SetActive(page == m_pages[i]);
                }
        }
    }
    
}
