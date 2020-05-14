using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnicodeBuilder : MonoBehaviour, I_UseHarddriveSave
{
    [TextArea(0,10)]
    public string m_unicodeChars="";
    public RectTransform m_whereToCreate;
    public GameObject m_objectToInstanciate;
    public UI_ServerDropdownJavaOMI m_targets;
    public GridLayoutGroup m_grids;
    public InputField m_debugInput;
    public int maxPerLine=4;
    public bool m_saveOnHarddrive = false;

    private void Awake()
    {

        SetUnicodeChars ( UnityDirectoryStorage.LoadFile("JavaOMI", "UnicodePrefList.txt", m_saveOnHarddrive));
        Invoke("Refresh", 0.1f);
    
    }
    private void Start()
    {
        SetUnicodeChars(UnityDirectoryStorage.LoadFile("JavaOMI", "UnicodePrefList.txt", m_saveOnHarddrive));
        Refresh();

    }
    private void OnEnable()
    {
        SetUnicodeChars(UnityDirectoryStorage.LoadFile("JavaOMI", "UnicodePrefList.txt", m_saveOnHarddrive));
        Refresh();

    }
    private void OnDisable()
    {
        UnityDirectoryStorage.SaveFile("JavaOMI", "UnicodePrefList.txt", m_unicodeChars, m_saveOnHarddrive);
    }
    private void OnApplicationQuit()
    {
        
        UnityDirectoryStorage.SaveFile("JavaOMI", "UnicodePrefList.txt", m_unicodeChars, m_saveOnHarddrive);
    }
    private void OnValidate()
    {
        if(Application.isPlaying)
          Refresh();
    }
    public void SetUnicodeChars(string chars)
    {
        if (chars == null || chars.Length<1)
            chars = m_unicodeChars;
        m_unicodeChars = chars;
        UnityDirectoryStorage.SaveFile("JavaOMI", "UnicodePrefList.txt", m_unicodeChars, m_saveOnHarddrive);
        Refresh();
    }

    internal void Add(string value)
    {
        m_unicodeChars += value;
        SetUnicodeChars(m_unicodeChars);
    }

    public void Refresh() {

        if (m_objectToInstanciate == null) return;
        if (m_whereToCreate == null) return;

        for (int i = m_whereToCreate.childCount-1; i >=0 ; i--)
        {
            Destroy(m_whereToCreate.GetChild(i).gameObject);
        }
        int w = (int)(((float)m_whereToCreate.rect.width / (float) maxPerLine) - 2f);
        m_grids.cellSize = new Vector2(w,w);

        char[] codes = m_unicodeChars.ToCharArray().Where(k => k != ' ' 
        && k != '\t' && k != '\n' && k != '\r'
        ).ToArray();
        for (int i = 0; i < codes.Length; i++)
        {
            GameObject instance = GameObject.Instantiate(m_objectToInstanciate);
            instance.transform.SetParent( m_whereToCreate,false);
            UI_SendUnicodeToJOMI unicodeScript = instance.GetComponent<UI_SendUnicodeToJOMI>();
            unicodeScript.SetUnicode(codes[i]);
            unicodeScript.SetTargets(m_targets);
        }
        if (m_debugInput != null)
            m_debugInput.SetTextWithoutNotify(  m_unicodeChars);
    }

    public void SetSwitchTo(bool useHarddrive)
    {
        m_saveOnHarddrive = useHarddrive;
    }

    public bool IsUsingHarddriveSave()
    {
        return m_saveOnHarddrive;
    }
}
