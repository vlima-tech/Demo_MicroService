
namespace Microsoft.Extensions.DependencyInjection
{
    public class FrameworkOption
    {
        /// <summary>
        /// Enable configuration module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool LoadConfigurationModule { get; set; }

        /// <summary>
        /// Enable environment module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool LoadEnvironmentModule { get; set; }

        /// <summary>
        /// Enable loggin module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool LoadLoggingModule { get; set; }

        /// <summary>
        /// Enable pipeline module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool LoadPipelineModule { get; set; }

        /// <summary>
        /// Enable REST module. Set 'True' to enable or 'False' to desable.
        /// </summary>
        public bool LoadRESTModule { get; set; }

        public bool LoadWorkerModule { get; set; }

        public bool LoadAutoMapperModule { get; set; }

        /// <summary>
        /// Define all options modules with 'True'.
        /// </summary>
        public void LoadAllModules()
        {
            this.LoadConfigurationModule = true;
            this.LoadEnvironmentModule = true;
            this.LoadLoggingModule = true;
            this.LoadPipelineModule = true;
            this.LoadRESTModule = true;
            this.LoadWorkerModule = true;
            this.LoadAutoMapperModule = true;
        }
    }
}