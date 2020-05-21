using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ReversoToJOMI : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_targets;

    public void PastTextToReversoFR()
    {
        StartCoroutine(CoroutinePastTextToUrl("https://www.reverso.net/orthographe/correcteur-francais/"));

    }
    public void PastTextToReversoEN()
    {
        StartCoroutine(CoroutinePastTextToUrl("https://www.reverso.net/spell-checker/english-spelling-grammar/"));

    }
   
    public void CopyTextToScribensFR()
    {
        StartCoroutine(CoroutineCopyAndGoTo("https://www.scribens.fr/"));

    }
    public void CopyTextToScribensEN()
    {
        StartCoroutine(CoroutineCopyAndGoTo("https://www.scribens.com/"));

    }
    public IEnumerator CoroutinePastTextToUrl(string url)
    {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SendShortcutCommands("Ctrl↓ C↕  Ctrl↑");
            yield return new WaitForSeconds(0.5f);
            item.OpenUrl( url);
            yield return new WaitForSeconds(2f);
            item.SendShortcutCommands("Ctrl↓ V↕  Ctrl↑");
            yield return new WaitForSeconds(0.5f);
            item.Keyboard(JavaKeyEvent.VK_ENTER, PressType.Stroke);
        }


    }
    public IEnumerator CoroutineCopyAndGoTo(string url)
    {

        foreach (var item in m_targets.GetJavaOMISelected())
        {
            item.SendShortcutCommands("Ctrl↓ C↕  Ctrl↑");
            yield return new WaitForSeconds(0.3f);
            item.SendRawCommand("url:" + url);
        }


    }
}
