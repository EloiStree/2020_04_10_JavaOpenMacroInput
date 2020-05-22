using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CmdToJOMI : MonoBehaviour
{
    UI_CMDToJOMI m_cmdToTest;
    public UI_ServerDropdownJavaOMI m_targets;
    public ControlPanelEnum m_panelToOpen;
     ControlPanelEnum m_panelToOpenPrevious;

    IEnumerator Start()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            bool test = false;
            if (test) {
                StartCoroutine(JavaOMI.Window.GoToHotspotConfig(item, 2.5f, 1f));
                yield return new WaitForSeconds(5);
                StartCoroutine(JavaOMI.Window.GoToDeveloperConfig(item));
                yield return new WaitForSeconds(5);
                StartCoroutine(JavaOMI.Window.WindowSearchAndValidate(item, "utorrent"));

                JavaOMI.Window.OpenNewCommandShell(item);
                JavaOMI.Window.OpenNewCommandWindow(item);
            }
            //JavaOMI.Window.CtrlAltDelete(item);
            //JavaOMI.Window.TaskManager(item);
            //JavaOMI.Window.SwitchKeyboardLayout(item);
            //JavaOMI.Window.VirtualDesktop.Create(item);
            yield return new WaitForSeconds(5);
           
           

            item.CombineStroke("ctrl + space");
            item.CombineStroke("ctrl + space");
            item.CombineStroke("alt + NP1 + NP2");
           // item.CombineStroke("ctrl + alt + F4");
            item.CombineStroke("ctrl + k ", "ctrl + c ");
            item.CombineStroke("ctrl + k ", "ctrl + u ");
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying && (m_panelToOpenPrevious!= m_panelToOpen)) {
            m_panelToOpenPrevious = m_panelToOpen;
            foreach (var item in m_targets.GetJavaOMISelected())
            {
                JavaOMI.Window.OpenConfigPanel(item, m_panelToOpen);
            }

        }
    }
}
