using System.Collections.Generic;
using System.Linq;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Статический класс с методами расширения обработчиков исключений
    /// </summary>
    public static class ExceptionsHandlersExtensions
    {
        /// <summary>
        /// Получить информацию об исключении в виде строки.
        /// Строка включает название исключения и его типы
        /// </summary>
        /// <param name="exception">Исключение, информацию о котором надо получить</param>
        /// <returns>Информация об исключении</returns>
        public static string GetInfo(this CpuException exception)
        {
            string result = $"Exception: {exception}";
            List<string> types = new();
            var enumType = typeof(CpuException);
            var enumMembers = typeof(CpuException).GetMember(exception.ToString());
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
