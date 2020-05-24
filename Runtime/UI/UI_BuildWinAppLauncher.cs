using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildWinAppLauncher : MonoBehaviour, I_TextSavable
{
    //    List<string> m_
    public UI_ServerDropdownJavaOMI m_targets;

    public InputField m_linkedInputField;
    public void SetFrom(string text) {

       throw new System.NotImplementedException("I lose the code somewhere in the project... I need to go back in my commits to find it back");
    }

    public void CallApplicationByName(string name) {
        Debug.Log(">W:" + name);
        foreach (var item in m_targets.GetJavaOMISelected())
        {
          
          JavaOmiCoroutine.Start(  JavaOMI.Window.WindowSearchAndValidate(item,name));
        }
    }

    public string GetSavableText()
    {
        return m_linkedInputField.text;
    }

    public string GetSavableDefaultText()
    {
        return m_linkedInputField.text;
    }

    public void SetTextFromLoad(string text)
    {
        m_linkedInputField.text = text ;
    }
}
