using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace JavaOpenMacroInput { 
public class JavaOMI {

    public static JavaOMI CreateShortcutFromFirstProcessRunning()
    {
        return new JavaOMI(JavaOpenMacroCommunicationProcess.GetFirstCreatedProcess());
    }
    public static JavaOMI CreateDefaultOne(int port =2501)
    {
        JavaOMI jomi;
        JavaOpenMacroCommunicationProcess.CreateDefaultOne(out jomi, port);
        return jomi;
    }
    private JavaOpenMacroCommunicationProcess m_linkedProcessUse;
    public JavaOMI(JavaOpenMacroCommunicationProcess processUse) {
        m_linkedProcessUse = processUse;
    }

   

    public void Keyboard(JavaKeyEvent key, PressType press = PressType.Stroke)
    {
        m_linkedProcessUse.Send(key, press);

    }
    public void MouseMove(int x, int y)
    {

        m_linkedProcessUse.SendMoveMousePosition(x,y);
    }
    public void MouseClick(JavaMouseButton button, PressType press = PressType.Stroke)
    {
        m_linkedProcessUse.Send(button, press );

    }
    public void MouseScroll(int tick)
    {
        m_linkedProcessUse.SendWheel(tick);

    }
    public static void TryBasicDirtyConvertion(string text, out JavaKeyEvent[] result)
    {
        JavaKeyEvent jke;
        List<JavaKeyEvent> convertion = new List<JavaKeyEvent>();
        for (int i = 0; i < text.Length; i++)
        {
            if (TryBasicDirtyConvertion(text[i], out jke)) {
                convertion.Add(jke);
            }
        }
        result = convertion.ToArray();
    }
    public static bool TryBasicDirtyConvertion(char c, out JavaKeyEvent result)
    {
        result = JavaKeyEvent.VK_NONCONVERT;
        switch (c)
        {
            case ' ': result = JavaKeyEvent.VK_SPACE;       return true;
            case 'a': case 'A': result = JavaKeyEvent.VK_A; return true;
            case 'b': case 'B': result = JavaKeyEvent.VK_B; return true;
            case 'c': case 'C': result = JavaKeyEvent.VK_C; return true;
            case 'd': case 'D': result = JavaKeyEvent.VK_D; return true;
            case 'e': case 'E': result = JavaKeyEvent.VK_E; return true;
            case 'f': case 'F': result = JavaKeyEvent.VK_F; return true;
            case 'g': case 'G': result = JavaKeyEvent.VK_G; return true;
            case 'h': case 'H': result = JavaKeyEvent.VK_H; return true;
            case 'i': case 'I': result = JavaKeyEvent.VK_I; return true;
            case 'j': case 'J': result = JavaKeyEvent.VK_J; return true;
            case 'k': case 'K': result = JavaKeyEvent.VK_K; return true;
            case 'l': case 'L': result = JavaKeyEvent.VK_L; return true;
            case 'm': case 'M': result = JavaKeyEvent.VK_M; return true;
            case 'n': case 'N': result = JavaKeyEvent.VK_N; return true;
            case 'o': case 'O': result = JavaKeyEvent.VK_O; return true;
            case 'p': case 'P': result = JavaKeyEvent.VK_P; return true;
            case 'q': case 'Q': result = JavaKeyEvent.VK_Q; return true;
            case 'r': case 'R': result = JavaKeyEvent.VK_R; return true;
            case 's': case 'S': result = JavaKeyEvent.VK_S; return true;
            case 't': case 'T': result = JavaKeyEvent.VK_T; return true;
            case 'u': case 'U': result = JavaKeyEvent.VK_U; return true;
            case 'v': case 'V': result = JavaKeyEvent.VK_V; return true;
            case 'w': case 'W': result = JavaKeyEvent.VK_W; return true;
            case 'x': case 'X': result = JavaKeyEvent.VK_X; return true;
            case 'y': case 'Y': result = JavaKeyEvent.VK_Y; return true;
            case 'z': case 'Z': result = JavaKeyEvent.VK_Z; return true;


            case '0': result = JavaKeyEvent.VK_0; return true;
            case '1': result = JavaKeyEvent.VK_1; return true;
            case '2': result = JavaKeyEvent.VK_2; return true;
            case '3': result = JavaKeyEvent.VK_3; return true;
            case '4': result = JavaKeyEvent.VK_4; return true;
            case '5': result = JavaKeyEvent.VK_5; return true;
            case '6': result = JavaKeyEvent.VK_6; return true;
            case '7': result = JavaKeyEvent.VK_7; return true;
            case '8': result = JavaKeyEvent.VK_8; return true;
            case '9': result = JavaKeyEvent.VK_9; return true;

            default:
                break;
        }
        return false;
    }
}

public class JavaOpenMacroCommunicationProcess
{
    public static JavaOpenMacroCommunicationProcess CreateDefaultOne(out JavaOMI shortcut, int port = 2501, string ip = "127.0.0.1", System.Threading.ThreadPriority priority = ThreadPriority.Normal) {
        JavaOpenMacroCommunicationProcess p= new JavaOpenMacroCommunicationProcess(ip, port, priority);
        shortcut = new JavaOMI(p);
        return p;
    }
    public static List<JavaOpenMacroCommunicationProcess> m_processesRunning = new List<JavaOpenMacroCommunicationProcess>();
    public static JavaOpenMacroCommunicationProcess GetFirstCreatedProcess() {
        for (int i = 0; i < m_processesRunning.Count; i++)
        {
            if (m_processesRunning != null)
                return m_processesRunning[i];
        }
        return null;
    }
    public JavaOpenMacroCommunicationProcess(string ip, int port, System.Threading.ThreadPriority priority)
    {
        m_ip = ip;
        m_port = port;
        Thread t = new Thread(SendToJavaOpenMacro);
        t.Priority = priority;
        t.Start();
        m_processesRunning.Add(this);
    }
    ~JavaOpenMacroCommunicationProcess() {
        m_processesRunning.Remove(this);
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
}