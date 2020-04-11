using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JavaOpenMacroInput;
public class TDD_SendKeyToJavaOpenMacroUDP : MonoBehaviour
{
    public JavaOpenMacroUDPMonoLauncher m_udpSender;
    public JavaKeyEvent[] m_keys = new JavaKeyEvent[] { JavaKeyEvent.VK_R, JavaKeyEvent.VK_C, JavaKeyEvent.VK_CONTROL };
    public JavaMouseButton[] m_mousePression = new JavaMouseButton[] { JavaMouseButton.BUTTON1_DOWN_MASK, JavaMouseButton.BUTTON2_DOWN_MASK };
    public float m_timeBetween=2;
    IEnumerator  Start()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetween);

            if(m_udpSender.m_process!=null)
            m_udpSender.m_process.Send(m_keys[UnityEngine.Random.Range(0, m_keys.Length)], PressType.Stroke);

            yield return new WaitForSeconds(m_timeBetween);
            if (m_udpSender.m_process != null)
                m_udpSender.m_process.Send(m_keys[UnityEngine.Random.Range(0, m_keys.Length)], PressType.Stroke);

            yield return new WaitForSeconds(m_timeBetween);
            if (m_udpSender.m_process != null)
                m_udpSender.m_process.SendWheel(UnityEngine.Random.Range(-3, 3));
            yield return new WaitForSeconds(m_timeBetween);
            if (m_udpSender.m_process != null)
                m_udpSender.m_process.SendMoveMousePosition(UnityEngine.Random.Range(0, 1900), UnityEngine.Random.Range(0, 1080));
        }
      
    }
    public void TDD_SendMSG()
    {
    }
}
