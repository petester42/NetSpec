using System;

namespace NetSpec.Matchers
{
    public class Expectation<T>
    {
        public Expression<T> expression;

        public Expectation(Expression<T> expression)
        {
            this.expression = expression;
        }

        public void verify(bool pass, FailureMessage message)
        {
            var handler = NimbleEnvironment.activeInstance.assertionHandler;
            handler.assert(pass, message: message, location: expression.location);
        }

        /// Tests the actual value using a matcher to match.
        public void to<U>(U matcher, string description = null) where U : Matcher<T>
        {
            var tuple = expressionMatches(expression, matcher: matcher, to: "to", description: description);
            verify(tuple.Item1, tuple.Item2);
        }

        /// Tests the actual value using a matcher to not match.
        public void toNot<U>(U matcher, string description = null) where U : Matcher<T>
        {
            var tuple = expressionDoesNotMatch(expression, matcher: matcher, toNot: "to not", description: description);
            verify(tuple.Item1, tuple.Item2);
        }

        internal static Tuple<bool, FailureMessage> expressionMatches<ExpressionType, U>(Expression<ExpressionType> expression, U matcher, string to, string description = null) where U : Matcher<ExpressionType>
        {
            var msg = new FailureMessage();
            msg.userDescription = description;
            msg.to = to;

            try
            {
                var pass = matcher.matches(expression, msg);

                if (string.IsNullOrEmpty(msg.actualValue))
                {
                    msg.actualValue = $"<{expression.evaluate().ToString()}?";
                }

                return Tuple.Create(pass, msg);
            }
            catch (Exception ex)
            {
                msg.actualValue = string.Format("an unexpected error thrown: {0}", ex.Message);
                return Tuple.Create(false, msg);
            }
        }

        internal static Tuple<bool, FailureMessage> expressionDoesNotMatch<ExpressionType, U>(Expression<ExpressionType> expression, U matcher, string toNot, string description) where U : Matcher<ExpressionType>
        {
            var msg = new FailureMessage();
            msg.userDescription = description;
            msg.to = toNot;

            try
            {
                var pass = matcher.doesNotMatch(expression, msg);
                
                if (string.IsNullOrEmpty(msg.actualValue))
                {
                    msg.actualValue = $"<{expression.evaluate().ToString()}?";
                }

                return Tuple.Create(pass, msg);
            }
            catch (Exception ex)
            {
                msg.actualValue = string.Format("an unexpected error thrown: {0}", ex.Message);
                return Tuple.Create(false, msg);
            }
        }
    }
}