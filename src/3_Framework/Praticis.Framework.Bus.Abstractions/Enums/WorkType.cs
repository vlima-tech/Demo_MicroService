
using System;

namespace Praticis.Framework.Bus.Abstractions.Enums
{
    /// <summary>
    /// Represents an work 
    /// </summary>
    [Flags]
    public enum WorkType
    {
        /// <summary>
        /// Represents a Command work.
        /// Command works can be delivered to only one handler.
        /// </summary>
        Command = 1,

        /// <summary>
        /// Repreents a Event work.
        /// Event works can be delivered to many handlers,
        /// use it to listening events from others contexts and execute some action.
        /// </summary>
        Event = 2
    }
}