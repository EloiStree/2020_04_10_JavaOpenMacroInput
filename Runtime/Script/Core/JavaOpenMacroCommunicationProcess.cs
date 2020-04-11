using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class JavaOpenMacroCommunicationProcess
{
    public JavaOpenMacroCommunicationProcess(string ip, int port, System.Threading.ThreadPriority priority)
    {
        m_ip = ip;
        m_port = port;
        Thread t = new Thread(SendToJavaOpenMacro);
        t.Priority = priority;
        t.Start();
    }
    private string m_ip = "127.0.0.1";
    private int m_port = 2510;
    private bool m_keepThreadAlive = true;
    private bool m_killJavaThreadWhenFinish = true;
    private Queue<string> m_toSend = new Queue<string>();
    private string m_lastSend;
    private string m_lastExceptionCatch;

    public int GetLeftMessagesToSend() { return m_toSend.Count; }
    public string GetLastSendMessage() { return m_lastSend; }
    public string GetLastExceptionCatch() { return m_lastExceptionCatch; }
    public void KillJavaThreadWhenDone()
    {
        m_killJavaThreadWhenFinish = true;
    }
    public void KillWhenPossible()
    {
        m_keepThreadAlive = true;
    }
    public void Send(string msg)
    {
        m_toSend.Enqueue(msg);
    }
    public void Send(JavaKeyEvent keyToType, PressType press)
    {
        string instruction = "ks:";
        if (press == PressType.Press) instruction = "kp:";
        if (press == PressType.Release) instruction = "kr:";
        m_toSend.Enqueue(instruction + keyToType.ToString());
    }
    public void Send(JavaMouseButton mouseType, PressType press)
    {
        string instruction = "ms:";
        if (press == PressType.Press) instruction = "mp:";
        if (press == PressType.Release) instruction = "mr:";
        m_toSend.Enqueue(instruction + (int)mouseType);
    }

    public void SendWheel(int value)
    {
        m_toSend.Enqueue("wh:" + value);
    }
    public void SendMoveMousePosition(int x, int y)
    {
        m_toSend.Enqueue("mm:" + x + ":" + y);
    }

    private void SendToJavaOpenMacro()
    {
        SendToJavaOpenMacro(m_ip, m_port);
    }
    private void SendToJavaOpenMacro(string ip, int port)
    {
        UdpClient udpClient = new UdpClient(ip, port);
        Byte[] sendBytes = new Byte[0];
        while (m_keepThreadAlive)
        {
            if (m_toSend.Count > 0)
            {
                string msg = m_toSend.Dequeue();
                m_lastSend = msg;
                sendBytes = Encoding.ASCII.GetBytes(msg);
                try
                {
                    udpClient.Send(sendBytes, sendBytes.Length);
                }
                catch (Exception e)
                {
                    m_lastExceptionCatch = e.ToString();
                }
                Thread.Sleep(5);
            }
            Thread.Sleep(50);
        }
        if (m_killJavaThreadWhenFinish)
            sendBytes = Encoding.ASCII.GetBytes("stop");
        try
        {
            udpClient.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            m_lastExceptionCatch = e.ToString();
        }

    }

}
public enum PressType { Press, Release, Stroke }
