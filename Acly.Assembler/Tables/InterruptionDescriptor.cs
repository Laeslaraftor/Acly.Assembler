using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;
using System;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор прерывания
    /// </summary>
    public class InterruptionDescriptor : SystemDescriptor
    {
        /// <summary>
        /// Обработчик прерывания
        /// </summary>
        public MemoryOperand Handler { get; set; } = 0;
        /// <summary>
        /// Настройки дескриптора
        /// </summary>
        public InterruptionAccessByte AccessByte { get; } = new();
        /// <summary>
        /// Количество раз, которое надо повторить этот дескриптор.
        /// Пригодится для указания одного обработчика на несколько прерываний сразу.
        /// </summary>
        public uint RepeatCount { get; set; }

        #region Управление

        /// <summary>
        /// Установить обработчик
        /// </summary>
        /// <param name="idt">Таблица дескрипторов прерываний в котором находится дескриптор</param>
        /// <param name="index">Индекс текущего дескриптора</param>
        public void SetupHandler(IDT idt, int index)
        {
            ProtectedModeContext.Instance.Accumulator.Set(Handler);
            string idtIndex = idt.StartCodeName;

            if (index != 0)
            {
                idtIndex += $" + {index}";
            }

            MemoryOperand lower = MemoryOperand.Create(idtIndex);
            lower.Set(null, true, RealModeContext.Instance.Accumulator);

            ProtectedModeContext.Instance.Accumulator.ShiftRight(16);

            MemoryOperand middle = MemoryOperand.Create(idt.StartCodeName, displacement: 6 + index, asPointer: false);
            middle.Set(null, true, RealModeContext.Instance.Accumulator);

            MemoryOperand selectorAddress = MemoryOperand.Create(idt.StartCodeName, displacement: 2 + index, asPointer: false);
            MemoryOperand istAddress = MemoryOperand.Create(idt.StartCodeName, displacement: 4 + index, asPointer: false);
            MemoryOperand accessAddress = MemoryOperand.Create(idt.StartCodeName, displacement: 5 + index, asPointer: false);
            MemoryOperand offsetHighAddress = MemoryOperand.Create(idt.StartCodeName, displacement: 8 + index, asPointer: false);

            selectorAddress.Set(Prefix.Word, true, SegmentSelector);
            istAddress.Set(Prefix.Byte, true, 0);
            accessAddress.Set(Prefix.Byte, true, (byte)AccessByte);
            offsetHighAddress.Set(Prefix.Dword, true, 0);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        protected override void GenerateCode(StringBuilder builder)
        {
            if (RepeatCount > 0)
            {
                builder.AppendLine($"{Asm.Tab}%rep {RepeatCount}");
            }

            builder.AppendLine($"{Asm.Tab}dw 0, 0");
            builder.AppendLine($"{Asm.Tab}dw {SegmentSelector.Value}");
            builder.AppendLine($"{Asm.Tab}db 0");
            builder.AppendLine($"{Asm.Tab}db {Convert.ToString(AccessByte, 2)}b{Asm.Tab}; {AccessByte}");
            builder.AppendLine($"{Asm.Tab}dw 0,0");
            //builder.AppendLine($"{Asm.Tab}dd (({Handler.Value} - $$) >> 32) & 0xFFFFFFFF");
            builder.AppendLine($"{Asm.Tab}dw 0");

            if (RepeatCount > 0)
            {
                builder.AppendLine($"{Asm.Tab}%endrep");
            }
        }

        #endregion
    }
}
