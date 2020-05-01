
using System;

namespace Praticis.Framework.Pipeline.Abstractions.Enums
{
    /// <summary>
    /// Represents a pipeline information type.
    /// </summary>
    [Flags]
    public enum PipelineInfoType
    {
        /// <summary>
        /// Represents a pipeline information value.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Represents a pipeline time (that can be start or finish time).
        /// </summary>
        Time = 2,
    }
}