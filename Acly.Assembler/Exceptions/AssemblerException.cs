using System;
using System.Runtime.Serialization;

namespace Acly.Assembler
{
    /// <summary>
    /// Исключение ассемблера
    /// </summary>
    public class AssemblerException : Exception
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public AssemblerException()
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"><inheritdoc/></param>
        public AssemblerException(string message) : base(message)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"><inheritdoc/></param>
        /// <param name="innerException"><inheritdoc/></param>
        public AssemblerException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="info"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        protected AssemblerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
