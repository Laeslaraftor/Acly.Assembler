using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Memory;
using Acly.Assembler.Registers;
using Acly.Assembler.Tables;

namespace Acly.Assembler.Demos.Bootloader32
{
    public static class Bootloader16Step2
    {
        public static async Task Create(string filePath)
        {
            Asm.Reset();

            Asm.StartWithMode(Mode.x16, 0x1000);
            Asm.DisableInterruptions();

            var loadingGdtText = Asm.CreateStringVariable("loading_gdt_message", "Loading GDT");
            var enablingProtectionText = Asm.CreateStringVariable("enabling_protection_message", "Enabling protection");

            Ints.BIOS.Video.ClearScreen(_color);
            Ints.BIOS.Video.SetCursorPosition(0, 0, 0);
            Ints.BIOS.Video.PrintString(loadingGdtText);

            CreateGDT();
            Asm.EmptyLine();

            Ints.BIOS.Video.SetCursorPosition(0, 1, 0);
            Ints.BIOS.Video.PrintString(enablingProtectionText);

            ProtectedModeContext.EnableProtectedMode();
            Asm.EmptyLine();

            Asm.Jump(MemoryOperand.Create(0x08, ProtectedModeLabel, null, 0, 1, false));

            Asm.EmptyLine();
            Asm.Switch(Mode.x32);
            Asm.Label(ProtectedModeLabel);

            MemoryOperand dataDescriptor = 0x10;

            Asm.Comment("Настройка сегментов");
            ProtectedModeContext.SetupSegments(dataDescriptor, 0x18, dataDescriptor);
            Asm.Context.StackPointer.Set(0x7C00);

            Asm.EmptyLine();
            Asm.Comment("Загрузка таблицы дескрипторов прерываний");
            CreateIDT();

            Asm.EmptyLine();
            Asm.Comment("Заполнение экрана белым цветом");
            VideoMemory.Fill(0xF020, 160 * 50);
            Asm.Comment("Установка курсора на начальную позицию [0, 0]");
            VideoMemory.SetCursorPosition(0, 0, 160);

            //Asm.EmptyLine();
            //Asm.Comment("Исключение деления на 0");
            //Asm.Context.Accumulator.Xor(0);
            //Asm.Context.Accumulator.Divide();

            Asm.EmptyLine();
            VideoMemory.PrintString("Success", 0, 0, 160, _successColor);
            Asm.DisableInterruptions();
            Asm.Halt();

            await File.WriteAllTextAsync(filePath, Asm.GetAssembly());
        }
        private static void CreateGDT()
        {
            GDT gdt = new();
            DataCodeDescriptor code = new()
            {
                Description = "Дескриптор кода"
            };
            DataCodeDescriptor data = new()
            {
                Description = "Дескриптор данных"
            };
            DataCodeDescriptor stack = new()
            {
                Description = "Дескриптор стека"
            };

            gdt.Sections.Add(code);
            gdt.Sections.Add(data);
            gdt.Sections.Add(stack);

            code.LimitLower = 0xFFFF;
            code.AccessByte.IsExecutable = true;
            code.AccessByte.IsReadable = true;
            code.AccessByte.Conforming = true;
            code.LimitHighFlags.DefaultOperationSize = OperationSize.x32_64;
            code.LimitHighFlags.Granularity = Granularity.Pages;
            code.LimitHighFlags.Limit = 0xF;

            data.LimitLower = code.LimitLower;
            data.LimitHighFlags.DefaultOperationSize = OperationSize.x32_64;
            data.LimitHighFlags.Granularity = Granularity.Pages;
            data.LimitHighFlags.Limit = 0xF;

            stack.LimitLower = code.LimitLower;
            stack.AccessByte.Conforming = true;
            stack.LimitHighFlags.DefaultOperationSize = OperationSize.x32_64;
            stack.LimitHighFlags.Granularity = Granularity.Pages;
            stack.LimitHighFlags.Limit = 0xF;

            Asm.Add(gdt);
            Asm.LoadGlobalDescriptorTable(gdt);
        }
        private static void CreateIDT()
        {
            IDT idt = new();
            ExceptionsHandlerBuilder exceptionsHandlerBuilder = new()
            {
                SegmentSelector = 0x8,
                Type = InterruptionType.x32_64Interrupt,
                Handler = ExceptionsHandlerBuilder.StandardHandler.Instance
            };

            exceptionsHandlerBuilder.Build(idt);

            Asm.Add(idt);
            Asm.LoadInterruptionDescriptorTable(idt);
        }
        
        private const string ProtectedModeLabel = "protected_mode";
        private static readonly ColorBuilder _color = new()
        {
            Background = BiosColor.Black,
            TextColor = BiosColor.LightPurple
        };
        private static readonly ColorBuilder _successColor = new()
        {
            Background = BiosColor.White,
            TextColor = BiosColor.Black
        };
    }
}
