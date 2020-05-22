public class EnumControlToPanel
{
    ControlPanelEnum m_configPanel;
    string m_command;

    public EnumControlToPanel(ControlPanelEnum configPanel, string command)
    {
        m_configPanel = configPanel;
        m_command = command;
    }
    public static string GetCommand(ControlPanelEnum panelToOpen)
    {
        for (int i = 0; i < ENUMTOCOMMAND.Length; i++)
        {
            if (ENUMTOCOMMAND[i].m_configPanel == panelToOpen)
                return ENUMTOCOMMAND[i].m_command;
        }
        return "";
    }

    public static EnumControlToPanel[] ENUMTOCOMMAND = new EnumControlToPanel[] {
        new EnumControlToPanel(
            ControlPanelEnum.Firewall, "control /name Microsoft.WindowsFirewall"),
        new EnumControlToPanel(
            ControlPanelEnum.IconConfig, "rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,0"),
        new EnumControlToPanel(
            ControlPanelEnum.HardwarePeripheral, "control hdwwiz.cpl"),
        new EnumControlToPanel(
            ControlPanelEnum.AdminTools, "control /name Microsoft.AdministrativeTools"),
        new EnumControlToPanel(
            ControlPanelEnum.BackupAndRestore, "control /name Microsoft.BackupAndRestore"),
        new EnumControlToPanel(
            ControlPanelEnum.BluetoothDevices, "	control bthprops.cpl"),
        new EnumControlToPanel(
            ControlPanelEnum.Credentials, "	control /name Microsoft.CredentialManager"),
        new EnumControlToPanel(
            ControlPanelEnum.DateAndTime, "control /name Microsoft.DateAndTime"),
        new EnumControlToPanel(
            ControlPanelEnum.DeviceManager, "control /name Microsoft.DeviceManager"),
        new EnumControlToPanel(
            ControlPanelEnum.FoldersOptions, "rundll32.exe shell32.dll,Options_RunDLL 7"),
        new EnumControlToPanel(
            ControlPanelEnum.Font, "control /name Microsoft.Fonts"),
        new EnumControlToPanel(
            ControlPanelEnum.GameControllers, "control /name Microsoft.GameControllers"),
        new EnumControlToPanel(
            ControlPanelEnum.InternetOptions, "control /name Microsoft.InternetOptions"),
        new EnumControlToPanel(
            ControlPanelEnum.KeyboardProperties, "control /name Microsoft.Keyboard"),
        new EnumControlToPanel(
            ControlPanelEnum.Language, "control /name Microsoft.Language"),
        new EnumControlToPanel(
            ControlPanelEnum.MouseConfig, "	control /name Microsoft.Mouse"),
        new EnumControlToPanel(
            ControlPanelEnum.NetworkSharing, "control /name Microsoft.NetworkAndSharingCenter"),
        new EnumControlToPanel(
            ControlPanelEnum.NetworkConnections, "control netconnections"),
        new EnumControlToPanel(
            ControlPanelEnum.Background, "control /name Microsoft.Personalization"),
        new EnumControlToPanel(
            ControlPanelEnum.PrintersAndDevices, "control /name Microsoft.Printers"),
        new EnumControlToPanel(
            ControlPanelEnum.Uninstall, "control /name Microsoft.ProgramsAndFeatures"),
        new EnumControlToPanel(
            ControlPanelEnum.DisplayConfig, "control desk.cpl"),
        new EnumControlToPanel(
            ControlPanelEnum.SoundInput, "control /name Microsoft.AudioDevicesAndSoundThemes"),
        new EnumControlToPanel(
            ControlPanelEnum.SoundOutput, "%windir%\\System32\\rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,1"),
        new EnumControlToPanel(
            ControlPanelEnum.SystemInfo, "control /name Microsoft.System"),
        new EnumControlToPanel(
            ControlPanelEnum.TaskScheduling, "control schedtasks"),
        new EnumControlToPanel(
            ControlPanelEnum.UserAccount, "control /name Microsoft.UserAccounts"),
        new EnumControlToPanel(
            ControlPanelEnum.ConfigurationPanel, "control /name Microsoft.WelcomeCenter"),
        new EnumControlToPanel(
            ControlPanelEnum.UpdateWindow, "control /name Microsoft.WindowsUpdate")
    };
}

public enum ControlPanelEnum
{
    Hotspot,
    UpdateWindow,
    ConfigurationPanel,
    Firewall,
    IconConfig,
    HardwarePeripheral,
    AdminTools,
    BackupAndRestore,
    BluetoothDevices,
    Credentials,
    DateAndTime,
    DeviceManager,
    FoldersOptions,
    Font,
    GameControllers,
    InternetOptions,
    KeyboardProperties,
    Language,
    MouseConfig,
    NetworkSharing,
    NetworkConnections,
    Background,
    PrintersAndDevices,
    Uninstall,
    DisplayConfig,
    SoundInput,
    SoundOutput,
    SystemInfo,
    TaskScheduling,
    UserAccount,
    FireWall
}