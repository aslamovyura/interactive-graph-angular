namespace ServerSideApp.Web.Constants
{
    /// <summary>
    /// Define constants for initialization of application services.
    /// </summary>
    public class InitializationConstants
    {
        /// <summary>
        /// Migration error message.
        /// </summary>
        public const string MigrationError = "An error occurred while DB migrating.";

        /// <summary>
        /// Migration success message.
        /// </summary>
        public const string MigrationSuccess = "The database is successfully migrated.";

        /// <summary>
        /// Database seed error.
        /// </summary>
        public const string SeedError = "An error occurred while DB seeding.";

        /// <summary>
        /// Database seed success.
        /// </summary>
        public const string SeedSuccess = "The database is successfully seeded.";

        /// <summary>
        /// Database seeding is disabled.
        /// </summary>
        public const string SeedDisabled = "Initial database seeding is disabled!";

        /// <summary>
        /// Database seeding is enabled.
        /// </summary>
        public const string SeedEnabled = "Initial database seeding is enabled!";

        /// <summary>
        /// Web host is staring.
        /// </summary>
        public const string WebHostStarting = "Web host is starting.";

        /// <summary>
        /// Web host was terminated unexpectedly.
        /// </summary>
        public const string WebHostTerminated = "Web host was terminated unexpectedly!";
    }
}