using JavaOpenMacroInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GitCommandToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public void ApplyToCommandLine()
    {

        StartCoroutine(CoroutineApplyCmdInCommandLine(false, ""));
    }
    public void ApplyToCommandLine(string message)
    {

        StartCoroutine(CoroutineApplyCmdInCommandLine(false, message));
    }
    public void OpenWinRAndApplyToCommandLine()
    {

        StartCoroutine(CoroutineApplyCmdInCommandLine(true, ""));
    }



    public IEnumerator CoroutineApplyCmdInCommandLine(bool openWinR, string msg) {
        List<JavaOMI> targets = m_targets.GetJavaOMISelected();
        Debug.Log(">>>" + targets.Count);


       // yield return new WaitForSeconds(3f);
        foreach (var item in targets)
        {
            if (openWinR)
            {
              
                yield return new WaitForSeconds(0.2f);
                item.PastText("cmd");
                yield return new WaitForSeconds(0.2f);
                item.Keyboard(JavaKeyEvent.VK_ENTER, PressType.Stroke);
                yield return new WaitForSeconds(1f);


            }
            GitJOMI.Add(item);
            yield return new WaitForSeconds(0.5f);
            if (string.IsNullOrEmpty(msg))
                GitJOMI.Commit(item, DateTime.Now);
            else GitJOMI.Commit(item, msg);
            yield return new WaitForSeconds(0.5f);
            GitJOMI.Pull(item);
            yield return new WaitForSeconds(0.5f);
            GitJOMI.Push(item);
            yield return new WaitForSeconds(0.5f);
            GitJOMI.Status(item);
        }

        yield break;
    }

    public void Add()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            GitJOMI.Add(item);
    }
    public void Commit()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            GitJOMI.Commit(item, DateTime.Now);
    }
    public void Commit(string message)
    {

        StartCoroutine(CoroutineApplyCmdInCommandLine(false, message));
    }
    
    public void Push()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            GitJOMI.Push(item);
    }
    public void Pull()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            GitJOMI.Pull(item);
    }
    public void Status()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
            GitJOMI.Status(item);
    }
}

public class GitJOMI {

    public static void Add(JavaOMI target)
    {
        SendClassicCommand(target, "git add .");
    }
    public static void Commit(JavaOMI target, DateTime date, string format = "{0:r}") {
        if (date == null)
            date = DateTime.Now;
        Commit(target, string.Format(format, date));
    }
    public static void Commit(JavaOMI target, string message)
    {

        SendClassicCommand(target, "git commit -m \"" + message + "\"");
    }
    public static void Status(JavaOMI target)
    {
        SendClassicCommand(target, "git status");
    }
    public static void Pull(JavaOMI target)
    {
        SendClassicCommand(target, "git pull");
    }
    public static void Push(JavaOMI target)
    {
        SendClassicCommand(target, "git push");
    }
    public static void DisplayVersion(JavaOMI target)
    {
        SendClassicCommand(target, "git -version");
    }
    public static void SetName(JavaOMI target,string name)
    {
        SendClassicCommand(target, "git config --global user.name \""+name+"\"");
    }
    public static void SetMail(JavaOMI target, string mail)
    {
        SendClassicCommand(target, "git config --global user.email  \"" + mail + "\"");
    }
    public static void DisplayGitConfig(JavaOMI target)
    {
        SendClassicCommand(target, "git config --list");

    }
    public static void Init(JavaOMI target)
    {
        SendClassicCommand(target, "git init");
    }
    public static void Clone(JavaOMI target, string projectUrl)
    {
        SendClassicCommand(target, "git clone  " + projectUrl );
    }
    public static void SendClassicCommand(JavaOMI target,string cmdAsLine)
    {
        target.PastText(cmdAsLine);
        target.Keyboard(JavaKeyEvent.VK_ENTER, PressType.Stroke);
            
    }
}
