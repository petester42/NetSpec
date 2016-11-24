namespace NetSpec.Matchers
{
    /// Protocol for the assertion handler that Nimble uses for all expectations.
    public interface AssertionHandler
    {
        void assert(bool assertion, FailureMessage message, SourceLocation location);
    }
}