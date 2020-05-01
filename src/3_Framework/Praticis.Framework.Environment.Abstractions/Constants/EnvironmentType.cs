
namespace Praticis.Framework.Environment.Abstractions
{
    public partial class EnvironmentType
    {
        public const string Development = "Development";
        public const string Staging = "Staging";
        public const string Production = "Production";

        // Add More Environment Type Here

        public const string Other = "Other";

        public static string SelectedValue { get; }
    }
}