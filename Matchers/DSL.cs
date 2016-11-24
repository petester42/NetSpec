
using System;
using System.Runtime.CompilerServices;
using NetSpec.Matchers;

namespace NetSpec
{
    /// Make an expectation on a given actual value. The value given is lazily evaluated.

    public partial class NetSpec
    {
        public Expectation<T> expect<T>(Func<T> expression, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            return new Expectation<T>(new Expression<T>(expression, new SourceLocation(file: file, line: line), isClosure: true));
        }

        /// Make an expectation on a given actual value. The closure is lazily invoked.
        // public Expectation<T> expect<T>([CallerFilePath] string file = null, [CallerLineNumber] uint line = 0, Func<T> expression = null)
        // {
        //     return new Expectation<T>(new Expression<T>(expression, new SourceLocation(file: file, line: line), isClosure: true));
        // }

        /// Always fails the test with a message and a specified location.
        public void fail(string message, SourceLocation location)
        {
            var handler = NimbleEnvironment.activeInstance.assertionHandler;
            handler.assert(false, message: new FailureMessage(stringValue: message), location: location);
        }

        /// Always fails the test with a message.
        public void fail(string message, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            fail(message, location: new SourceLocation(file: file, line: line));
        }

        /// Always fails the test.
        public void fail([CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            fail("fail() always fails", file: file, line: line);
        }

        /// Like Swift's precondition(), but raises NSExceptions instead of sigaborts
        internal void nimblePrecondition(Func<bool> expr, Func<string> name, Func<string> message, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            var result = expr();
            if (!result)
            {
                throw new Exception($"{name()} - {message()}");
                // preconditionFailure("\(name()) - \(message())", file: file, line: line)
            }
        }

        internal void internalError(string msg, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            //TODO: fix message
            throw new Exception(
                $"Nimble Bug Found: {msg} at {file}:{line}.\n" +
                "Please file a bug to Nimble: https://github.com/Quick/Nimble/issues with the " +
                "code snippet that caused this error."
            );

            // fatalError(
            //             $"Nimble Bug Found: {msg} at {file}:{line}.\n" +
            //             "Please file a bug to Nimble: https://github.com/Quick/Nimble/issues with the " +
            //             "code snippet that caused this error."
            //         );
        }
    }
}