using System;
using System.IO;
using System.Text;
using WixToolset.Dtf.WindowsInstaller;

namespace KanidmInstaller.CustomActions
{
    public static class CustomActions
    {
        [CustomAction]
        public static ActionResult WriteKanidmConfig(Session session)
        {
            try
            {
                var configPath = session.CustomActionData["ConfigPath"];
                var configuredUri = session.CustomActionData["Uri"];

                if (!Uri.TryCreate(configuredUri, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeHttp))
                {
                    session.Log("KANIDM_URI must be an absolute HTTP or HTTPS URL.");
                    return ActionResult.Failure;
                }

                if (File.Exists(configPath))
                {
                    session.Log($"Preserving existing Kanidm configuration at {configPath}.");
                    return ActionResult.Success;
                }

                var configDirectory = Path.GetDirectoryName(configPath);
                if (string.IsNullOrWhiteSpace(configDirectory))
                {
                    session.Log("The Kanidm configuration path has no parent directory.");
                    return ActionResult.Failure;
                }

                Directory.CreateDirectory(configDirectory);

                var escapedUri = configuredUri
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"");
                var contents = $"uri = \"{escapedUri}\"{Environment.NewLine}";

                File.WriteAllText(configPath, contents, new UTF8Encoding(false));
                session.Log($"Created Kanidm configuration at {configPath}.");
                return ActionResult.Success;
            }
            catch (Exception exception)
            {
                session.Log($"Unable to create the Kanidm configuration: {exception}");
                return ActionResult.Failure;
            }
        }
    }
}
