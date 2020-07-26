using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace JavaOpenMacroInput
{

    public class JavaOpenMacroUDPMonoLauncher : MonoBehaviour
    {
        public JavaOpenMacroCommunicationProcess m_process;
        public string m_ip = "127.0.0.1";
        public int m_port = 2510;
        public System.Threading.ThreadPriority m_threadPrioity = System.Threading.ThreadPriority.Normal;
        public bool m_autoStart = true;
        [Header("Debug")]
        public long m_leftToSend = 0;
        public string m_lastSend;
        [TextArea(1, 5)]
        public string m_exceptionCatch;
        private void OnEnable()
        {
            if (m_autoStart)
            {
                StartThreadWith(m_ip, m_port, m_threadPrioity);
            }

        }
        private void OnDisable()
        {
            m_process.KillJavaThreadWhenDone();
            m_process.KillWhenPossible();
        }
        public void Update()
        {
            m_leftToSend = m_process.GetMessagesInQueue();
            m_lastSend = m_process.GetLastSendMessage();
            m_exceptionCatch = m_process.GetLastSendMessage();
        }


        public void StartThreadWith(string ip, int port, System.Threading.ThreadPriority priority)
        {
            if (m_process != null)
            {
                m_process.KillWhenPossible();
            }
            m_process = new JavaOpenMacroCommunicationProcess(ip, port, priority);
        }



    }

}