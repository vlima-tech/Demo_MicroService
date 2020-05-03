
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Settings
{
    public class Filter
    {
        #region Properties

        public string Topic { get; set; }

        public IEnumerable<EventType> EventTypes { get; set; }

        #endregion
    }
}