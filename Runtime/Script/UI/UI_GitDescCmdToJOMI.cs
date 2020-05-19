using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GitDescCmdToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public InputField m_descriptionField;
    public InputField m_gitCmdField;
    [TextArea(1,3)]
    public string m_description;
    [TextArea(1, 1)]
    public string m_gitCommand;
    public bool m_enterValidation=true;


    public void Push() {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.PastText(m_gitCmdField.text);
            if(m_enterValidation)   
                item.Keyboard(JavaOpenMacroInput.JavaKeyEvent.VK_ENTER, JavaOpenMacroInput.PressType.Stroke);

        }
    }
    public void ResetText() {
        m_gitCmdField.SetTextWithoutNotify(m_gitCommand);
    }

    private void OnValidate()
    {
        m_descriptionField.SetTextWithoutNotify(m_description);
        m_gitCmdField.SetTextWithoutNotify(m_gitCommand);
    }
}
