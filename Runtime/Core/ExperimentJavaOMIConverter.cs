using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JavaOpenMacroInput;

public class ExperimentJavaOMIConverter : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField m_editCommandsStorage;
    public ConvertTextToJavaKey[] m_converTableDefault = new ConvertTextToJavaKey[] { };
    public ConvertTextToJavaKey[] m_converTableCustom = new ConvertTextToJavaKey[] { };

    [System.Serializable]
    public class ConvertTextToJavaKey {
        public string m_regexToReplace;
        public JavaKeyEvent m_key;
        public PressType m_pressType;

        public ConvertTextToJavaKey(JavaKeyEvent key, PressType press, string value)
        {
            this.m_regexToReplace = value;
            this.m_key = key;
            this.m_pressType = press;
        }
    }
    [Header("Debug")]
    [TextArea(0, 10)]
    public string m_praseText = "";
    public string[] m_cmdsFound;

    public void Update()
    {
        TryToParse();

    }

    public void OnValidate()
    {
        TryToParse();
    }

    public void AppendAtStartText(string text) { m_editCommandsStorage.text = text + m_editCommandsStorage.text; }
    public void AppendAtEndText(string text) { m_editCommandsStorage.text += text; }

    public void AppendAtEndTextCommand(string text) {
        m_editCommandsStorage.text += "[[" + text + "]]";
    }

    public string[] TryToParse()
    {
        if (m_editCommandsStorage == null)
            return new string[0];
        return TryToParse(m_editCommandsStorage.text);
    }
    public string [] TryToParse(string text)
    {
        if (text == null || text.Length<=0)
            return new string [0];
        m_praseText = ParseTextToCMD(text);
        m_cmdsFound = GetJavaCommandsIn(m_praseText);
        return m_cmdsFound;
    }

    private string ParseTextToCMD(string text)
    {
        string result = " "+text.ToLower()+" ";

        result = result.Replace("↕", "↕ ");
        result = result.Replace("↓", "↓ ");
        result = result.Replace("↑", "↑ ");

        result = result.Replace(" ScrollUp↕ ".ToLower(), string.Format("[[wh:{0}]]", 1));
        result = result.Replace(" ScrollDown↕ ".ToLower(), string.Format("[[wh:{0}]]", -1));
        result = result.Replace(" ClickLeft↕ ".ToLower(), "[[ms:0]]");
        result = result.Replace(" ClickRight↕ ".ToLower(), "[[ms:2]]");
        result = result.Replace(" ClickScroll↕ ".ToLower(), "[[ms:1]]");
        result = result.Replace(" ClickLeft↓ ".ToLower(), "[[mp:0]]");
        result = result.Replace(" ClickLeft↑ ".ToLower(), "[[mr:0]]");
        result = result.Replace(" ClickRight↓ ".ToLower(), "[[mp:2]]");
        result = result.Replace(" ClickRight↑ ".ToLower(), "[[mr:2]]");
        result = result.Replace(" ClickScroll↓ ".ToLower(), "[[mp:1]]");
        result = result.Replace(" ClickScroll↑ ".ToLower(), "[[mr:1]]");


        ParseTextToCMDFrom(ref result, m_converTableCustom);
         ParseTextToCMDFrom(ref result, m_converTableDefault);


       


        return result;
    }

    private void ParseTextToCMDFrom(ref string text, ExperimentJavaOMIConverter.ConvertTextToJavaKey[] tableConversion)
    {
        foreach (var item in tableConversion)
        {
            if (!string.IsNullOrEmpty( item.m_regexToReplace )) { 
                char pressType = 's';
                if (item.m_pressType == PressType.Press)
                    pressType = 'p';
                if (item.m_pressType == PressType.Release)
                    pressType = 'r';
                text =text.Replace(" " + item.m_regexToReplace.ToLower() + " ", string.Format("[[k{0}:{1}]]", pressType, item.m_key.ToString()));
            }
        }
    }

    public string[] GetJavaCommandsIn(string text ) {
        string commandBorderRegex = "\\[\\[[^\\[\\]]+\\]\\]";

        List<string> cmds = new List<string>();
        foreach (Match match in Regex.Matches(text, commandBorderRegex))
        {
           cmds.Add(match.Value.Substring(2, match.Value.Length-4));
        }
        return cmds.ToArray();
    }
    public static String convertToString( Enum eff)
    {
        return Enum.GetName(eff.GetType(), eff);
    }

    public static EnumType converToEnum<EnumType>( String enumValue)
    {
        return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
    }
    public static List<string> GetDataSourceTypes<T>()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
    public static List<T> GetListOf<T>() { 
    return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
    public void Reset()
    {
        m_editCommandsStorage = GetComponent<InputField>();
        //↑ ↓ ↔ ↕
        m_converTableDefault = new ConvertTextToJavaKey[] {
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD0, PressType.Press   , "NP0↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD0, PressType.Release , "NP0↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD1, PressType.Press   , "NP1↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD1, PressType.Release , "NP1↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD2, PressType.Press   , "NP2↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD2, PressType.Release , "NP2↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD3, PressType.Press   , "NP3↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD3, PressType.Release , "NP3↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD4, PressType.Press   , "NP4↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD4, PressType.Release , "NP4↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD5, PressType.Press   , "NP5↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD5, PressType.Release , "NP5↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD6, PressType.Press   , "NP6↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD6, PressType.Release , "NP6↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD7, PressType.Press   , "NP7↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD7, PressType.Release , "NP7↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD8, PressType.Press   , "NP8↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD8, PressType.Release , "NP8↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD9, PressType.Press   , "NP9↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD9, PressType.Release , "NP9↑"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD0, PressType.Stroke , "NP0↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD1, PressType.Stroke , "NP1↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD2, PressType.Stroke , "NP2↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD3, PressType.Stroke , "NP3↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD4, PressType.Stroke , "NP4↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD5, PressType.Stroke , "NP5↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD6, PressType.Stroke , "NP6↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD7, PressType.Stroke , "NP7↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD8, PressType.Stroke , "NP8↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUMPAD9, PressType.Stroke , "NP9↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_0, PressType.Press   , "0↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_0, PressType.Release , "0↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_1, PressType.Press   , "1↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_1, PressType.Release , "1↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_2, PressType.Press   , "2↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_2, PressType.Release , "2↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_3, PressType.Press   , "3↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_3, PressType.Release , "3↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_4, PressType.Press   , "4↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_4, PressType.Release , "4↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_5, PressType.Press   , "5↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_5, PressType.Release , "5↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_6, PressType.Press   , "6↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_6, PressType.Release , "6↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_7, PressType.Press   , "7↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_7, PressType.Release , "7↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_8, PressType.Press   , "8↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_8, PressType.Release , "8↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_9, PressType.Press   , "9↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_9, PressType.Release , "9↑"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_0, PressType.Stroke , "0↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_1, PressType.Stroke , "1↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_2, PressType.Stroke , "2↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_3, PressType.Stroke , "3↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_4, PressType.Stroke , "4↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_5, PressType.Stroke , "5↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_6, PressType.Stroke , "6↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_7, PressType.Stroke , "7↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_8, PressType.Stroke , "8↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_9, PressType.Stroke , "9↕"),



        new ConvertTextToJavaKey(JavaKeyEvent.VK_PLUS, PressType.Press   , "+↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PLUS, PressType.Release , "+↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MINUS, PressType.Press   , "-↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MINUS, PressType.Release , "-↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MULTIPLY, PressType.Press   , "*↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MULTIPLY, PressType.Release , "*↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DIVIDE, PressType.Press   , "/↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DIVIDE, PressType.Release , "/↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_KP_DOWN, PressType.Press   , ".↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_KP_DOWN, PressType.Release , ".↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_UP, PressType.Press   , "Up↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_UP, PressType.Release , "Up↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_LEFT, PressType.Press   , "Left↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_LEFT, PressType.Release , "Left↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_RIGHT, PressType.Press   , "Right↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_RIGHT, PressType.Release , "Right↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DOWN, PressType.Press   , "Down↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DOWN, PressType.Release , "Down↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SPACE, PressType.Press   , "Space↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SPACE, PressType.Release , "Space↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CONTROL, PressType.Press   , "Ctrl↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CONTROL, PressType.Release , "Ctrl↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT, PressType.Press   , "Alt↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT, PressType.Release , "Alt↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT_GRAPH, PressType.Press   , "AltGr↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT_GRAPH, PressType.Release , "AltGr↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SHIFT, PressType.Press   , "Shift↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SHIFT, PressType.Release , "Shift↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CAPS_LOCK, PressType.Press   , "ShiftLock↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CAPS_LOCK, PressType.Release , "ShiftLock↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_TAB, PressType.Press   , "Tab↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_TAB, PressType.Release , "Tab↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ESCAPE, PressType.Press   , "Escape↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ESCAPE, PressType.Release , "Escape↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Press   , "Del↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Release , "Del↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Press   , "Delete↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Release , "Delete↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ENTER, PressType.Press   , "Enter↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ENTER, PressType.Release , "Enter↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_BACK_SPACE, PressType.Press   , "BackSpace↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_BACK_SPACE, PressType.Release , "BackSpace↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_WINDOWS, PressType.Press   , "Window↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_WINDOWS, PressType.Release , "Window↑"),





        new ConvertTextToJavaKey(JavaKeyEvent.VK_PLUS, PressType.Stroke   , "+↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MINUS, PressType.Stroke   , "-↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_MULTIPLY, PressType.Stroke   , "*↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DIVIDE, PressType.Stroke   , "/↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_KP_DOWN, PressType.Stroke , ".↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_UP, PressType.Stroke , "Up↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_LEFT, PressType.Stroke   , "Left↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_RIGHT, PressType.Stroke   , "Right↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DOWN, PressType.Stroke   , "Down↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SPACE, PressType.Stroke   , "Space↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CONTROL, PressType.Stroke   , "Ctrl↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT, PressType.Stroke   , "Alt↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ALT_GRAPH, PressType.Stroke   , "AltGr↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_SHIFT, PressType.Stroke   , "Shift↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_CAPS_LOCK, PressType.Stroke   , "ShiftLock↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_TAB, PressType.Stroke   , "Tab↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ESCAPE, PressType.Stroke , "Escape↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Stroke , "Del↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_DELETE, PressType.Stroke , "Delete↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_ENTER, PressType.Stroke , "Enter↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_BACK_SPACE, PressType.Stroke , "BackSpace↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_WINDOWS, PressType.Stroke , "Window↕"),




        new ConvertTextToJavaKey(JavaKeyEvent.VK_A, PressType.Press   , "A↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_A, PressType.Release , "A↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_B, PressType.Press   , "B↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_B, PressType.Release , "B↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_C, PressType.Press   , "C↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_C, PressType.Release , "C↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_D, PressType.Press   , "D↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_D, PressType.Release , "D↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_E, PressType.Press   , "E↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_E, PressType.Release , "E↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F, PressType.Press   , "F↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F, PressType.Release , "F↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_G, PressType.Press   , "G↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_G, PressType.Release , "G↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_H, PressType.Press   , "H↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_H, PressType.Release , "H↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_I, PressType.Press   , "I↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_I, PressType.Release , "I↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_J, PressType.Press   , "J↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_J, PressType.Release , "J↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_K, PressType.Press   , "K↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_K, PressType.Release , "K↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_L, PressType.Press   , "L↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_L, PressType.Release , "L↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_M, PressType.Press   , "M↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_M, PressType.Release , "M↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_N, PressType.Press   , "N↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_N, PressType.Release , "N↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_O, PressType.Press   , "O↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_O, PressType.Release , "O↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_P, PressType.Press   , "P↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_P, PressType.Release , "P↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Q, PressType.Press   , "Q↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Q, PressType.Release , "Q↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_R, PressType.Press   , "R↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_R, PressType.Release , "R↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_S, PressType.Press   , "S↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_S, PressType.Release , "S↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_T, PressType.Press   , "T↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_T, PressType.Release , "T↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_U, PressType.Press   , "U↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_U, PressType.Release , "U↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_V, PressType.Press   , "V↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_V, PressType.Release , "V↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_W, PressType.Press   , "W↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_W, PressType.Release , "W↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_X, PressType.Press   , "X↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_X, PressType.Release , "X↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Y, PressType.Press   , "Y↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Y, PressType.Release , "Y↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Z, PressType.Press   , "Z↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Z, PressType.Release , "Z↑"),





        new ConvertTextToJavaKey(JavaKeyEvent.VK_A, PressType.Stroke , "A↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_B, PressType.Stroke , "B↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_C, PressType.Stroke , "C↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_D, PressType.Stroke , "D↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_E, PressType.Stroke , "E↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F, PressType.Stroke , "F↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_G, PressType.Stroke , "G↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_H, PressType.Stroke , "H↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_I, PressType.Stroke , "I↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_J, PressType.Stroke , "J↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_K, PressType.Stroke , "K↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_L, PressType.Stroke , "L↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_M, PressType.Stroke , "M↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_N, PressType.Stroke , "N↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_O, PressType.Stroke , "O↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_P, PressType.Stroke , "P↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Q, PressType.Stroke , "Q↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_R, PressType.Stroke , "R↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_S, PressType.Stroke , "S↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_T, PressType.Stroke , "T↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_U, PressType.Stroke , "U↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_V, PressType.Stroke , "V↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_W, PressType.Stroke , "W↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_X, PressType.Stroke , "X↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Y, PressType.Stroke , "Y↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_Z, PressType.Stroke , "Z↕"),



        new ConvertTextToJavaKey(JavaKeyEvent.VK_F1, PressType.Press   , "F1↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F1, PressType.Release , "F1↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F2, PressType.Press   , "F2↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F2, PressType.Release , "F2↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F3, PressType.Press   , "F3↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F3, PressType.Release , "F3↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F4, PressType.Press   , "F4↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F4, PressType.Release , "F4↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F5, PressType.Press   , "F5↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F5, PressType.Release , "F5↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F6, PressType.Press   , "F6↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F6, PressType.Release , "F6↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F7, PressType.Press   , "F7↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F7, PressType.Release , "F7↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F8, PressType.Press   , "F8↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F8, PressType.Release , "F8↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F9, PressType.Press   , "F9↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F9, PressType.Release , "F9↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F10, PressType.Press   , "F10↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F10, PressType.Release , "F10↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F11, PressType.Press   , "F11↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F11, PressType.Release , "F11↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F12, PressType.Press   , "F12↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F12, PressType.Release , "F12↑"),


        new ConvertTextToJavaKey(JavaKeyEvent.VK_F1,  PressType.Stroke ,   "F1↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F2,  PressType.Stroke ,   "F2↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F3,  PressType.Stroke ,   "F3↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F4,  PressType.Stroke ,   "F4↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F5,  PressType.Stroke ,   "F5↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F6,  PressType.Stroke ,   "F6↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F7,  PressType.Stroke ,   "F7↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F8,  PressType.Stroke ,   "F8↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F9,  PressType.Stroke ,   "F9↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F10, PressType.Stroke , "F10↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F11, PressType.Stroke , "F11↕"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_F12, PressType.Stroke , "F12↕"),


        new ConvertTextToJavaKey(JavaKeyEvent.VK_INSERT, PressType.Press   , "Insert↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_INSERT, PressType.Release , "Insert↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_INSERT,  PressType.Stroke ,  "Insert↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_HOME, PressType.Press   , "Home↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_HOME, PressType.Release , "Home↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_HOME,  PressType.Stroke ,  "Home↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_DOWN, PressType.Press   , "PageDown↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_DOWN, PressType.Release , "PageDown↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_DOWN,  PressType.Stroke ,  "PageDown↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_UP, PressType.Press   , "PageUp↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_UP, PressType.Release , "PageUp↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAGE_UP,  PressType.Stroke , "PageUp↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAUSE, PressType.Press   , "Pause↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAUSE, PressType.Release , "Pause↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_PAUSE,  PressType.Stroke , "Pause↕"),

        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUM_LOCK, PressType.Press   , "NumLock↓"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUM_LOCK, PressType.Release , "NumLock↑"),
        new ConvertTextToJavaKey(JavaKeyEvent.VK_NUM_LOCK,  PressType.Stroke , "NumLock↕"),

    };
    }
}
