using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Demos.Bootloader16
{
    public static class Bootloader
    {
        private readonly static ColorBuilder _color = new()
        {
            Background = BiosColor.Black,
            TextColor = BiosColor.Blue,
            IsBright = true
        };
        private readonly static ColorBuilder _lightColor = new()
        {
            Background = BiosColor.Black,
            TextColor = BiosColor.LightBlue,
        };

        public static async Task Create(string filePath)
        {
            Asm.EndingTimes = 510;
            Asm.EndingDoubleWord = 0xAA55;

            var message = Asm.CreateStringVariable("message", Size.x16);
            message.Value = "Hello from x16 bootloader!";

            Asm.Origin(Origins.BiosLoader);
            _ = Asm.StartWithMode(Mode.x16);

            Asm.Jump(PrintDarkMessageLabel);

            Asm.Label(PrintMessageLabel);
            Asm.Comment("Функция вывода сообщения по центру экрана.");
            Ints.BIOS.Video.ClearScreen(RealModeContext.Instance.Data.Higher);
            Ints.BIOS.Video.SetCursorPosition(0, 11, 39 - message.Value.Length / 2);
            Ints.BIOS.Video.PrintString(message, 0, 0);
            Asm.Return();

            Asm.Label(PrintDarkMessageLabel);
            Asm.Comment("Функция вывода сообщения с обычным цветом");
            RealModeContext.Instance.Data.Higher.Set(_color);
            Asm.Call(PrintMessageLabel);
            Ints.BIOS.SystemFunctions.Delay(10);
            Asm.Jump(PrintLightMessageLabel);

            Asm.Label(PrintLightMessageLabel);
            Asm.Comment("Функция вывода сообщения со светлым цветом");
            RealModeContext.Instance.Data.Higher.Set(_lightColor);
            Asm.Call(PrintMessageLabel);
            Ints.BIOS.SystemFunctions.Delay(10);
            Asm.Jump(PrintDarkMessageLabel);


            await File.WriteAllTextAsync(filePath, Asm.GetAssembly());
        }

        private const string PrintMessageLabel = "print_message";
        private const string PrintLightMessageLabel = "print_light_message";
        private const string PrintDarkMessageLabel = "print_Dark_message";
    }
}
