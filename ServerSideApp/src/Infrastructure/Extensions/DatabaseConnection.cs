namespace ServerSideApp.Infrastructure.Extensions
{
    /// <summary>
    /// Define extension method for configuration of database connection.
    /// </summary>
    public static class DatabaseConnection
    {
        /// <summary>
        /// Get connection string type (Default/Docker) based on appsettings.json.
        /// </summary>
        /// <param name="isDockerSupport">Docker support enable.</param>
        /// <returns>Connection type.</returns>
        public static string ToDbConnectionString(this bool isDockerSupport)
        {
            string result = isDockerSupport switch
            {
                true => "DockerConnection",
                _ => "DefaultConnection",
            };
            return result;
        }
    }
}