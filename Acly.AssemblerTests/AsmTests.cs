using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;
using System.Diagnostics;
using Acly.Assembler.Interruptions;

namespace Acly.Assembler.Tests
{
    [TestClass()]
    public class AsmTests
    {
        [TestMethod()]
        public void SwitchTest()
        {
            Asm.CreateVariable("TestVariable", Size.x64);
            AsmSettings.UpperCaseRegisters = false;

            Asm.StartWithMode(Mode.x64);

            Asm.Comment($"R8 = 10 (0x{10:X})");
            Asm.Context.R8.Set(10);
            Asm.Comment("R9 = R8");
            Asm.Context.R9.Set(Asm.Context.R8);
            Asm.EmptyLine();
            Asm.Comment("R8 << R9 должно быть: (10 << 10) = 10240");
            Asm.Context.R8.ShiftLeft(Asm.Context.R9);

            Asm.EmptyLine();
            Asm.Comment("Подготовка к преобразованию числа в строку");

            Asm.Context.Accumulator.Set(Asm.Context.R8);
            Asm.Context.R9.Set(10);
            Asm.Context.Count.Set(Asm.Context.StackPointer);
            Asm.Context.StackPointer.Decrement();
            Asm.Context.StackPointer.Set(Prefix.Byte, true, 0);

            Asm.Label("L1");
            Asm.Context.Data.Xor(Asm.Context.Data);
            Asm.Context.R9.Divide();
            RealModeContext.Instance.Data.Lower.Add('0');
            Asm.Context.StackPointer.Decrement();
            Asm.Context.StackPointer.Set(null, true, RealModeContext.Instance.Data.Lower);
            Asm.Context.Accumulator.EqualsZero();
            Asm.JumpIfNotEquals("L1");

            Asm.EmptyLine();

            Asm.Context.Accumulator.Set(1);
            Asm.Context.DestinationIndex.Set(1);
            Asm.Context.SourceIndex.Set(Asm.Context.StackPointer);
            Asm.Context.Data.Set(Asm.Context.Count);
            Asm.Context.Data.Subtract(Asm.Context.StackPointer);
            Asm.SystemCall();

            Asm.EmptyLine();

            Asm.Context.Accumulator.Set(60);
            Asm.Context.DestinationIndex.Xor(Asm.Context.DestinationIndex);
            Asm.SystemCall();

            Debug.WriteLine(Asm.GetAssembly());
        }
    }
}