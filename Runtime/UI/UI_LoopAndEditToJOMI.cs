using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_LoopAndEditToJOMI : MonoBehaviour
{

    public UI_InputFieldToJOMI m_inputFieldToJOMI;
    public Dropdown m_commandType;
    public InputField m_loopTime;
    UnityAction m_pushAction;
    public bool m_useDebug;
    public void Awake()
    {
        m_pushAction = new UnityAction(Push);
        ChangeLoopTime(m_loopTime.text);
        int id = 0;
        switch (m_inputFieldToJOMI.m_textType)
        {
            case UI_ItemWithDrowdownToJOMI.TypeOfText.Command: id = 0; break;
            case UI_ItemWithDrowdownToJOMI.TypeOfText.Shortcuts: id = 1; break;
            case UI_ItemWithDrowdownToJOMI.TypeOfText.CopyPast: id = 2; break;
        }
        m_commandType.ClearOptions();
        m_commandType.AddOptions(new string[] { "Command","Shortcuts","Copy Past"}.ToList());
        m_commandType.value = id;
        StartListening();
    }
    public void Start()
    {
        StopListening();
        StartListening();
    }
    public void Push() {
        string t = m_inputFieldToJOMI.GetText();
        if(m_useDebug)
        Debug.Log("LOOP Push:"+t);
        switch (m_commandType.value)
        {
            case 0: m_inputFieldToJOMI.PushText(t, UI_ItemWithDrowdownToJOMI.TypeOfText.Command); break;
            case 1: m_inputFieldToJOMI.PushText(t, UI_ItemWithDrowdownToJOMI.TypeOfText.Shortcuts); break;
            default: m_inputFieldToJOMI.PushText(t, UI_ItemWithDrowdownToJOMI.TypeOfText.CopyPast); break;
        }
    }

    private void OnDisable()
    {

        StopListening();
        StartListening();
    }
    private void OnDestroy()
    {
        StopListening();
        StartListening();
    }

    public void StartListening()
    {
        m_loopTime.onValueChanged.RemoveListener(ChangeLoopTime);
        m_loopTime.onValueChanged.AddListener(ChangeLoopTime);
    }
    public void StopListening() {
        m_loopTime.onValueChanged.RemoveListener(ChangeLoopTime);
    }

    public void ChangeLoopTime(string arg0)
    {
        float value = 0;
        float.TryParse(arg0, out value);
        LooperUtilityMono.SetTime( value, m_pushAction);
    }

    public void SetLoopActive(bool value) {

        LooperUtilityMono.SetLoopActiveState(value, m_pushAction);
    }
   
}

public class LooperUtilityMono : MonoBehaviour {

    public List<LoopState> m_loops = new List<LoopState>();


    public void Update()
    {

        for (int i = 0; i < m_loops.Count; i++)
        {
            if (m_loops[i].m_isActive) { 
                m_loops[i].m_currentTimer -= Time.deltaTime;
            }
            if (m_loops[i].m_isActive && m_loops[i].m_currentTimer <= 0)
            {
                m_loops[i].m_currentTimer = m_loops[i].m_loopTime;
                m_loops[i].m_actionToPush.Invoke();
            }

        }

    }

    [System.Serializable]
    public class LoopState {
        public bool m_isActive;
        public float m_loopTime;
        public float m_currentTimer;
        public UnityAction m_actionToPush;

        public void SetLoopTime(float loopTime)
        {
            m_loopTime = loopTime;
        }

        public void SetLoopActiveState(bool activateLoop)
        {
            m_isActive = activateLoop;
        }


    }
    public Dictionary<string, LoopState> m_loopRegister = new Dictionary<string, LoopState>();
    public static LooperUtilityMono m_instanceInScene;
    private void Awake()
    {
        m_instanceInScene = this;
    }
    public static void SetTime(float loopTime, UnityAction unityAction)
    {
        LoopState ls = GetLoopState(unityAction);
        if (ls == null)
            return;
        ls.SetLoopTime(loopTime);

    }
    public static void SetLoopActiveState(bool activateLoop, UnityAction unityAction)
    {
        LoopState ls = GetLoopState(unityAction);
        if (ls == null)
            return;
        ls.SetLoopActiveState(activateLoop);
    }

    private static LoopState GetLoopState(UnityAction unityAction)
    {
        if (unityAction == null) return null;
        LooperUtilityMono i = m_instanceInScene;
        if (m_instanceInScene == null)
        {
            GameObject go = new GameObject("LoopUtilityMono");
            m_instanceInScene = go.AddComponent<LooperUtilityMono>();
            i = m_instanceInScene;
        }
        if (i == null) return null;
        string id = ""+unityAction.GetHashCode();
        if (!i.m_loopRegister.ContainsKey(id)) {
            LoopState ls = new LoopState() { m_actionToPush = unityAction };
            i.m_loops.Add(ls);
            i.m_loopRegister.Add(id, ls);
        }
        return i.m_loopRegister[id];
    }
}