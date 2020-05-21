using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MDTabletToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public InputField m_col;
    public InputField m_line;

    public void PushTable() {
        string txt = "";
        string line = "";
        List<string> elementInLine = new List<string>();

        int colValue=1, lineValue=1;
        if (!int.TryParse(m_col.text, out colValue))
            colValue = 1;
        if (!int.TryParse(m_line.text, out lineValue))
            lineValue = 1;

        for (int i = 0; i < lineValue; i++)
        {
           
            if (i == 0)
            {
                elementInLine.Clear();
                line = "|";
                for (int j = 0; j < colValue; j++)
                {
                    elementInLine.Add(string.Format("t{0}", j));
                }
                line += string.Join(" | ", elementInLine);
                line += "|\n";
                txt += line;

                elementInLine.Clear();
                line = "|";
                for (int j = 0; j < colValue; j++)
                {
                    elementInLine.Add(string.Format(":---:", j));
                }
                line += string.Join(" | ", elementInLine);
                line += "|\n";
                txt += line;
            }
          else
            {
                elementInLine.Clear();
                line = "|";
                for (int j = 0; j < colValue; j++)
                {
                    elementInLine.Add(string.Format("l{0}c{1}", i, j));
                }
                line += string.Join(" | ", elementInLine);
                line += "|\n";
                txt += line;
            }

        }

        foreach (var item in m_targets.GetJavaOMISelected())
        {
           item.PastText(txt);
        }
    }
}
