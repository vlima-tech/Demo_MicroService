
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Pipeline.Abstractions.Enums;

namespace Praticis.Framework.Pipeline.Abstractions
{
    public class PipelineInfo : Notification
    {
        public int HashCode { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime FinishTime { get; set; }
        public PipelineInfoType InfoType { get; set; }
        public TimeSpan Elapsed => this.FinishTime - this.StartTime;

        #region Constructors

        public PipelineInfo(int hashCode, DateTime startTime)
            : base(string.Empty, NotificationType.Pipeline)
        {
            this.HashCode = hashCode;
            this.StartTime = startTime;
            base.Time = startTime;
            this.InfoType = PipelineInfoType.Time;
        }

        public PipelineInfo(int hashCode, string message)
            : base(message, NotificationType.Pipeline)
        {
            this.HashCode = hashCode;
            this.InfoType = PipelineInfoType.Information;
        }

        public PipelineInfo(int hashCode, DateTime startTime, DateTime finishTime)
            : base(string.Empty, NotificationType.Pipeline)
        {
            this.HashCode = hashCode;
            this.StartTime = startTime;
            base.Time = startTime;
            this.FinishTime = finishTime;
            this.InfoType = PipelineInfoType.Time;
        }

        #endregion

        public void DefineStartTime(DateTime startTime)
        {
            this.Time = startTime;
            this.StartTime = startTime;
        }

        public void DefineFinishTime(DateTime finishTime)
        { this.FinishTime = finishTime; }

        public void DefineInformation(string information)
        { this.Value = information; }

        public bool IsInsideOf(PipelineInfo pipeline)
        {
            bool isInside = false;

            if (pipeline.StartTime > this.StartTime && pipeline.FinishTime < this.FinishTime)
                isInside = true;

            return isInside;
        }

        public override string ToString()
        {
            return string.Format("[{0}]: Code: {1}; Message: {2} ", this.NotificationType, this.Code, this.Value);
        }
    }
}