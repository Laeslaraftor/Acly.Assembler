using Acly.Assembler.Contexts;
using System.Text;
using System;
using Acly.Assembler.Registers;
using System.Collections.Generic;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Tables;

namespace Acly.Assembler
{
    /// <summary>
    /// Статический класс, в котором происходит основная движуха
    /// </summary>
    public static class Asm
    {
        /// <summary>
        /// Текущий контекст режима процессора
        /// </summary>
        public static CpuModeContext Context
        {
            get
            {
                if (_currentContext == null)
                {
                    throw new InvalidOperationException("Режим работы процессора не был установлен");
                }

                return _currentContext;
            }
        }

        private static StringBuilder Builder
        {
            get 
            {
                _builder ??= _beginningBuilder;
                return _builder;
            }
            set => _builder = value;
        }

        private static readonly StringBuilder _beginningBuilder = new();
        private static readonly StringBuilder _textBuilder = new();
        private static readonly StringBuilder _dataBuilder = new();
        private static readonly StringBuilder _bssBuilder = new();
        private static readonly Dictionary<string, Variable> _variables = new();
        private static readonly Dictionary<string, DescriptorTable> _tables = new();

        private static StringBuilder? _builder;
        private static CpuModeContext? _currentContext;

        #region Основные оп-коды

        /// <summary>
        /// Установить элемент как глобальный
        /// </summary>
        /// <param name="label">Название элемента</param>
        public static void SetGlobal(string label)
        {
            Emit($"global {label}");
        }
        /// <summary>
        /// Начать функцию
        /// </summary>
        /// <param name="name">Название функции</param>
        public static void Label(string name)
        {
            Emit();
            Emit($"{name}:", false);
        }
        /// <summary>
        /// hlt. Остановить процессор
        /// </summary>
        public static void Halt()
        {
            Emit("hlt");
        }
        /// <summary>
        /// cpuid. Получить информацию о процессоре
        /// </summary>
        public static void CpuId()
        {
            Emit("cpuid");
        }

        /// <summary>
        /// ret. Вернуться из процедуры
        /// </summary>
        public static void Return()
        {
            Emit("ret");
        }
        /// <summary>
        /// retfq. Вернуться из дальней процедуры
        /// </summary>
        public static void ReturnFar()
        {
            Emit("retfq");
        }

        /// <summary>
        /// Системный вызов (syscall). Работает только на Linux
        /// </summary>
        public static void SystemCall()
        {
            Emit("syscall");
        }

        /// <summary>
        /// Вызвать функцию по заданному адресу
        /// </summary>
        /// <param name="address">Адрес функции</param>
        public static void Call(MemoryOperand address)
        {
            Emit($"call {address}");
        }
        /// <summary>
        /// Вызвать функцию на названию
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void Call(string label)
        {
            Emit($"call {label}");
        }
        /// <summary>
        /// callf. Вызвать дальнюю функцию по заданному адресу
        /// </summary>
        /// <param name="address">Адрес функции</param>
        public static void CallFar(MemoryOperand address)
        {
            Emit($"callf {address}");
        }
        /// <summary>
        /// callf. Вызвать дальнюю функцию на названию
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void CallFar(string label)
        {
            Emit($"callf {label}");
        }

        /// <summary>
        /// Перейти к выполнению код по заданному адресу
        /// </summary>
        /// <param name="address">Адрес кода</param>
        public static void Jump(MemoryOperand address)
        {
            Emit($"jmp {address}");
        }
        /// <summary>
        /// Перейти к выполнению функции
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void Jump(string label)
        {
            Emit($"jmp {label}");
        }

        /// <summary>
        /// Совершить прерывание. Некоторые полезные прерывания можно найти в <see cref="Ints"/>.
        /// </summary>
        /// <param name="interruption">Прерывание, которое необходимо совершить</param>
        public static void Interrupt(Interruption interruption)
        {
            Emit($"int {interruption}");
        }

        #endregion

        #region Логика

