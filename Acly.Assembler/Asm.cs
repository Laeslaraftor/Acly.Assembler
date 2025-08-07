﻿using Acly.Assembler.Contexts;
using System.Text;
using Acly.Assembler.Registers;
using System.Collections.Generic;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Tables;
using Acly.Assembler.Memory;
using System;

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
                _currentContext ??= RealModeContext.Instance;
                return _currentContext;
            }
        }
        /// <summary>
        /// Заполнение до указанного количества байт + сигнатура загрузочного сектора.
        /// Если null, то не будет прописываться
        /// </summary>
        public static int? EndingTimes { get; set; }
        /// <summary>
        /// Завершающее двойное слово
        /// </summary>
        public static ulong? EndingDoubleWord { get; set; }
        /// <summary>
        /// Текущая секция кода
        /// </summary>
        public static Section? CurrentSection
        {
            get
            {
                if (Builder == _textBuilder)
                {
                    return Assembler.Section.Text;
                }
                else if (Builder == _dataBuilder)
                {
                    return Assembler.Section.Data;
                }
                else if (Builder == _bssBuilder)
                {
                    return Assembler.Section.Bss;
                }

                return null;
            }
        }
        /// <summary>
        /// Текущий режим работы процессора
        /// </summary>
        public static Mode CurrentMode { get; private set; } = Mode.x16;

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
        private static readonly Dictionary<string, VariableBase> _variables = new();
        private static readonly List<ICodeGenerator> _delayedCodeGenerators = new();

        private static PageTableBuilder? _pageTableBuilder;
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
        /// iret. Вернуться из прерывания
        /// </summary>
        public static void InterruptionReturn()
        {
            Emit("iret");
        }
        /// <summary>
        /// iretd. Вернуться из прерывания
        /// </summary>
        public static void InterruptionReturnDouble()
        {
            Emit("iretd");
        }
        /// <summary>
        /// iretq. Вернуться из прерывания
        /// </summary>
        public static void InterruptionReturnQuad()
        {
            Emit("iretq");
        }

        /// <summary>
        /// Системный вызов (syscall). Работает только на Linux
        /// </summary>
        public static void SystemCall()
        {
            Emit("syscall");
        }

        /// <summary>
        /// call. Вызвать функцию по заданному адресу
        /// </summary>
        /// <param name="address">Адрес функции</param>
        public static void Call(MemoryOperand address)
        {
            Emit($"call {address}");
        }
        /// <summary>
        /// call. Вызвать функцию на названию
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
        /// jmp. Перейти к выполнению код по заданному адресу
        /// </summary>
        /// <param name="address">Адрес кода</param>
        public static void Jump(MemoryOperand address)
        {
            Emit($"jmp {address}");
        }
        /// <summary>
        /// jmp. Перейти к выполнению функции
        /// </summary>
        /// <param name="label">Название функции</param>
        public static void Jump(string label)
        {
            Emit($"jmp {label}");
        }

        /// <summary>
        /// int. Совершить прерывание. Некоторые полезные прерывания можно найти в <see cref="Ints"/>.
        /// </summary>
        /// <param name="interruption">Прерывание, которое необходимо совершить</param>
        public static void Interrupt(Interruption interruption)
        {
            Emit($"int {interruption}");
        }
        /// <summary>
        /// sti. Включить обработку прерываний, устанавливая регистр IF в 1
        /// </summary>
        public static void EnableInterruptions()
        {
            Emit("sti");
        }
        /// <summary>
        /// cli. Отключить обработку прерываний, устанавливая регистр IF в 0
        /// </summary>
        public static void DisableInterruptions()
        {
            Emit("cli");
        }

        /// <summary>
        /// lodsb. Загружает один байт из памяти по адресу, указанному в регистре SI, ESI или RSI, в регистр AL (младший байт регистра AX).
        /// Затем изменяет значение регистра SI, ESI или RSI (увеличивает или уменьшает его на 1), чтобы указывать на следующий байт в памяти.
        /// </summary>
        public static void LoadStringByte()
        {
            Emit("lodsb");
        }

        /// <summary>
        /// pusha. Сохранить все регистры в стек
        /// </summary>
        public static void PushAll()
        {
            Emit("pusha");
        }
        /// <summary>
        /// pushad. Сохранить все регистры в стек
        /// </summary>
        public static void PushAllDouble()
        {
            Emit("pushad");
        }
        /// <summary>
        /// popa. Восстановить все регистры из стека
        /// </summary>
        public static void PopAll()
        {
            Emit("popa");
        }
        /// <summary>
        /// popad. Восстановить все регистры из стека
        /// </summary>
        public static void PopAllDouble()
        {
            Emit("popad");
        }

        /// <summary>
        /// rep stosd. Записать данные из AX в видеопамять CX количества раз
        /// </summary>
        public static void RepeatStoreByte()
        {
            Emit("rep stosb");
        }
        /// <summary>
        /// rep stosw. Записать данные из AX в видеопамять CX количества раз
        /// </summary>
        public static void RepeatStoreWord()
        {
            Emit("rep stosw"); 
        }
        /// <summary>
        /// rep stosd. Записать данные из AX в видеопамять CX количества раз
        /// </summary>
        public static void RepeatStoreDoubleWord()
        {
            Emit("rep stosd");
        }

        #endregion

        #region Логика

        /// <summary>
        /// je. Перейти к функции если значения равны\равно нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfEquals(string label)
        {
            Emit($"je {label}");
        }
        /// <summary>
        /// je. Перейти к функции по адресу если значения равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfEquals(MemoryOperand functionAddress)
        {
            Emit($"je {functionAddress}");
        }

        /// <summary>
        /// jz. Перейти к функции если значения равны\равно нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfZero(string label)
        {
            Emit($"jz {label}");
        }
        /// <summary>
        /// jz. Перейти к функции по адресу если значения равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfZero(MemoryOperand functionAddress)
        {
            Emit($"jz {functionAddress}");
        }

        /// <summary>
        /// jne. Перейти к функции если значения не равны\не равны нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotEquals(string label)
        {
            Emit($"jne {label}");
        }
        /// <summary>
        /// jne. Перейти к функции по адресу если значения не равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfNotEquals(MemoryOperand functionAddress)
        {
            Emit($"jne {functionAddress}");
        }

        /// <summary>
        /// jnz. Перейти к функции если значения не равны\не равны нулю
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotZero(string label)
        {
            Emit($"jnz {label}");
        }
        /// <summary>
        /// jnz. Перейти к функции по адресу если значения не равны\равно нулю
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfNotZero(MemoryOperand functionAddress)
        {
            Emit($"jnz {functionAddress}");
        }

        /// <summary>
        /// jg. Перейти к функции если значение больше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfGreater(string label)
        {
            Emit($"jg {label}");
        }
        /// <summary>
        /// jg. Перейти к функции по адресу если значение больше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfGreater(MemoryOperand functionAddress)
        {
            Emit($"jg {functionAddress}");
        }

        /// <summary>
        /// jl. Перейти к функции если значение меньше (знаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfLess(string label)
        {
            Emit($"jl {label}");
        }
        /// <summary>
        /// jl. Перейти к функции по адресу если значение меньше (знаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfLess(MemoryOperand functionAddress)
        {
            Emit($"jl {functionAddress}");
        }

        /// <summary>
        /// ja. Перейти к функции если значение больше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfAbove(string label)
        {
            Emit($"ja {label}");
        }
        /// <summary>
        /// ja. Перейти к функции по адресу если значение больше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfAbove(MemoryOperand functionAddress)
        {
            Emit($"ja {functionAddress}");
        }

        /// <summary>
        /// jb. Перейти к функции если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfBelow(string label)
        {
            Emit($"jb {label}");
        }
        /// <summary>
        /// jb. Перейти к функции по адресу если значение меньше (беззнаковое)
        /// </summary>
        /// <param name="functionAddress">Адрес функции</param>
        public static void JumpIfBelow(MemoryOperand functionAddress)
        {
            Emit($"jb {functionAddress}");
        }

        /// <summary>
        /// jc. Перейти, если флаг переноса (CF) равен 1
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfCarry(string label)
        {
            Emit($"jc {label}");
        }
        /// <summary>
        /// jc. Перейти, если флаг переноса (CF) равен 1
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfCarry(MemoryOperand functionAddress)
        {
            Emit($"jc {functionAddress}");
        }

        /// <summary>
        /// jnc. Перейти, если флаг переноса (CF) равен 0
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotCarry(string label)
        {
            Emit($"jnc {label}");
        }
        /// <summary>
        /// jnc. Перейти, если флаг переноса (CF) равен 0
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfNotCarry(MemoryOperand functionAddress)
        {
            Emit($"jnc {functionAddress}");
        }

        /// <summary>
        /// jo. Перейти, если флаг переполнения (OF) равен 1
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfOverflow(string label)
        {
            Emit($"jo {label}");
        }
        /// <summary>
        /// jo. Перейти, если флаг переполнения (OF) равен 1
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfOverflow(MemoryOperand functionAddress)
        {
            Emit($"jo {functionAddress}");
        }

        /// <summary>
        /// jno. Перейти, если флаг переполнения (OF) равен 0
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotOverflow(string label)
        {
            Emit($"jno {label}");
        }
        /// <summary>
        /// jno. Перейти, если флаг переполнения (OF) равен 0
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfNotOverflow(MemoryOperand functionAddress)
        {
            Emit($"jno {functionAddress}");
        }

        /// <summary>
        /// js. Перейти, если флаг знака (SF) равен 1
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfSign(string label)
        {
            Emit($"js {label}");
        }
        /// <summary>
        /// js. Перейти, если флаг знака (SF) равен 1
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfSign(MemoryOperand functionAddress)
        {
            Emit($"js {functionAddress}");
        }

        /// <summary>
        /// jns. Перейти, если флаг знака (SF) равен 0
        /// </summary>
        /// <param name="label">Функция к которой надо перейти</param>
        public static void JumpIfNotSign(string label)
        {
            Emit($"jns {label}");
        }
        /// <summary>
        /// jns. Перейти, если флаг знака (SF) равен 0
        /// </summary>
        /// <param name="functionAddress">Адрес функции к которой надо перейти</param>
        public static void JumpIfNotSign(MemoryOperand functionAddress)
        {
            Emit($"jns {functionAddress}");
        }

        #endregion

        #region Таблицы

        /// <summary>
        /// Создать таблицу страниц памяти
        /// </summary>
        /// <returns>Таблица страниц памяти</returns>
        /// <exception cref="AssemblerException"></exception>
        public static PageTableBuilder CreatePageTable()
        {
            if (_pageTableBuilder != null)
            {
                throw new AssemblerException("Таблица страниц памяти уже создана");
            }

            _pageTableBuilder ??= new();

            return _pageTableBuilder;
        }

        /// <summary>
        /// lgdt. Загрузить глобальную таблицу дескрипторов (GDT).
        /// </summary>
        /// <param name="gdt">Глобальная таблица дескрипторов, которую необходимо загрузить</param>
        public static void LoadGlobalDescriptorTable(GDT gdt)
        {
            Emit($"lgdt [{gdt.CodeName}]");
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
        /// lidt. Загрузить таблицу прерываний (IDT).
        /// </summary>
        /// <param name="idt">Таблица, которую необходимо загрузить</param>
        public static void LoadInterruptionDescriptorTable(IDT idt)
        {
            int count = 0;

            foreach (var section in idt.Sections)
            {
                for (int i = 0; i < Math.Max(section.RepeatCount, 1); i++)
                {
                    section.SetupHandler(idt, count * 8);
                    count++;
                }
            }

            Emit($"lidt [{idt.CodeName}]");
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
        /// <param name="origin">Указание на то, куда разместить следующий фрагмент кода</param>
        /// <returns>Название функции точки входа</returns>
        public static string StartWithMode(Mode mode, uint? origin = null)
        {
            Switch(mode);

            if (origin != null)
            {
                Origin(origin.Value);
            }

            Section(Assembler.Section.Text);
            SetGlobal(StartLabel);
            Label(StartLabel);

            return StartLabel;
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
            CurrentMode = cpuMode;
            Emit($"[BITS {(int)cpuMode}]", false);
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

        /// <summary>
        /// Подключить файл
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        public static void Include(string fileName)
        {
            Emit($"%include \"{fileName}\"");
        }

        /// <summary>
        /// Начать повторение. Этот кусок кода будет повторяться при компиляции заданное количество раз.
        /// </summary>
        /// <param name="count">Количество раз, которое надо повторить следующий кусок кода</param>
        public static void StartRepeat(long count)
        {
            Emit($"%rep {count}", true);
        }
        /// <summary>
        /// Заверить повторение кода
        /// </summary>
        public static void EndRepeat()
        {
            Emit("endrep", true);
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
        /// Создать строковую переменную
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная</returns>
        public static StringVariable CreateStringVariable(string name, Size size = Size.x16, bool isReserved = true)
        {
            StringVariable result = new(size, name, isReserved);
            _variables.Add(name, result);

            return result;
        }
        /// <summary>
        /// Создать строковую переменную
        /// </summary>
        /// <param name="name">Название переменной. Название должно быть уникальное, чтобы не было повторений</param>
        /// <param name="value">Значение переменной</param>
        /// <param name="size">Размер переменной. Принимаются только x16, x32, x64</param>
        /// <param name="isReserved">Является ли переменная зарезервированной.
        /// Если true - то переменная будет находится в <see cref="Section.Data"/>, 
        /// иначе в <see cref="Section.Bss"/></param>
        /// <returns>Переменная</returns>
        public static StringVariable CreateStringVariable(string name, string value, Size size = Size.x16, bool isReserved = true)
        {
            StringVariable result = new(size, name, isReserved)
            {
                Value = value
            };
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
        /// Добавить код из генератора кода
        /// </summary>
        /// <param name="codeGenerator">Генератор кода, который сгенерирует код для вставки</param>
        public static void Add(ICodeGenerator codeGenerator)
        {
            Add(codeGenerator, false);
        }
        /// <summary>
        /// Добавить код из генератора кода
        /// </summary>
        /// <param name="codeGenerator">Генератор кода, который сгенерирует код для вставки</param>
        /// <param name="delayed">
        /// Если true, то код будет добавлен в только в моменте получения исходного кода.
        /// Может пригодится когда надо добавить код независимо от текущей позиции
        /// </param>
        public static void Add(ICodeGenerator codeGenerator, bool delayed)
        {
            if (delayed)
            {
                _delayedCodeGenerators.Add(codeGenerator);
                return;
            }

            var startSection = CurrentSection;
            var code = codeGenerator.GenerateCode();

            if (code.Code == string.Empty)
            {
                return;
            }

            Section(code.Section);

            if (code.Section == Assembler.Section.Text && CurrentMode != code.Mode)
            {
                Switch(code.Mode);
            }

            Emit(code.Code, false);

            if (startSection != null)
            {
                Section(startSection.Value);
            }
        }

        /// <summary>
        /// Сбросить все изменения
        /// </summary>
        public static void Reset()
        {
            EndingTimes = null;
            EndingDoubleWord = null;

            BiosVideoInterruption.PrintStringCodeGenerator.IsAdded = false;
            _pageTableBuilder = null;
            _currentContext = null;
            _builder = null;

            _delayedCodeGenerators.Clear();
            _beginningBuilder.Clear();
            _textBuilder.Clear();
            _dataBuilder.Clear();
            _bssBuilder.Clear();
            _variables.Clear();
        }

        /// <summary>
        /// Получить составленный ассемблерный код
        /// </summary>
        /// <returns>Составленный код</returns>
        public static string GetAssembly()
        {
            StringBuilder result = new();
            result.Append(_beginningBuilder.ToString());

            foreach (var delayedCodeGenerator in _delayedCodeGenerators)
            {
                Add(delayedCodeGenerator);
            }

            AppendSection(result, Assembler.Section.Text, _textBuilder.ToString());
            
            if (_dataBuilder.Length > 0)
            {
                AppendSection(result, Assembler.Section.Data, _dataBuilder.ToString());
            }

            var dataVariables = GetVariables(true);
            var bssVariables = GetVariables(false);

            if (dataVariables != null)
            {
                result.AppendLine();
                result.AppendLine(dataVariables.TrimEnd());
            }

            //if (_tables.Count > 0 || _pageTableBuilder != null)
            //{
            //    AppendSection(result, Assembler.Section.Data, null);
                
            //    if (_tables.Count > 0)
            //    {
            //        foreach (var table in _tables.Values)
            //        {
            //            result.AppendLine(table.ToAssembler());
            //        }
            //    }
            //    if (_pageTableBuilder != null)
            //    {
            //        result.AppendLine(_pageTableBuilder.GenerateAssemblerCode());
            //    }
            //}
            if (bssVariables != null)
            {
                AppendSection(result, Assembler.Section.Bss, bssVariables);
            }

            result.AppendLine();

            if (EndingTimes != null)
            {
                result.AppendLine($"times {EndingTimes.Value} - ($ - $$) db 0");
            }
            if (EndingDoubleWord != null)
            {
                result.AppendLine($"dw 0x{EndingDoubleWord.Value:X}");
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
                    result.AppendLine(variable.AssemblerLine);
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

        internal const string Tab = "    ";
        private const string StartLabel = "_start";

        #endregion
    }
}
