using System;

namespace NetSpec.Matchers
{
    public class MatcherFunc<T> : Matcher<T>
    {
        public Func<Expression<T>, FailureMessage, bool> matcher;

        public MatcherFunc(Func<Expression<T>, FailureMessage, bool> matcher)
        {
            this.matcher = matcher;
        }

        public bool matches(Expression<T> actualExpression, FailureMessage failureMessage)
        {
            return matcher(actualExpression, failureMessage);
        }

        public bool doesNotMatch(Expression<T> actualExpression, FailureMessage failureMessage)
        {
            return !matcher(actualExpression, failureMessage);
        }
    }

    public class NonNilMatcherFunc<T> : Matcher<T>
    {
        public Func<Expression<T>, FailureMessage, bool> matcher;

        public NonNilMatcherFunc(Func<Expression<T>, FailureMessage, bool> matcher)
        {
            this.matcher = matcher;
        }

        public bool matches(Expression<T> actualExpression, FailureMessage failureMessage)
        {
            var pass = matcher(actualExpression, failureMessage);

            if (attachNilErrorIfNeeded(actualExpression, failureMessage: failureMessage))
            {
                return false;
            }
            return pass;
        }

        public bool doesNotMatch(Expression<T> actualExpression, FailureMessage failureMessage)
        {
            var pass = !matcher(actualExpression, failureMessage);

            if (attachNilErrorIfNeeded(actualExpression, failureMessage: failureMessage))
            {
                return false;
            }
            return pass;
        }

        internal bool attachNilErrorIfNeeded(Expression<T> actualExpression, FailureMessage failureMessage)
        {
            if (actualExpression.evaluate() == null)
            {
                failureMessage.postfixActual = " (use beNil() to match nils)";
                return true;
            }
            return false;
        }
    }
}