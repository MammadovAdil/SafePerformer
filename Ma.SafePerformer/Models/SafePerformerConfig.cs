namespace Ma.SafePerformer.Models
{
    /// <summary>
    /// Configuration details for safe performer.
    /// </summary>
    public class SafePerformerConfig
    {
        /// <summary>
        /// Maximum amount of attempts to complete operation
        /// after connection succeeds.
        /// </summary>
        public int OpertaionAttemptLimit { get; set; }
        /// <summary>
        /// Break time in minutes to wait before retrying to connect
        /// after fail.
        /// </summary>
        public int ConnectAttemptBreakMinutes { get; set; }

        private static SafePerformerConfig defaultConfig;
        /// <summary>
        /// Get default configuration values.
        /// </summary>
        public static SafePerformerConfig DefaultConfig
        {            
            get
            {
                // Initialize if needed
                if (defaultConfig == null)
                    defaultConfig = new SafePerformerConfig
                    {
                        OpertaionAttemptLimit = 10,
                        ConnectAttemptBreakMinutes = 30
                    };

                return defaultConfig;
            }
        }
    }
}
