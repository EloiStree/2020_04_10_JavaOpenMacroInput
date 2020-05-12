using JavaOpenMacroInput;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConfigServerOMI : MonoBehaviour
{

    public JavaOMI m_deviceTarget;
    public InputField m_name;
    public Toggle m_autoStart;
    public UI_IpPart [] m_ip = new UI_IpPart[4];
    public InputField m_port;
    public Dropdown m_serverState;
    public Selectable[] m_buttons;
    public System.Threading.ThreadPriority m_threadPriority;
    private string m_previousName="";
    public bool m_saveOnHardrive;

    [Header("Debug")]
    public string m_resume = "";

    public string GetServerName() {
        if (m_name == null)
            return "";
        return m_name.text; }
    public string GetServerIp() {
        if (m_serverState == null)
            return "127.0.0.1";
        return string.Format("{0:0}.{1:0}.{2:0}.{3:0}", m_ip[0].GetIndex(), m_ip[1].GetIndex(), m_ip[2].GetIndex(), m_ip[3].GetIndex()); }
    public int GetServerPort() {  
        int port = 2501;
        int.TryParse(m_port.text, out port);
        return port;
    }

    void OnValidate() {

        m_resume = GetServerName()+"|"+GetServerIp() + ":" + GetServerPort();
    }

    void Awake()
    {
        LoadPrefData(m_saveOnHardrive);
        m_serverState.onValueChanged.AddListener(OnServerChange);
        //m_previousName = m_name.text;
        //m_name.onValueChanged.AddListener(NameChanged);

        if (m_autoStart.isOn)
            m_serverState.value = 1
;    }

    
    //private void NameChanged(string newName)
    //{
    //    bool find;
    //    JavaOMI.RenameRegistered(m_previousName, newName,out find);
    //    m_previousName = name;
    //}

    void OnDestroy() {

      //  m_name.onValueChanged.RemoveListener(NameChanged);
        m_serverState.onValueChanged.RemoveListener(OnServerChange);
        SavePrefData(m_saveOnHardrive);
    }


    private void OnServerChange(int arg0)
    {
        if (arg0 == 0)
        {

            // Stop
            if (m_deviceTarget != null) { 
                JavaOMI.UnregisterShortcut(m_name.text);
                m_deviceTarget.StopThread(); 

            }
            m_deviceTarget = null;
            SetInteractable(true);
        }
        else if (arg0 == 1 || arg0==2)
        {
            // Run
            if (m_deviceTarget == null)
            {
                m_deviceTarget = new JavaOMI(new JavaOpenMacroCommunicationProcess(GetServerIp(), GetServerPort(), m_threadPriority));
                if(!JavaOMI.IsServerRegistered(m_name.text))
                  JavaOMI.RegisterShortcut(m_name.text, m_deviceTarget, true);
            }
            m_deviceTarget.SetPause(arg0==2);
            SetInteractable(false);
        }
        SavePrefData(m_saveOnHardrive);
    }

    private void SetInteractable(bool isInteractable)
    {
        m_name.interactable = isInteractable;
        m_port.interactable = isInteractable;
        for (int i = 0; i < 4; i++)
        {
            m_ip[i].SetInteractable(isInteractable);
        }
        for (int i = 0; i < m_buttons.Length; i++)
        {
            if(m_buttons[i]!=null)
               m_buttons[i].interactable = isInteractable;

        }
    }

    public void PingServer()
    {
        m_deviceTarget.Keyboard(JavaKeyEvent.VK_P);
        m_deviceTarget.Keyboard(JavaKeyEvent.VK_I);
        m_deviceTarget.Keyboard(JavaKeyEvent.VK_N);
        m_deviceTarget.Keyboard(JavaKeyEvent.VK_G);
    }

    public void PastText(string txt) { 
         bool altered;
        m_deviceTarget.PastText(txt, out altered);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            SavePrefData(m_saveOnHardrive);
    }
    private void Reset() {

        if (m_ip.Length != 4)
            m_ip = new UI_IpPart[4];
        m_prefId = UnityEngine.Random.value * 100000f + "-" + UnityEngine.Random.value * 100000f;
    }

    public string m_prefId="";
    private void LoadPrefData(bool saveOnharddrive = true)
    {
        PreferenceSave toLoad = new PreferenceSave();
        
        string loaded = UnityDirectoryStorage.LoadFile("JavaOMI", m_prefId + ".txt", saveOnharddrive);
        if (loaded == "") 
            return;
        toLoad = JsonUtility.FromJson<PreferenceSave>(loaded);

        m_autoStart.isOn= toLoad.m_autoStart ;
        m_name.text = toLoad.m_threadName;
        m_port.text = "" + toLoad.m_port;
        m_ip[0].SetIndex(toLoad.m_ip[0]);
        m_ip[1].SetIndex(toLoad.m_ip[1]);
        m_ip[2].SetIndex(toLoad.m_ip[2]);
        m_ip[3].SetIndex(toLoad.m_ip[3]);
       
    }

    private void SavePrefData(bool saveOnharddrive = true)
    {
        PreferenceSave toSave = new PreferenceSave();
        toSave.m_autoStart = m_autoStart.isOn;
        toSave.m_threadName = GetServerName();
        toSave.m_port = GetServerPort();
        toSave.m_ip[0] = m_ip[0].GetIndex();
        toSave.m_ip[1] = m_ip[1].GetIndex();
        toSave.m_ip[2] = m_ip[2].GetIndex();
        toSave.m_ip[3] = m_ip[3].GetIndex();

        UnityDirectoryStorage.SaveFile("JavaOMI", m_prefId + ".txt", JsonUtility.ToJson(toSave), saveOnharddrive);
          
    }
 

    [System.Serializable]
    public class PreferenceSave {

        public bool m_autoStart=false;
        public string m_threadName="";
        public int [] m_ip = new int[] { 0,0,0,0};
        public int m_port=0;

    }
    
}
