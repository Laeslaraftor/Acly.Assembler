using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Статический класс с методами расширения обработчиков исключений
    /// </summary>
    public static class ExceptionsHandlersExtensions
    {
        /// <summary>
        /// Проверить является ли прерывание зарезервированным
        /// </summary>
        /// <param name="interruption">Прерывание, которое надо проверить</param>
        /// <returns>Является ли прерывание зарезервированным</returns>
        public static bool IsReserved(this CpuInterruption interruption)
        {
            var enumType = typeof(CpuInterruption);
            var enumMembers = typeof(CpuInterruption).GetMember(interruption.ToString());
            var enumValue = enumMembers.First(member => member.DeclaringType == enumType);

            if (enumValue != null)
            {
                return enumValue.GetCustomAttribute<CpuReservedAttribute>() != null;
            }

            return false;
        }
        /// <summary>
        /// Получить информацию об прерывании в виде строки.
        /// Строка включает название исключения и его типы
        /// </summary>
        /// <param name="interruption">Прерывание, информацию о котором надо получить</param>
        /// <returns>Информация об исключении</returns>
        public static string GetInfo(this CpuInterruption interruption)
        {
            string result = $"Stopped by {interruption}";
            List<string> types = new();
            var enumType = typeof(CpuInterruption);
            var enumMembers = typeof(CpuInterruption).GetMember(interruption.ToString());
            var enumValue = enumMembers.First(member => member.DeclaringType == enumType);

            if (enumValue == null)
            {
                return result;
            }

            foreach (var attribute in enumValue.GetCustomAttributes(false))
            {
                if (attribute is CpuFaultAttribute)
                {
                    types.Add("Fault");
                }
                else if(attribute is CpuTrapAttribute)
                {
                    types.Add("Trap");
                }
                else if (attribute is CpuInterruptAttribute)
                {
                    types.Add("Interrupt");
                }
                else if (attribute is CpuAbortAttribute)
                {
                    types.Add("Abort");
                }
                else if (attribute is CpuReservedAttribute)
                {
                    types.Add("Reserved");
                }
            }

            if (types.Count > 0)
            {
                result += "; Type: ";

                for (int i = 0; i < types.Count; i++)
                {
                    if (i > 0)
                    {
                        result += ", ";
                    }

                    result += types[i];
                }
            }

            return result;
        }
    }
}
