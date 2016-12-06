using System;

namespace NetSpec.Matchers
{
    public class NetSpecAssertionHandler : AssertionHandler
    {
        public void assert(bool assertion, FailureMessage message, SourceLocation location)
        {
            if (!assertion)
            {
                recordFailure($"{message.stringValue}\n", location: location);
            }
        }

        private void recordFailure(string message, SourceLocation location) {
            Console.Write($"{message}");
        }
    }
}