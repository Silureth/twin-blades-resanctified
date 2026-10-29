namespace Resanctified.Engine.Input
{
    public enum PSSButton
    {
        ButtonNone = 0,
        ButtonA = 1,
        ButtonB = 2,
        ButtonBack = 4,
        ButtonDPadDown = 8,
        ButtonDPadLeft = 16, // 0x00000010
        ButtonDPadRight = 32, // 0x00000020
        ButtonDPadUp = 64, // 0x00000040
        ButtonStart = 128, // 0x00000080
        ButtonX = 256, // 0x00000100
        ButtonY = 512, // 0x00000200
        ButtonLeftThumbstickDown = 1024, // 0x00000400
        ButtonLeftThumbstickLeft = 2048, // 0x00000800
        ButtonLeftThumbstickRight = 4096, // 0x00001000
        ButtonLeftThumbstickUp = 8192, // 0x00002000
        ButtonRightThumbstickDown = 16384, // 0x00004000
        ButtonRightThumbstickLeft = 32768, // 0x00008000
        ButtonRightThumbstickRight = 65536, // 0x00010000
        ButtonRightThumbstickUp = 131072, // 0x00020000
        ButtonLeftTrigger = 262144, // 0x00040000
        ButtonRightTrigger = 524288, // 0x00080000
        ButtonLeftShoulder = 1048576, // 0x00100000
        ButtonRightShoulder = 2097152, // 0x00200000
    }
}
