
namespace Microsoft.Extensions.DependencyInjection
{
    public class FrameworkOption
    {
        /// <summary>
        /// Enable configuration module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool UseConfigurationModule { get; set; }

        /// <summary>
        /// Enable environment module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool UseEnvironmentModule { get; set; }

        /// <summary>
        /// Enable loggin module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool UseLoggingModule { get; set; }

        /// <summary>
        /// Enable pipeline module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool UsePipelineModule { get; set; }

        public bool UseWorkerModule { get; set; }

        public bool UseAutoMapperModule { get; set; }

        public bool UseKafkaBusModule { get; set; }

        public bool UseKafkaWorkerModule { get; set; }

        /// <summary>
        /// Define all options modules with 'True'.
        /// </summary>
        public void LoadAllModules()
        {
            this.UseConfigurationModule = true;
            this.UseEnvironmentModule = true;
            this.UseLoggingModule = true;
            this.UsePipelineModule = true;
            this.UseWorkerModule = true;
            this.UseAutoMapperModule = true;
            this.UseKafkaBusModule = true;
            this.UseKafkaWorkerModule = true;
        }
    }
}