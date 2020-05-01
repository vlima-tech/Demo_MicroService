
using System.Collections.Generic;

namespace Praticis.Framework.Pipeline.Abstractions
{
    public interface IPipelineStore : IList<PipelineInfo>
    {
        /// <summary>
        /// Verify if pipeline exists.
        /// </summary>
        /// <returns></returns>
        bool HasPipeline();

        /// <summary>
        /// Obtains a pipline by hash code.
        /// </summary>
        /// <param name="commandHash">The hash code of command or event</param>
        /// <returns></returns>
        PipelineInfo GetPipeline(int commandHash);
    }
}