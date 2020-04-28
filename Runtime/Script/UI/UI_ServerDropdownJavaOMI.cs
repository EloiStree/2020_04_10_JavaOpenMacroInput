using JavaOpenMacroInput;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ServerDropdownJavaOMI : MonoBehaviour
{
    public Dropdown m_linked;
    public bool m_useAllOption=true;
    public bool m_useNoneOption=true;

    void Awake()
    {
        JavaOMI.AddRegisterListener(RefreshDropdown);


    }
    private void OnEnable()
    {
        RefreshDropdown("");


    }


    private void OnDestroy()
    {


        JavaOMI.RemoveRegisterListener(RefreshDropdown);
    }
    private void RefreshDropdown(string runningThreadName)
    {
        m_linked.ClearOptions();
        if(m_useAllOption)
            m_linked.options.Add(new Dropdown.OptionData("All"));
       m_linked.AddOptions(JavaOMI.GetAllRunningNameRegistered().ToList());
        if (m_useNoneOption)
            m_linked.options.Add(new Dropdown.OptionData("None"));

    }

    public List<JavaOMI> GetJavaOMISelected()
    {
        List<JavaOMI> result = new List<JavaOMI>();
        if (m_linked.options.Count <= 0) return result;
        string value = m_linked.options[m_linked.value].text;
       

        if (value.ToLower() == "none" || value.ToLower() == "-") return result;
        if (value.ToLower() != "all") {
            JavaOMI toadd = JavaOMI.GetRegistered(value);
            //Debug.Log("LO: " + value + " - " + toadd);
            if (toadd!=null)
            result.Add(toadd);
            return result;
        }

        foreach (string name in JavaOMI.GetAllRunningNameRegistered())
        {
            JavaOMI toadd = JavaOMI.GetRegistered(name);
           // Debug.Log("LA: " + name+" - "+toadd);
            if (toadd != null)
                result.Add(toadd);
        }
        return result;
    }

    void Reset() {
        m_linked = GetComponent<Dropdown>();
    }
}
