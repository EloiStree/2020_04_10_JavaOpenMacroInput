using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SetLockerOfOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public void SetLocker(string password) {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SetLocker(password);

        }
    }
}
