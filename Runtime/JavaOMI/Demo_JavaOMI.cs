using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JavaOpenMacroInput;

public class Demo_JavaOMI : MonoBehaviour
{
    public JavaOMI deviceTarget;
    public JavaKeyEvent[] m_toSend = new JavaKeyEvent[] {JavaKeyEvent.VK_O, JavaKeyEvent.VK_K };
    public float m_delayTimeBetweenKey = .1f;

    public TextAsset [] m_textToPast;
    public float m_delayTimeBetweenText = .5f;
    public int m_textSpliteLenght = 10;
    [Header("Debug")]
    public bool m_mayHaveAlteration;
    IEnumerator Start()
    {
        deviceTarget = JavaOMI.CreateDefaultOne();

        //while (true) {
            yield return new WaitForSeconds(3);
            for (int i = 0; i < m_toSend.Length; i++)
            {
                deviceTarget.Keyboard(m_toSend[i]);
                yield return new WaitForSeconds(m_delayTimeBetweenKey);

            }
            yield return new WaitForSeconds(3);
            for (int i = 0; i < m_textToPast.Length; i++)
            {
            if (m_textToPast[i] != null) {
                bool alteration;            
                deviceTarget.PastText(m_textToPast[i].text, out alteration);
                m_mayHaveAlteration |= alteration ;
            }
                yield return new WaitForSeconds(m_delayTimeBetweenText);

            }

        //}

    }
    public void Reset()
    {
        JavaOMI.TryBasicDirtyConvertion("Hello World", out m_toSend);
    }

}
