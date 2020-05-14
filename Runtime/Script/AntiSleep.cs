using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AntiSleep : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

  
}
