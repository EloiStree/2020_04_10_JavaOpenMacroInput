using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JavaOpenMacroInput;

public class Demo_JavaOMI : MonoBehaviour
{
    public JavaOMI deviceTarget;
    public JavaKeyEvent[] m_toSend = new JavaKeyEvent[] {JavaKeyEvent.VK_O, JavaKeyEvent.VK_K };
    public float m_delayTime = .1f;

    IEnumerator Start()
    {
        deviceTarget = JavaOMI.CreateDefaultOne();

        while (true) {
            yield return new WaitForSeconds(3);
            for (int i = 0; i < m_toSend.Length; i++)
            {
                deviceTarget.Keyboard(m_toSend[i]);
                yield return new WaitForSeconds(m_delayTime);

            }
        }
    }
    public void Reset()
    {
        JavaOMI.TryBasicDirtyConvertion("Hello World", out m_toSend);
    }

}
