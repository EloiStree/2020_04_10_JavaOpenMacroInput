using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CMDToJOMI : MonoBehaviour
{

    public UI_ServerDropdownJavaOMI m_targets;




    public void SearchAndValidate(string text) {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            JavaOmiCoroutine.Start( JavaOMI.Window.WindowSearchAndValidate(item, text));
        }
    }


    public void OpenExe(string applicationNameWithExe)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            JavaOMI.Window.OpenExePath(item,applicationNameWithExe);
        }
    }
    public void OpenDefault_VirtualKeyboard()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.OpenDefaultApplication(item, JavaOMI.Window.DefaultWindowApp.VirtualKeyboard);
    }
    public void OpenDefault_CMD()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.OpenDefaultApplication(item, JavaOMI.Window.DefaultWindowApp.CMD);

    }
    public void OpenDefault_Calculator()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.OpenDefaultApplication(item, JavaOMI.Window.DefaultWindowApp.Calculatrice);

    }
    public void ShutdownItReallyDoBeWorry()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.CallShutdown(item);

    }
    public void OpenScreenCaptureTool()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.CallCaptureScreen(item);
    }

    public void GoToAppData()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.GoToAppData(item);
    }
    public void GoToProg32Bit()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.GoToProg86(item);
    }
    public void GoToProg64Bit()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.GoToProg(item);
    }
    public void LockComputer()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.LockComputer(item);
    }

    public void TakeSceenshot()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.TakeScreenshot(item);
    }
    public void GoToUserDirectory(string relativePathInUserDir)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            JavaOMI.Window.GoToUserDirectory(item, relativePathInUserDir);
    }
    public void GoToUserDocument()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserDocument(item); };
    }
    public void GoToUserVideo()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserVideo(item); };
    }
    public void GoToUserMusic()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserMusic(item); };
    }
    public void GoToUserDesktop()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserDesktop(item); };
    }
    public void GoToUserDownload()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserDownload(item); };
    }
  
    public void GoToUserImage()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserImage(item); };
    }
    public void GoToUserScreenshots()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToUserScreenshots(item); };
    }
    public void GoToControlPanel()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) { JavaOMI.Window.GoToControlPanel(item); };
    }
    public void OpenControlCommandDocumentation()
    {
        foreach (var item in m_targets.GetJavaOMISelected()) {
            item.OpenUrl("https://ss64.com/nt/");
        };

        
    }
    //https://www.digitaltrends.com/computing/how-to-use-command-prompt/
    public void OpenControlPanel()
    {
        //https://www.tenforums.com/tutorials/86339-list-commands-open-control-panel-items-windows-10-a.html
        //https://www.lifewire.com/command-line-commands-for-control-panel-applets-2626060
        ControlPanelEnum enumPanel = ControlPanelEnum.Hotspot; 
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            switch (enumPanel) {
                case ControlPanelEnum.Hotspot: 
                JavaOMI.Window.GoToUserScreenshots(item);
                    break;

                default: break;
            }
        };
    }



   
}
