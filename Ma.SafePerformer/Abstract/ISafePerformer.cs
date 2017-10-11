using Ma.SafePerformer.Models;
using System;

namespace Ma.SafePerformer.Abstract
{
    public interface ISafePerformer
    {
        /// <summary>
        /// Configurationd details for SafePerformer.
        /// </summary>
        SafePerformerConfig Config { get; }

        /// <summary>
        /// Connection tester to use to test and connect when operation fails
        /// </summary>
        IConnectionTester ConnectionTester { get; set; }

        /// <summary>
        /// Perform operation safely and get results.
        /// </summary>
        /// <remarks>
        /// Perform operation, when exception occurs
        /// log the exception and test connection until
        /// connection succeeds.
        /// </remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="operation">Operation to perform.</param>
        /// <returns>Result of operation.</returns>
        TResult Perform<TResult>(Func<TResult> operation);

        /// <summary>
        /// Perform operation safely.
        /// </summary>
        /// <remarks>
        /// Perform operation, when exception occurs
        /// log the exception and test connection until
        /// connection succeeds.
        /// </remarks>
        /// <param name="operation">Operation to perform.</param>
        void Perform(Action operation);

        /// <summary>
        /// Test connections until succeds.
        /// </summary>
        void Connect();
    }
}
