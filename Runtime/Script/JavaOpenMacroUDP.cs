using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class JavaOpenMacroUDP : MonoBehaviour
{
    public string m_ip = "127.0.0.1";
    public int m_port = 2510;
    public Queue <string> m_toSend = new Queue<string>();
    public int m_leftToSend = 0;
    public string m_lastSenD;
    public string m_exceptionDebug;
    public bool m_keepThreadAlive=true;

    private void OnDisable()
    {
        m_keepThreadAlive = false;
    }
    public enum PressType { Press, Release, Stroke}
    public void Send(string msg)
    {
        m_toSend.Enqueue(msg);
    }
    public void Send(JavaKeyEvent keyToType, PressType press )
    {
        string instruction = "ks:";
        if (press == PressType.Press) instruction = "kp:";
        if (press == PressType.Release) instruction = "kr:";
        m_toSend.Enqueue(instruction + keyToType.ToString());
    }
    private void Start()
    {
        Thread t = new Thread(SendToJavaOpenMacro);
        t.Priority = System.Threading.ThreadPriority.Lowest;
        t.Start();
    }
  

    private void SendToJavaOpenMacro() {
        SendToJavaOpenMacro(m_ip, m_port);
    }
    private void SendToJavaOpenMacro(string ip, int port)
    {
        UdpClient udpClient = new UdpClient(ip, port);
        Byte[] sendBytes= new Byte[0];
        while (m_keepThreadAlive) {
            if (m_toSend.Count > 0) {
                string msg = m_toSend.Dequeue();
                m_lastSenD = msg;
                sendBytes = Encoding.ASCII.GetBytes(msg);
                try
                {
                    udpClient.Send(sendBytes, sendBytes.Length);
                }
                catch (Exception e)
                {
                        m_exceptionDebug=e.ToString();
                }
                Thread.Sleep(50);
            }
            Thread.Sleep(50);
        }
        sendBytes = Encoding.ASCII.GetBytes("stop");
        try
        {
            udpClient.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            m_exceptionDebug = e.ToString();
        }

    }

   
}
public enum JavaKeyEvent : int
{
    VK_A, VK_B, VK_C, VK_D, VK_E, VK_F, VK_G, VK_H, VK_I, VK_J, VK_K, VK_L, VK_M, VK_N, VK_O, VK_P, VK_Q, VK_R, VK_S, VK_T, VK_U, VK_V, VK_W, VK_X, VK_Y, VK_Z,
    VK_ENTER,
    VK_BACK_SPACE,
    VK_TAB,
    VK_SHIFT,
    VK_CONTROL,
    VK_ALT,
    VK_PAUSE,
    VK_CAPS_LOCK,
    VK_ESCAPE,
    VK_SPACE,
    VK_PAGE_UP,
    VK_PAGE_DOWN,
    VK_END,
    VK_HOME,
    VK_LEFT,
    VK_UP,
    VK_RIGHT,
    VK_DOWN,
    VK_0,
    VK_1,
    VK_2,
    VK_3,
    VK_4,
    VK_5,
    VK_6,
    VK_7,
    VK_8,
    VK_9,
    VK_NUMPAD0,
    VK_NUMPAD1,
    VK_NUMPAD2,
    VK_NUMPAD3,
    VK_NUMPAD4,
    VK_NUMPAD5,
    VK_NUMPAD6,
    VK_NUMPAD7,
    VK_NUMPAD8,
    VK_NUMPAD9,
    VK_WINDOWS,
    VK_CONTEXT_MENU,
    VK_ALT_GRAPH,
    VK_F1,
    VK_F2,
    VK_F3,
    VK_F4,
    VK_F5,
    VK_F6,
    VK_F7,
    VK_F8,
    VK_F9,
    VK_F10,
    VK_F11,
    VK_F12,
    VK_PRINTSCREEN,
    VK_INSERT,
    VK_KP_RIGHT,
    VK_KP_LEFT,
    VK_KP_DOWN,
    VK_KP_UP

}