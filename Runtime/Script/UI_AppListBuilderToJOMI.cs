using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_AppListBuilderToJOMI : MonoBehaviour , I_TextSavable
{
    public UI_ServerDropdownJavaOMI m_targets;
    public InputField m_builderInput;
    public List<AppStored > m_appStored = new List<AppStored>();
    public ApplicationsNamesEvent m_onChanged;

    [System.Serializable]
    public class AppStored
    {
        public string m_name = "";
        public string m_appPath = "";
        public Type m_type;
        public enum Type { Path, STARTUP}
    }

    public void ParseText(string txt) {
        string[] lines = txt.Split('\n');
        m_appStored.Clear();
        for (int i = 0; i < lines.Length; i++)
        {
            AppStored app = new AppStored();
            bool isFound = false;
            string line = lines[i];
            int index=0;
            index = line.ToLower().IndexOf("||path||".ToLower());
            if ( index >= 0)
            {
                app.m_name = line.Substring(0, index).Trim();
                app.m_appPath = line.Substring(index+8).Trim();
                app.m_type = AppStored.Type.Path;
                isFound = true;
            }
            index = line.ToLower().IndexOf("||startup||".ToLower());
            if (index >= 0)
            {
                app.m_name = line.Substring(0, index).Trim();
                app.m_appPath = line.Substring(index + 11).Trim();
                app.m_type = AppStored.Type.STARTUP;
                isFound = true;

            }
            if (!isFound) {
                app.m_name = app.m_appPath = line.Trim();
                app.m_type = AppStored.Type.STARTUP;
                isFound = true;
            }
            if (line.Trim().Length > 0) {
                m_appStored.Add(app);
            }
        }
        m_onChanged.Invoke(m_appStored.Select(k => k.m_name).ToArray());
    }


    public void ResetExample(bool withNotification) {
        string t = GetSavableDefaultText();
        if (withNotification)
            m_builderInput.text = t;
        else m_builderInput.SetTextWithoutNotify(t);
    }

    public void LaunchApplicationFromName(string appName) {

        List<AppStored> found = 
            m_appStored.Where(k => k.m_name.Trim().ToLower() 
            == appName.Trim().ToLower()  ).ToList();
        if (found.Count > 0) {
            AppStored app= found[0];
            foreach (var item in m_targets.GetJavaOMISelected())
            {
                if (app.m_type == AppStored.Type.Path)
                {
                    JavaOMI.Window.OpenExePath(item , app.m_appPath);
                }
                else if (app.m_type == AppStored.Type.STARTUP)
                {
                    JavaOMI.Window.OpenLnkFromStartup(item , app.m_appPath);

                }

            }
            
        }
    }

    public void OpenStartUpFolder() {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            JavaOMI.Window.GoToStartup(item);
        }
    }

    public string GetSavableText()
    {
        return m_builderInput.text;
    }

    public void SetTextFromLoad(string text)
    {
        m_builderInput.text = text;
        ParseText(m_builderInput.text);
    }

    public string GetSavableDefaultText()
    {
        return "Audacity||STARTUP||Audacity\n"
            + "Google Chrome||STARTUP||Google Chrome\n"
            + "Adobe Photoshop 2020\n"
            + "Visual Studio 2019\n"
            + "Steam||STARTUP||Steam\\Steam\n"
            + "OBS||STARTUP||OBS Studio\\OBS Studio (32bit)\n"
            + "Media Player||PATH||C:\\Program Files\\Windows Media Player\\wmplayer.exe\n";
    }

    [System.Serializable]
    public class ApplicationsNamesEvent : UnityEvent<string[]> { }
}
