using JavaOpenMacroInput;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrollToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;
    public InputField m_usersName;
    public InputField m_linesToSend;
    public JavaKeyEvent m_chatInput = JavaKeyEvent.VK_ENTER;

    [SerializeField] string[] m_names;
    [SerializeField] string[] m_lines;
    [SerializeField] float m_minTimeBetweenPush=5;
    [SerializeField] float m_maxTimeBetweenPush=50;
    [SerializeField] bool m_isOn;

    private void Start()
    {
    
        Refresh();
        StartCoroutine(Loop());
    }
    private void OnEnable()
    {
     
        Refresh();
        StartCoroutine(Loop());
    }
    private void OnDisable()
    {
        Refresh();
        StopCoroutine(Loop());
   
    }

    public void SetOnOff(bool value) {
        m_isOn = value;
    }
    public void SetMinTime(float value) { m_minTimeBetweenPush = value; }
    public void SetMaxTime(float value) { m_maxTimeBetweenPush = value; }
    public void SetMinTime(string value) {
        float v = 0;
        if (float.TryParse(value, out v))
            SetMinTime(v);
    }
    public void SetMaxTime(string value) {
        float v = 0;
        if (float.TryParse(value, out v))
            SetMaxTime(v);
    }
    public void Refresh()
    {
        m_names = m_usersName.text.Split(',');
        m_lines = m_linesToSend.text.Split('\n');
        m_lines = Randomize<string>(m_lines).ToArray();
    }
    private IEnumerator Loop()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (m_isOn) { 
                yield return new WaitForSeconds(UnityEngine.Random.Range(m_minTimeBetweenPush, m_maxTimeBetweenPush));
                if (m_isOn) { 
                    PushRandomTroll();
                }
            }
        }
    }

    public void PushRandomTroll()
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.Keyboard(m_chatInput);
            item.PastText(GetRandomTroll());
            item.Keyboard(m_chatInput);
        }

    }

    private int m_index;
    private string GetRandomTroll()
    {
        string name = "";
        if(m_names.Length>0)
            name= m_names[UnityEngine.Random.Range(0, m_names.Length)];
        string line = "";
        if (m_index<m_lines.Length )
            line = m_lines[m_index];
        m_index ++;
        if (m_index >= m_lines.Length)
        { 
            m_index = 0;
            m_lines= Randomize<string>(m_lines).ToArray();
        }
        return line.Replace("#USERNAME#", name).Replace("#USER#", name).Replace("#NAME#", name);
    }

    public void  ResetToDefaultExample()
    {
        m_usersName.text = "Patate,Legend23,Leroy";
        m_linesToSend.text = "\nGo uninstall you fag #USER#" +
            "\nHow do you do to play so badly #USER# ?" +
            "\nDo you like to get fucked #USERNAME# ?" +
            "\nJesus Chris, what the fuck are you doing #NAME# ?";
    }
    public void AppendAtEnd(string text) { m_linesToSend.text += text; }
    public void AppendAtStart(string text) { m_linesToSend.text = text  + m_linesToSend.text;}

    public static IEnumerable<T> Randomize<T>( IEnumerable<T> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

   
}
