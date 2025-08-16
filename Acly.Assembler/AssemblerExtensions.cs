using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Acly.Assembler
{
    /// <summary>
    /// Статический класс с методами расширения для упрощения работы с ассемблером
    /// </summary>
    public static class AssemblerExtensions
    {
        /// <summary>
        /// Получить короткое название типа данных
        /// </summary>
        /// <param name="prefix">Тип данных</param>
        /// <returns>Короткое название типа данных</returns>
        public static string GetShortName(this Prefix prefix)
        {
            var enumType = typeof(Prefix);
            var enumMembers = enumType.GetMember(prefix.ToString());
            var enumValue = enumMembers.First(member => member.DeclaringType == enumType);
            var description = enumValue.GetCustomAttribute<DescriptionAttribute>();

            if (description != null)
            {
                return description.Description;
            }

            return string.Empty;
        }
    }
}
