
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class UriExtension
    {
        private static IEnumerable<string> ExtractFolderSections(string folder)
        {
            List<string> sections = new List<string>();

            sections.AddRange(folder.Split("//"));
            sections.AddRange(folder.Split("\\"));
            
            sections.RemoveAll(s => string.IsNullOrEmpty(s));

            return sections;
        }

        /// <summary>
        /// Append a folder to current path.
        /// You have a path <strong>/bin/netcore3.0</strong> and need append a folder to path
        /// <list type=">bullet">
        /// <item>Use: var path = new Uri(currentPath).Append("logs")</item>
        /// <item>Output: /bin/netcore3.0/logs/</item>
        /// </list>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="folderName">The folder or file name.</param>
        /// <returns></returns>
        public static Uri Append(this Uri uri, string folderName)
        {
            //TODO: Finalizar implementação do ExtractFolderSections para evitar erros causados por má formação do parâmetro Folder Name.
            //var teste = ExtractFolderSections(folderName);

            char folderDivision = '/';
            string path;

            if (uri.AbsolutePath.Contains('\\'))
                folderDivision = '\\';

            path = uri.AbsolutePath;
            path += path.EndsWith(folderDivision) ? string.Empty : folderDivision.ToString();
            path += folderName;

            path = path.Replace("%20", " ")
                .Replace("%2520", " ");

            return new Uri(path);
        }

        /// <summary>
        /// Build a path appending news folders and/or filename.
        /// You have a path <strong>/bin/netcore3.0</strong> and need append folders "logs", "pipeline" and "log.txt" to path
        /// <list type=">bullet">
        /// <item>Use: var path = new Uri(currentPath).Append("logs").Append("pipeline").Append("log.txt")</item>
        /// <item>Output: /bin/netcore3.0/logs/pipeline/log.txt</item>
        /// </list>
        /// </summary>
        /// <param name="uri">The current uri.</param>
        /// <param name="folders">The folder list. You can insert filename to end and genarate a file path.</param>
        /// <returns></returns>
        public static Uri Append(this Uri uri, params string[] folders)
        {
            char folderDivision = '/';
            string path;
            
            if (uri.AbsolutePath.Contains('\\'))
                folderDivision = '\\';

            path = uri.AbsolutePath;
            path += path.EndsWith(folderDivision) ? string.Empty : folderDivision.ToString();

            foreach (var folder in folders)
                path += folder + folderDivision;

            path = path.Replace("%20", " ")
                .Replace("%2520", " ");

            return new Uri(path);
        }

        public static Uri ReplaceParameter(this Uri uri, KeyValuePair<string, string> urlParameter)
        {   
            return new Uri(uri.AbsolutePath.Replace(urlParameter.Key, urlParameter.Value));
        }

        public static Uri ReplaceParameters(this Uri uri, IDictionary<string, string> uriParameters)
        {
            string uriReplaced = uri.OriginalString;

            foreach (var uriParameter in uriParameters)
                uriReplaced = uriReplaced.Replace(uriParameter.Key, uriParameter.Value);

            return new Uri(uriReplaced);
        }
    }
}