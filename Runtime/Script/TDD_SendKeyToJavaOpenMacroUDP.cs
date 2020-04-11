using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_SendKeyToJavaOpenMacroUDP : MonoBehaviour
{
    public JavaOpenMacroUDP m_udpSender;
    private void Start()
    {
        InvokeRepeating("TDD_SendMSG", 0, 2);
      
    }
    public void TDD_SendMSG()
    {
        m_udpSender.Send((JavaKeyEvent)UnityEngine.Random.Range(0, 80), JavaOpenMacroUDP.PressType.Stroke);
    }
}