        /// <summary>
        /// Перейти к функции если значения равны\равно нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfEquals(string label)
        {
            Emit($"je {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значения равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfEquals(MemoryOperand functionAddress)
        {
            Emit($"je {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значения не равны\не равны нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotEquals(string label)
        {
            Emit($"jne {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значения не равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfNotEquals(MemoryOperand functionAddress)
        {
            Emit($"jne {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значение больше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfGreater(string label)
        {
            Emit($"jg {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение больше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfGreater(MemoryOperand functionAddress)
        {
            Emit($"jg {functionAddress}");
        }
        /// <summary>
        /// Перейти к функции если значение меньше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfLess(string label)
        {
            Emit($"jl {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение меньше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfLess(MemoryOperand functionAddress)
        {
            Emit($"jl {functionAddress}");
        }

        /// <summary>
        /// Перейти к функции если значение больше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfAbove(string label)
        {
            Emit($"ja {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение больше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfAbove(MemoryOperand functionAddress)
        {
            Emit($"ja {functionAddress}");
        }
        /// <summary>
        /// Перейти к функции если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfBelow(string label)
        {
            Emit($"jb {label}");
        }
        /// <summary>
        /// Перейти к функции по адресу если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfBelow(MemoryOperand functionAddress)
        {
            Emit($"jb {functionAddress}");
        }

        #endregion

        #region Таблицы

        /// <summary>
        /// Создать глобальную таблицу дескрипторов (GDT).
        /// Эта таблица может быть только в единственном экземпляре!
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <returns>Новая глобальная таблица дескрипторов (GDT)</returns>
        public static GlobalDescriptorTable CreateGlobalDescriptorTable(string name)
        {
            foreach (var createdTable in _tables.Values)
            {
                if (createdTable is GlobalDescriptorTable)
                {
                    throw new AssemblerException("Глобальная таблица дескрипторов (GDT) может быть только в единственном экземпляре!");
                }
            }

            GlobalDescriptorTable table = new(name);
            _tables.Add(name, table);

            return table;
        }
        /// <summary>
        /// Создать локальную таблицу дескрипторов (LDT)
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <returns>Новая глобальная таблица дескрипторов (GDT)</returns>
        public static LocalDescriptorTable CreateLocalDescriptorTable(string name)
        {
            LocalDescriptorTable table = new(name);
            _tables.Add(name, table);

            return table;
        }
        /// <summary>
        /// Создать таблицу дескрипторов прерывания (IDT)
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <returns>Новая глобальная таблица дескрипторов (GDT)</returns>
        public static InterruptDescriptorTable CreateInterruptionDescriptorTable(string name)
        {
            InterruptDescriptorTable table = new(name);
            _tables.Add(name, table);

            return table;
        }

        /// <summary>
        /// lgdt. Загрузить глобальную таблицу дескрипторов (GDT).
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void LoadGlobalDescriptorTable(GlobalDescriptorTable table)
        {
            DescriptorTableCommand("lgdt", table);
        }
        /// <summary>
        /// lldt. Загрузить локальную таблицу дескрипторов (LDT).
        /// Эта таблица должны быть предварительно описана в глобальной таблице дескрипторов (GDT)
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void LoadLocalDescriptorTable(LocalDescriptorTable table)
        {
            DescriptorTableCommand("lldt", table);
        }
        /// <summary>
        /// lidt. Загрузить таблицу прерываний (IDT).
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void LoadInterruptionDescriptorTable(InterruptDescriptorTable table)
        {
            DescriptorTableCommand("lidt", table);
        }

        /// <summary>
        /// sgdt. Сохранить текущую глобальную таблицу дескрипторов (GDT).
        /// Полезно для диагностики или виртуализации.
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void SaveGlobalDescriptorTable(GlobalDescriptorTable table)
        {
            DescriptorTableCommand("sgdt", table);
        }
        /// <summary>
        /// sldt. Сохранить текущую локальную таблицу дескрипторов (LDT). 
        /// Может возвращать 0, если LDT не используется
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void SaveLocalDescriptorTable(LocalDescriptorTable table)
        {
            DescriptorTableCommand("sldt", table);
        }
        /// <summary>
        /// sidt. Сохранить текущую таблицу прерываний (IDT).
        /// Полезно для отладки или rootkit'ов
        /// </summary>
        /// <param name="table">Таблица, которую необходимо загрузить</param>
        public static void SaveInterruptionDescriptorTable(InterruptDescriptorTable table)
        {
            DescriptorTableCommand("sidt", table);
        }

        private static void DescriptorTableCommand(string command, DescriptorTable table)
        {
            Emit($"{command} [{table.GetFormattedName()}{DescriptorTable.NamePostfix}]");
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// Начать код со стандартной точки входа _start с указанным режимом процессора
        /// </summary>
        /// <param name="mode">Начальный режим процессора</param>
        public static void StartWithMode(Mode mode)
        {
            Switch(mode);
            Section(Assembler.Section.Text);
            SetGlobal("_start");
            Label("_start");
        }

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <param name="comment">Комментарий, который будет добавлен к коду</param>
        public static void Comment(string comment)
        {
            Emit($"; {comment}");
        }
        /// <summary>
        /// Добавить пустую строку
        /// </summary>
        public static void EmptyLine() => Emit();

        /// <summary>
        /// Переключить режим работы процессора. Если будет указан текущий режим, то ничего не произойдёт.
        /// </summary>
        /// <param name="cpuMode">Новый режим работы процессора</param>
        public static void Switch(Mode cpuMode)
        {
            Emit($"[BITS {(int)cpuMode}]");
            _currentContext = CpuModeContext.GetContext(cpuMode);
        }
        /// <summary>
        /// Указание на то, куда разместить следующий фрагмент кода
        /// </summary>
        /// <param name="origin"></param>
        public static void Origin(uint origin)
        {
            Emit($"[ORG 0x{origin:X}]");
        }
        /// <summary>
        /// Задать текущую секцию кода.
        /// </summary>
        /// <param name="section"></param>
        public static void Section(Section section)
        {
            if (section == Assembler.Section.Text)
            {
                Builder = _textBuilder;
            }
            else if (section == Assembler.Section.Data)
            {
                Builder = _dataBuilder;
            }
            else if (section == Assembler.Section.Bss)
            {
                Builder = _bssBuilder;
            }
        }

        internal static void Emit(string line)
        {
            Emit(line, Builder != _beginningBuilder);
        }
        internal static void Emit(string line, bool withTab)
        {
            string tab = string.Empty;

            if (withTab)
            {
                tab = Tab;
            }

            Builder.AppendLine(tab + line);
        }
        internal static void Emit() => Emit(string.Empty, false);

        #endregion

        #region Переменные и константы

        /// <summary>
        /// Создать переменную
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная</returns>
        public static Variable CreateVariable(string name, Size size, bool isReserved = true)
        {
            Variable result = new(size, name, isReserved);
            _variables.Add(name, result);

            return result;
        }
        /// <summary>
        /// Создать переменную массива
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная массива</returns>
        public static ArrayVariable CreateArrayVariable(string name, Size size, bool isReserved = true)
        {
            ArrayVariable result = new(size, name, isReserved);
            _variables.Add(name, result);

            return result;
        }

        /// <summary>
        /// equ. Приравнять константу значению
        /// </summary>
        /// <param name="name">Название константы</param>
        /// <param name="operand">Значение, которому будет равна константа</param>
        /// <returns>Созданная константа</returns>
        public static Variable CreateConstant(string name, MemoryOperand operand)
        {
            Emit($"{name} equ {operand}", false);

            return new(Size.x64, name, true);
        }
        /// <summary>
        /// equ. Приравнять константу значению
        /// </summary>
        /// <param name="name">Название константы</param>
        /// <param name="label">Значение, которому будет равна константа</param>
        /// <returns>Созданная константа</returns>
        public static Variable CreateConstant(string name, string label)
        {
            Emit($"{name} equ {label}", false);

            return new(Size.x64, name, true);
        }

        #endregion

        #region Управление

        /// <summary>
        /// Получить составленный ассемблерный код
        /// </summary>
        /// <returns>Составленный код</returns>
        public static string GetAssembly()
        {
            StringBuilder result = new();
            result.Append(_beginningBuilder.ToString());

            AppendSection(result, Assembler.Section.Text, _textBuilder.ToString());

            var dataVariables = GetVariables(true);
            var bssVariables = GetVariables(false);

            if (dataVariables != null || _tables.Count > 0)
            {
                AppendSection(result, Assembler.Section.Data, null);
                
                if (dataVariables != null)
                {
                    result.AppendLine(dataVariables.TrimEnd());
                }
                if (_tables.Count > 0)
                {
                    foreach (var table in _tables.Values)
                    {
                        result.AppendLine(table.ToAssembler());
                    }
                }
            }
            if (bssVariables != null)
            {
                AppendSection(result, Assembler.Section.Bss, bssVariables);
            }

            return result.ToString();
        }

        private static void AppendSection(StringBuilder builder, Section section, string? content)
        {
            builder.AppendLine();
            builder.AppendLine($"section .{section.ToString().ToLower()}");
            
            if (content != null)
            {
                builder.AppendLine(content.TrimEnd());
            }
        }
        private static string? GetVariables(bool isReserved)
        {
            StringBuilder result = new();

            foreach (var variable in _variables.Values)
            {
                if (variable.IsReserved == isReserved)
                {
                    result.AppendLine(Tab + variable.AssemblerLine);
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return result.ToString();
        }

        #endregion

        #region Константы

        private const string Tab = "    ";

        #endregion
    }
}
