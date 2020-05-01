
using System.Linq;
using System.Collections.Generic;

using Praticis.Framework.Pipeline.Abstractions;
using Praticis.Framework.Pipeline.Abstractions.Enums;

namespace Praticis.Framework.Pipeline.Collections
{
    public class PipelineInfoCollection : List<PipelineInfo>, IPipelineStore
    {
        public new void Add(PipelineInfo pipeline)
        {
            bool itemExists = true;
            PipelineInfo collectionItem;

            collectionItem = this.Where(x => x.HashCode == pipeline.HashCode).FirstOrDefault();

            if (collectionItem == null)
            {
                collectionItem = pipeline;
                itemExists = false;
            }

            switch (pipeline.InfoType)
            {
                case PipelineInfoType.Information:
                    collectionItem.DefineInformation(pipeline.Value);
                    break;

                case PipelineInfoType.Time:
                    collectionItem.DefineStartTime(pipeline.StartTime);
                    collectionItem.DefineFinishTime(pipeline.FinishTime);
                    break;
            }

            if (!itemExists)
                base.Add(collectionItem);
        }

        public bool HasPipeline() => this.Count > 0;

        public PipelineInfo GetPipeline(int commandHash)
        {
            return this.Where(p => p.HashCode == commandHash)
                .FirstOrDefault();
        }
    }
}