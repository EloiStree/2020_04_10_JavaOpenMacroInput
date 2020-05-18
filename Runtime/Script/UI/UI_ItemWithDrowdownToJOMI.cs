using UnityEngine;

public class UI_ItemWithDrowdownToJOMI : MonoBehaviour
{

    public UI_ServerDropdownJavaOMI m_targets;
    public enum TypeOfText { CopyPast, Command, Shortcuts }
    //A↓ A↑ [[Some Text]] B↕
    public TypeOfText m_textType = TypeOfText.CopyPast;

    public void PushText(string text)
    {
        foreach (var item in m_targets.GetJavaOMISelected())
        {
            if (m_textType == TypeOfText.CopyPast)
                item.PastText(text);
            else if (m_textType == TypeOfText.Command)
                item.SendRawCommand(text);
            else if (m_textType == TypeOfText.Shortcuts)
                item.SendShortcutCommands(text);
        }
    }
    public void PushText(string text, TypeOfText type)
    {
        SetTypeOfText(type);
        PushText(text);
    }
        public void SetTypeOfText(TypeOfText type)
    {
        m_textType = type;
    }
}