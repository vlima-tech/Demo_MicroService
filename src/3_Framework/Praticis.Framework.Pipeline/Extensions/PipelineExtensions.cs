
using System.Linq;

using Praticis.Framework.Pipeline.Abstractions;
using Praticis.Framework.Pipeline.Collections;

namespace Praticis.Framework.Pipeline
{
    public static class PipelineExtension
    {
        public static PipelineInfo GetPipeline(this PipelineInfoCollection pipelineCollection, int commandHash)
        {
            return pipelineCollection.Where(p => p.HashCode == commandHash)
                .FirstOrDefault();
        }
    }
}