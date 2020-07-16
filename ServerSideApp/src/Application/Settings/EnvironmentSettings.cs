namespace ServerSideApp.Application.Settings
{
    /// <summary>
    /// Application environment settings.
    /// </summary>
    public class EnvironmentSettings
    {
        /// <summary>
        /// Environment settings. "true" - use docker container, "false" - use IIS Express.
        /// </summary>
        public bool IsDockerSupport { get; set; }
    }
}