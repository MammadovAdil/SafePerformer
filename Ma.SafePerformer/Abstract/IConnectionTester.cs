namespace Ma.SafePerformer.Abstract
{
    public interface IConnectionTester
    {
        /// <summary>
        /// Test the connection.
        /// </summary>
        /// <returns>True if connection succeeds/False otherwise.</returns>
        bool Test();
    }
}
