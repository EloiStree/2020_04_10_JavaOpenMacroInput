using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIToRunningJavaOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_serverSelected;
    


    public void StrokeKey(JavaKeyEvent key) {
        foreach (JavaOMI device in m_serverSelected.GetJavaOMISelected())
        {
            device.Keyboard(key);
        }
    }

    public void Past(string text) {
        foreach (JavaOMI device in m_serverSelected.GetJavaOMISelected())
        {
            bool guaranti;
            device.PastText(text,out guaranti);
        }
    }
    //public void PlayMusic() {
    //    foreach (JavaOMI device in m_serverSelected.GetJavaOMISelected())
    //    {
    //        device.Keyboard(JavaKeyEvent.VK_PAUSE);
    //        device.Keyboard(JavaKeyEvent.VK_PRINTSCREEN);

    //        device.Keyboard(JavaKeyEvent.VK_WINDOWS);
    //    }
    //}
    //public void Demo_CmdCommit() {
    //    foreach (JavaOMI device in m_serverSelected.GetJavaOMISelected())
    //    {
    //        bool garanti;
    //        device.MouseClick(JavaMouseButton.BUTTON1_DOWN_MASK);
    //        device.PastText("cmd", out garanti);
    //        device.Keyboard(JavaKeyEvent.VK_ENTER);
    //        Thread.Sleep(1);
    //        device.PastText("git add .", out garanti);
    //        device.Keyboard(JavaKeyEvent.VK_ENTER);
    //        device.PastText("git commit -m  \" Hey :) \"", out garanti);
    //        device.Keyboard(JavaKeyEvent.VK_ENTER);

    //        device.Keyboard(JavaKeyEvent.VK_WINDOWS);
    //        //Alt F4
    //    }
    //}

}
