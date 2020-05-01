
using System;
using System.Runtime.CompilerServices;
using System.Text;

using Newtonsoft.Json;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public class Log : Notification
    {
        /// <summary>
        /// Create a log notification
        /// </summary>
        /// <param name="message">The log message.</param>
        public Log(string message, [CallerMemberName] string sourceMethod = "", 
            [CallerFilePath] string sourceFileName = "", [CallerLineNumber] int sourceLineNumber = 0) 
            : base(message, NotificationType.Log)
        {
            base.Value = $"{message} [Line: {sourceLineNumber}; Method: {sourceMethod}; File: {sourceFileName}]";
        }

        /// <summary>
        /// Create a log notification
        /// </summary>
        /// <param name="message">The log message.</param>
        /// /// <param name="objectManipulated">The object data manipulated thet generate error.</param>
        /// <param name="sourceMethod">Source method name origined log.</param>
        /// <param name="sourceFileName">Source file name origined log.</param>
        /// <param name="sourceLineNumber">Source line number origined log.</param>
        public Log(string message, object objectManipulated, [CallerMemberName] string sourceMethod = "",
            [CallerFilePath] string sourceFileName = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(Guid.NewGuid(), "", message, NotificationType.Log)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("\n");
            builder.AppendLine("Message: " + message);
            builder.AppendLine("\n\n ## Ocurred On ## \n\n");
            builder.AppendLine("Source File: " + sourceFileName);
            builder.AppendLine("Method: " + sourceMethod);
            builder.AppendLine("Line Number: " + sourceLineNumber);
            builder.AppendLine("Data: " + JsonConvert.SerializeObject(objectManipulated));

            base.Value = builder.ToString();
        }
    }
}