using Ma.SafePerformer.Abstract;
using Ma.SafePerformer.Models;
using System;

namespace Ma.SafePerformer.Concrete
{
    /// <summary>
    /// Default SafePerformer to perform operations safely.
    /// </summary>
    public class SafePerformer
        : ISafePerformer
    {
        /// <summary>
        /// Logger.
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Configurationd details for SafePerformer.
        /// </summary>
        public SafePerformerConfig Config { get; private set; }

        /// <summary>
        /// Connection tester to use to test and connect when operation fails
        /// </summary>
        public IConnectionTester ConnectionTester { get; set; }

        /// <summary>
        /// Initialize safe performer.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When logManager is null.
        /// </exception>
        /// <param name="logger">Logger to log info.</param>
        public SafePerformer(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Logger = logger;

            // Initialize Config
            Config = SafePerformerConfig.DefaultConfig;
        }

        /// <summary>
        /// Perform operation safely and get results.
        /// </summary>
        /// <remarks>
        /// Perform operation, when exception occurs
        /// log the exception and test connection until
        /// connection succeeds.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// When operation is null.
        /// </exception>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="operation">Operation to perform.</param>
        /// <returns>Result of operation.</returns>
        public TResult Perform<TResult>(Func<TResult> operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            int attemptCount = 0;
            bool operationSucceeds = false;
            TResult result = default(TResult);

            while (!operationSucceeds)
            {
                try
                {
                    // Increase attempt count
                    attemptCount++;

                    // Try to perform operation
                    result = operation();

                    // Set operation state to true to exit the process
                    operationSucceeds = true;
                }
                catch (Exception ex)
                {
                    string note = string.Format("Return type: {0} | Method name: {1}",
                            typeof(TResult).Name,
                            operation.Method.Name);
                    Logger.Log(ex, note);                    

                    /// If attempt limit has not been exceeded connect and try again.
                    /// Otherwise throw exception and exit process.
                    if (attemptCount <= Config.OpertaionAttemptLimit)
                        Connect();
                    else
                        throw;
                }
            }

            return result;
        }

        /// <summary>
        /// Perform operation safely.
        /// </summary>
        /// <remarks>
        /// Perform operation, when exception occurs
        /// log the exception and test connection until
        /// connection succeeds.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// When operation is null.
        /// </exception>
        /// <param name="operation">Operation to perform.</param>
        /// <param name="connectionTester">Connection tester to connect when operation fails.</param>
        public void Perform(Action operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            // Use overload of this method
            Perform(() => { operation(); return 0; });
        }

        /// <summary>
        /// Test connections until succeeds.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// When connectionTester is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// When ConnectionTester has not been set.
        /// </exception>
        public void Connect()
        {
            if (ConnectionTester == null)
                throw new InvalidOperationException(string.Format(
                    "{0} must be set in order to be able to connect.", nameof(ConnectionTester)));

            bool connectionSucceeds = false;

            while (!connectionSucceeds)
            {
                Logger.Log(string.Format(
                    "Testing '{0}' connection.",
                    ConnectionTester.GetType().Name));

                connectionSucceeds = ConnectionTester.Test();

                if (connectionSucceeds)
                {
                    Logger.Log(
                        "Successfully connected. Continuing operation...");
                }
                else
                {
                    Logger.Log(string.Format(
                        "Connections failed too many times. Sleeping for {0} minutes.",
                        Config.ConnectAttemptBreakMinutes));

                    System.Threading.Thread.Sleep(
                        TimeSpan.FromMinutes(Config.ConnectAttemptBreakMinutes));
                }
            }
        }
    }
}
