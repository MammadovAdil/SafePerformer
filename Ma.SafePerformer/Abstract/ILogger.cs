using System;

namespace Ma.SafePerformer.Abstract
{
    public interface ILogger
    {
        /// <summary>
        /// Add message to log.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void Log(string message);

        /// <summary>
        /// Add exception details to log.
        /// </summary>
        /// <param name="ex">Exception details to log.</param>
        void Log(Exception ex);

        /// <summary>
        /// Add exception and note to log.
        /// </summary>
        /// <param name="ex">Exception to log.</param>
        /// <param name="note">Note to add.</param>
        void Log(Exception ex, string note);
    }
}
