using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Memory;
using Acly.Assembler.Registers;
using Acly.Assembler.Tables;
using Acly.Assembler.Video;

namespace Acly.Assembler.Demos.Bootloader32
{
    public static class Bootloader16Step2
    {
        private const string VbeExceptionHandlerLabel = "vbe_exception_handler";
        private const string ExceptionHandlerLabel = "cpu_exception_handler";
        private const string ExceptionHandlerLabel16 = "cpu_exception_handler16";
        private const string PitHandlerLabel = "pit_handler";
        private const string PitHandlerZeroLabel = "pit_handler_zero";
        private const string ProtectedModeLabel = "protected_mode";
        private const string ProtectedModeLoopLabel = ".protected_mode_loop";
        private const string StringLengthLabel = "strlen";
        private const string StringLengthCountLabel = ".strlen_count";

        private static RealModeContext RealMode => RealModeContext.Instance;
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
        private static readonly ColorBuilder _errorColor = new()
        {
            Background = BiosColor.LightRed,
            TextColor = BiosColor.White
        };
        private static readonly ColorBuilder _pitColor1 = new()
        {
            Background = BiosColor.Blue,
            TextColor = BiosColor.White
        };
        private static readonly ColorBuilder _pitColor2 = new()
        {
            Background = BiosColor.LightBlue,
            TextColor = BiosColor.White
        };

        public static async Task Create(string filePath)
        {
            Asm.Reset();

            Asm.StartWithMode(Mode.x16, 0x1000);
            Asm.DisableInterruptions();

            var collectionVbeInfoText = Asm.CreateStringVariable("collecting_vbe_info_message", "Collecting VBE info");
            var settingUpPicText = Asm.CreateStringVariable("setting_up_pic_message", "Start setting up PIC");
            var loadingGdtText = Asm.CreateStringVariable("loading_gdt_message", "Loading GDT");
            var enablingProtectionText = Asm.CreateStringVariable("enabling_protection_message", "Enabling protection");
            var successText = Asm.CreateStringVariable("success_message", "Success! Burger King vs KFC vs Rulem life vs zeWhite");
            var errorText = Asm.CreateStringVariable("error_message", "Failed");
            var vbeErrorText = Asm.CreateStringVariable("vbe_error_message", "Cannot get VBE info");
            var pit1Text = Asm.CreateStringVariable("pit1_message", "PIT 1");
            var pit2Text = Asm.CreateStringVariable("pit2_message", "PIT 2");

            Ints.BIOS.Video.ClearScreen(_color);
            Ints.BIOS.Video.SetCursorPosition(0, 0, 0);
            Ints.BIOS.Video.PrintString(collectionVbeInfoText);

            var vbeInfo = VBE.GetInfo(VbeExceptionHandlerLabel);

            Ints.CPU.PIT.Initialize(50);
            Ints.CPU.PIT.Remap();

            Ints.BIOS.Video.SetCursorPosition(0, 1, 0);
            Ints.BIOS.Video.PrintString(settingUpPicText);

            Asm.EmptyLine();
            Ints.BIOS.Video.SetCursorPosition(0, 2, 0);
            Ints.BIOS.Video.PrintString(loadingGdtText);

            CreateGDT();
            Asm.EmptyLine();

            Ints.BIOS.Video.SetCursorPosition(0, 3, 0);
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
            RealMode.Accumulator.Set(MemoryOperand.Create(vbeInfo.Version, true));
            RealMode.Accumulator.Compare(0x0200);
            Asm.JumpIfBelow(VbeExceptionHandlerLabel);

            Asm.EmptyLine();
            Asm.Comment("Заполнение экрана белым цветом");
            VideoMemory.Fill(80 * 50, _successColor);
            Asm.Comment("Установка курсора на начальную позицию [0, 0]");
            VideoMemory.SetCursorPosition(0, 0, 160);

            Asm.EnableInterruptions();
            //Asm.EmptyLine();
            //Asm.Comment("Исключение деления на 0");
            //Asm.Context.Accumulator.Xor(0);
            //Asm.Context.Accumulator.Divide();

            Asm.EmptyLine();
            VideoMemory.PrintString(successText, 0, _successColor);

            Asm.Label(ProtectedModeLoopLabel);
            Asm.Jump(ProtectedModeLoopLabel);

            Asm.Label(VbeExceptionHandlerLabel);
            Asm.Context.SourceIndex.Set(vbeErrorText);
            Asm.Jump(ExceptionHandlerLabel);

            Asm.Switch(Mode.x16);
            Asm.Label(ExceptionHandlerLabel16);
            Asm.Comment("Обработчик исключения 16 бит");
            Asm.DisableInterruptions();
            Ints.BIOS.Video.ClearScreen(_errorColor);
            Ints.BIOS.Video.SetCursorPosition(0, 0, 0);
            Ints.BIOS.Video.PrintString(errorText);
            Asm.Halt();

            Asm.Switch(Mode.x32);

            Asm.Label(ExceptionHandlerLabel);
            Asm.Comment("Обработчик исключения");
            Asm.DisableInterruptions();
            VideoMemory.Fill(80 * 50, _errorColor);
            Asm.Call(StringLengthLabel);
            Asm.Context.Accumulator.Set(Asm.Context.Count);
            Asm.Context.Data.Set(0);
            Asm.Context.Base.Set(2);
            Asm.Context.Base.Divide();
            Asm.Context.Count.Set(Asm.Context.Accumulator);
            Asm.Context.Count.Add(11 * 80 + 1);
            VideoMemory.PrintString(Asm.Context.SourceIndex, Asm.Context.Count, _errorColor);
            Asm.Halt();

            Asm.Label(PitHandlerLabel);
            Asm.Comment("Обработчик прерывания PIT");
            var mem = MemoryOperand.Create(0x9000, true);
            Asm.Context.Accumulator.Set(mem);
            Asm.Context.Accumulator.EqualsZero(null, false, Asm.Context.Accumulator);
            Asm.JumpIfZero(PitHandlerZeroLabel);
            VideoMemory.PrintString(pit1Text, 160, _pitColor1);
            mem.Set(Prefix.Byte, false, 0);
            Asm.Return();

            Asm.Label(PitHandlerZeroLabel);
            mem.Set(Prefix.Byte, false, 1);
            VideoMemory.PrintString(pit2Text, 160, _pitColor2);
            Asm.Return();

            Asm.Label(StringLengthLabel);
            Asm.Comment("Функция подсчёта длины строки");
            Asm.Context.Count.Xor(Asm.Context.Count);
            Asm.Context.Count.Decrement();
            Asm.Label(StringLengthCountLabel);
            Asm.Context.Count.Increment();
            MemoryOperand.Create(Asm.Context.SourceIndex, Asm.Context.Count, 0, 1).Compare(Prefix.Byte, false, 0);
            Asm.JumpIfNotEquals(StringLengthCountLabel);
            Asm.Return();

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
            ExceptionsHandler handler = new()
            {
                ExitEntryPoint = ExceptionHandlerLabel,
                PitHandlerPointer = PitHandlerLabel,
            };
            ExceptionsHandlerBuilder exceptionsHandlerBuilder = new()
            {
                SegmentSelector = 0x8,
                Type = InterruptionType.x32_64Interrupt,
                Handler = handler,
            };

            exceptionsHandlerBuilder.Build(idt);

            Asm.Add(idt);
            Asm.LoadInterruptionDescriptorTable(idt);
        }
    }
}
