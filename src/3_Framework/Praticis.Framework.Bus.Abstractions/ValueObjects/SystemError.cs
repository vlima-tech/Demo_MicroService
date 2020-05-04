
using System;
using System.Runtime.CompilerServices;
using System.Text;

using Newtonsoft.Json;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public class SystemError : Notification
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectManipulated">The object data manipulated thet generate error.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="sourceMethod">Source method name origined error.</param>
        /// <param name="sourceFileName">Source file name origined error.</param>
        /// <param name="sourceLineNumber">Source line number origined error.</param>
        public SystemError(/*[NotNull]*/ object objectManipulated, /*[NotNull]*/ string errorMessage, string stackTrace = "", string innerException = "", 
            [CallerMemberName] string sourceMethod = "",  [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFileName = "")
            : base(Guid.NewGuid(), "", errorMessage, NotificationType.System_Error)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(errorMessage);

            builder.AppendLine("\n ## Data ## \n");
            builder.AppendLine(JsonConvert.SerializeObject(objectManipulated));
            
            builder.AppendLine("\n ## Ocurred On ## \n");
            builder.AppendLine($"Source File: {sourceFileName}");
            builder.AppendLine($"Method: {sourceMethod}");
            builder.AppendLine($"Line: {sourceLineNumber}");

            if (!string.IsNullOrEmpty(innerException))
            {
                builder.AppendLine("\n ## Inner Exception ## \n");
                builder.AppendLine($"Error: {innerException}");
            }

            if (!string.IsNullOrEmpty(stackTrace))
            {
                builder.AppendLine("\n ## Stack Trace ## \n");
                builder.AppendLine(stackTrace);
            }

            base.Value = builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectManipulated">The object data manipulated thet generate error.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="sourceMethod">Source method name origined error.</param>
        /// <param name="sourceFileName">Source file name origined error.</param>
        /// <param name="sourceLineNumber">Source line number origined error.</param>
        public SystemError(string errorMessage, [CallerMemberName] string sourceMethod = "",
            [CallerFilePath] string sourceFileName = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(errorMessage, NotificationType.System_Error)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(errorMessage);

            builder.AppendLine("\n\n ## Ocurred On ## \n\n");
            builder.AppendLine("Source File: " + sourceFileName);
            builder.AppendLine("Method: " + sourceMethod);
            builder.AppendLine("Line Number: " + sourceLineNumber);

            base.Value = builder.ToString();
        }

        #endregion

        public override string ToString() => base.Value;
    }
}