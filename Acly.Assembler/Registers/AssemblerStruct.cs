using System;
using System.Collections.Generic;
using System.Text;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Класс, реализующий ассемблерную структуру
    /// </summary>
    public class AssemblerStruct : ICodeGenerator
    {
        /// <summary>
        /// Создать новый экземпляр ассемблерной структуры
        /// </summary>
        public AssemblerStruct() : this("struct")
        {

        }
        /// <summary>
        /// Создать новый экземпляр ассемблерной структуры
        /// </summary>
        /// <param name="name">Название структуры</param>
        public AssemblerStruct(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Название структуры
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Должна ли структура находится в секции BSS
        /// </summary>
        public bool IsBss { get; set; }

        private readonly List<Field> _fields = new();

        #region Управление

        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField()
        {
            return CreateField(null);
        }
        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField(string? name)
        {
            Field result = new(name);
            _fields.Add(result);

            return result;
        }
        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="size">Размер поля</param>
        /// <param name="comment">Комментарий к полю</param>
        /// <param name="reserved">Зарезервировано ли поле</param>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField(string? name, Prefix size, string? comment = null, bool reserved = false)
        {
            return CreateField(name, size, 0, comment, reserved);
        }
        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="repeat">Количество повторений поля</param>
        /// <param name="size">Размер поля</param>
        /// <param name="comment">Комментарий к полю</param>
        /// <param name="reserved">Зарезервировано ли поле</param>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField(string? name, MemoryOperand repeat, Prefix size, string? comment = null, bool reserved = false)
        {
            return CreateField(name, repeat, size, 0, comment, reserved);
        }
        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="size">Размер поля</param>
        /// <param name="value">Значение поля</param>
        /// <param name="comment">Комментарий к полю</param>
        /// <param name="reserved">Зарезервировано ли поле</param>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField(string? name, Prefix size, MemoryOperand value, string? comment = null, bool reserved = false)
        {
            return CreateField(name, null, size, value, comment, reserved);
        }
        /// <summary>
        /// Создать новое поле
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="repeat">Количество повторений поля</param>
        /// <param name="size">Размер поля</param>
        /// <param name="value">Значение поля</param>
        /// <param name="comment">Комментарий к полю</param>
        /// <param name="reserved">Зарезервировано ли поле</param>
        /// <returns>Экземпляр нового поля</returns>
        public Field CreateField(string? name, MemoryOperand? repeat, Prefix size, MemoryOperand value, string? comment = null, bool reserved = false)
        {
            var field = CreateField(name);
            field.Size = size;
            field.Value = value;
            field.Repeat = repeat;
            field.Comment = comment;
            field.IsReserved = reserved;

            return field;
        }
        /// <summary>
        /// Удалить поле
        /// </summary>
        /// <param name="field">Поле, которое надо удалить</param>
        /// <returns>Было ли удалено поле</returns>
        public bool RemoveField(Field field)
        {
            return _fields.Remove(field);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public GeneratedCode GenerateCode()
        {
            StringBuilder builder = new();
            int maxNameLength = 0;

            var currentSection = Asm.CurrentSection;
            var section = Section.Data;

            if (IsBss)
            {
                section = Section.Bss;
            }

            if (currentSection != section)
            {
                Asm.Section(section);
            }

            builder.AppendLine($"{Name}:");

            foreach (var field in _fields)
            {
                if (field.Name != null)
                {
                    maxNameLength = Math.Max(maxNameLength, field.Name.Length);
                }
            }

            maxNameLength += 4;

            foreach (var field in _fields)
            {
                builder.AppendLine(field.ToString(maxNameLength));
            }

            if (currentSection != section)
            {
                Asm.Section(currentSection.GetValueOrDefault());
            }

            return new(section, builder.ToString());
        }

        #endregion

        #region Классы

        /// <summary>
        /// Поле ассемблерной структуры
        /// </summary>
        public class Field
        {
            /// <summary>
            /// Создать новый экземпляр поля
            /// </summary>
            /// <param name="name">Название поля</param>
            public Field(string? name)
            {
                Name = name;
            }

            /// <summary>
            /// Название поля
            /// </summary>
            public string? Name { get; }
            /// <summary>
            /// Размер поля
            /// </summary>
            public Prefix Size { get; set; }
            /// <summary>
            /// Значение поля
            /// </summary>
            public MemoryOperand Value { get; set; } = 0;
            /// <summary>
            /// Количество повторений поля
            /// </summary>
            public MemoryOperand? Repeat { get; set; }
            /// <summary>
            /// Комментарий к полю
            /// </summary>
            public string? Comment { get; set; }
            /// <summary>
            /// Является ли поле зарезервированным
            /// </summary>
            public bool IsReserved { get; set; }

            #region Управление

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <returns><inheritdoc/></returns>
            public override string ToString()
            {
                return ToString(8);
            }
            /// <summary>
            /// Получить ассемблерный вариант поля
            /// </summary>
            /// <param name="columnsWidth">Ширина колонки в символах</param>
            /// <returns>Ассемблерный вариант поля</returns>
            public string ToString(int columnsWidth)
            {
                string result = $"{Asm.Tab}";

                if (Name != null)
                {
                    string name = $".{Name}";
                    result += StaticWidthText(name, columnsWidth);
                }

                string value = $"{GetTypeName()} {Value}";

                if (Repeat != null)
                {
                    value = $"times {Repeat} {value}";
                }

                value = StaticWidthText(value, columnsWidth);
                result += value;
                result = StaticWidthText(result, columnsWidth * 2);
                
                if (Comment != null)
                {
                    result += $"; {Comment}";
                }

                return result;
            }

            private string GetTypeName()
            {
                if (IsReserved)
                {
                    return $"res{Size.ToString().ToLower()[0]}";
                }

                return Size.GetShortName();
            }

            #endregion

            #region Статика

            private static string StaticWidthText(string text, int width)
            {
                return text.PadRight(width, ' ');
            }

            #endregion
        }

        #endregion
    }
}
