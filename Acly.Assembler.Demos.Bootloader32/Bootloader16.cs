using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;
using Acly.Assembler.Tables;

namespace Acly.Assembler.Demos.Bootloader32
{
    public static class Bootloader16
    {
        private static readonly ColorBuilder _errorColor = new()
        {
            TextColor = BiosColor.Red
        };

        public static async Task Create(string filePath)
        {
            Asm.Reset();
            Asm.EndingTimes = 510;
            Asm.EndingDoubleWord = 0xAA55;

            var startMessage = Asm.CreateStringVariable("message", Size.x16);
            startMessage.Value = "Start booting...";
            var diskErrorMessage = Asm.CreateStringVariable("disk_error_message", Size.x16);
            diskErrorMessage.Value = "Disk read error!";

            Asm.Origin(Origins.BiosLoader);
            _ = Asm.StartWithMode(Mode.x16);

            Asm.Context.BasePointer.Set(0x9000);
            Asm.Context.StackPointer.Set(Asm.Context.BasePointer);

            Ints.BIOS.Video.ClearScreen();
            Ints.BIOS.Video.SetCursorPosition(0, 0, 0);
            Ints.BIOS.Video.PrintString(startMessage);

            Ints.BIOS.Disk.ReadSectors(DiskType.HardDrive, 50, 0x1000);
            Asm.JumpIfCarry(DiskReadErrorLabel);
            Asm.Jump(0x1000);

            Asm.Label(DiskReadErrorLabel);
            Ints.BIOS.Video.Scroll(ScrollDirection.Down, 0, _errorColor, 1, 1, 0, 80);
            Ints.BIOS.Video.SetCursorPosition(0, 1, 0);
            Ints.BIOS.Video.PrintString(diskErrorMessage);
            Asm.Halt();


            await File.WriteAllTextAsync(filePath, Asm.GetAssembly());
        }

        private const string DiskReadErrorLabel = "disk_error";
    }
}
