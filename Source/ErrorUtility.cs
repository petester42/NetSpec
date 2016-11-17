using System;

namespace NetSpec
{
    internal sealed class ErrorUtility
    {
        internal static void raiseError(string message)
        {
            throw new Exception(message);
        }
    }
}
