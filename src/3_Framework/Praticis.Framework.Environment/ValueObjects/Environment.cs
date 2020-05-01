
using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.FileProviders;

using Praticis.Framework.Environment.Abstractions;

namespace Praticis.Framework.Environment
{
    public partial class Environment : IEnvironment
    {
        /// <summary>
        /// The application name.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The environment name.
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// The application directory.
        /// </summary>
        public string ApplicationRootPath { get; set; }

        /// <summary>
        /// The configuration directory.
        /// </summary>
        public string ConfigurationRootPath { get; set; }

        /// <summary>
        /// The contents or files of the application directory.
        /// </summary>
        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }

        #region Constructors

        /// <summary>
        /// Create default environment, development.
        /// </summary>
        public Environment()
        {
            this.DefineEnvironmentName(EnvironmentType.Development);

            this.ApplicationRootPath = AppDomain.CurrentDomain.BaseDirectory;
            this.ConfigurationRootPath = AppDomain.CurrentDomain.BaseDirectory;
            this.ContentRootPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="environment">The environment name. Use EnvironmentType class to get it easy.</param>
        /// <param name="applicationRootPath">The application directory.</param>
        /// <param name="configurationRootPath">The configuration directory.</param>
        /// <param name="contentRootPath">The contents or files of the application directory.</param>
        public Environment(string environment, Uri applicationRootPath = null, Uri configurationRootPath = null, Uri contentRootPath = null)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            this.DefineEnvironmentName(environment);

            this.ApplicationRootPath = applicationRootPath     != null ? applicationRootPath.AbsolutePath   : baseDirectory;
            this.ConfigurationRootPath = configurationRootPath != null ? configurationRootPath.AbsolutePath : baseDirectory;
            this.ContentRootPath = contentRootPath             != null ? contentRootPath.AbsolutePath       : baseDirectory;
        }

        #endregion

        #region Factories

        public static Environment CreateNew([NotNull] string environmentName, string applicationRootPath = null,
            string configurationRootPath = null, string contentRootPath = null)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return new Environment
            {
                EnvironmentName = environmentName,
                ApplicationRootPath = applicationRootPath     ?? baseDirectory,
                ConfigurationRootPath = configurationRootPath ?? baseDirectory,
                ContentRootPath = contentRootPath             ?? baseDirectory,
            };
        }

        #endregion

        /// <summary>
        /// Define environment name.
        /// </summary>
        /// <param name="environmentName">The environment name.</param>
        public virtual void DefineEnvironmentName([NotNull] string environmentName) => this.EnvironmentName = environmentName;

        /// <summary>
        /// Define application directory.
        /// </summary>
        /// <param name="appRootPath">The application path.</param>
        public virtual void DefineApplicationRootPath([NotNull] string appRootPath) => this.ApplicationRootPath = appRootPath;

        /// <summary>
        /// Define configuration directory.
        /// </summary>
        /// <param name="configRootPath">The configuration path.</param>
        public virtual void DefineConfigurationRootPath([NotNull] string configRootPath) => this.ConfigurationRootPath = configRootPath;

        /// <summary>
        /// Define contents or files directory.
        /// </summary>
        /// <param name="contentRootPath">The path of contents or files of the application.</param>
        public virtual void DefineContentRootPath([NotNull] string contentRootPath) => this.ContentRootPath = contentRootPath;

        #region Environment Types

        /// <summary>
        /// Verify if is development environment.
        /// </summary>
        /// <returns>Return 'True' to development environment or 'False' to not.</returns>
        public virtual bool IsDevelopment()
        { return this.EnvironmentName.ToLower().Equals(EnvironmentType.Development.ToLower()); }

        /// <summary>
        /// Verify if is production environment.
        /// </summary>
        /// <returns>Return 'True' to production environment or 'False' to not.</returns>
        public virtual bool IsProduction()
        { return this.EnvironmentName.ToLower().Equals(EnvironmentType.Production.ToLower()); }

        /// <summary>
        /// Verify if is staging environment.
        /// </summary>
        /// <returns>Return 'True' to staging environment or 'False' to not.</returns>
        public virtual bool IsStaging()
        { return this.EnvironmentName.ToLower().Equals(EnvironmentType.Staging.ToLower()); }

        /// <summary>
        /// Verify if environment name informed math with current environment.
        /// </summary>
        /// <returns>Return 'True' if match or 'False' if not.</returns>
        public virtual bool IsEnvironment(string environmentName)
        { return this.EnvironmentName.ToLower().Equals(environmentName.ToLower()); }

        #endregion
    }
}