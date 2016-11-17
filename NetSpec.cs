using System;
using System.Reflection;
using System.Linq;

namespace NetSpec
{
    public interface Matcher<ValueType>
    {
        bool matches(Expression<ValueType> actualExpression, string failureMessage);
        bool doesNotMatch(Expression<ValueType> actualExpression, string failureMessage);
    }

    public class MatcherFunc<T> : Matcher<T>
    {
        public Func<Expression<T>, string, bool> matcher;

        public MatcherFunc(Func<Expression<T>, string, bool> matcher)
        {
            this.matcher = matcher;
        }

        public bool matches(Expression<T> actualExpression, string failureMessage)
        {
            return this.matcher(actualExpression, failureMessage);
        }

        public bool doesNotMatch(Expression<T> actualExpression, string failureMessage)
        {
            return !this.matcher(actualExpression, failureMessage);
        }
    }

    public class Expression<T>
    {
        internal Func<T> _expression;

        public Expression(Func<T> expression)
        {
            _expression = expression;
        }

        public T evaluate()
        {
            return _expression();
        }

    }

    public class Expectation<T>
    {

        public Expression<T> expression;

        public Expectation(Expression<T> expression)
        {
            this.expression = expression;
        }

        public void verify(bool pass, string message)
        {
            Console.WriteLine("{0}, {1}", pass, message);
            // let handler = NimbleEnvironment.activeInstance.assertionHandler
            // handler.assert(pass, message: message, location: expression.location)
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

        internal static Tuple<bool, string> expressionMatches<ExpressionType, U>(Expression<ExpressionType> expression, U matcher, string to, string description) where U : Matcher<ExpressionType>
        {
            var msg = "";
            try
            {
                var pass = matcher.matches(expression, msg);

                return Tuple.Create(pass, msg);
            }
            catch (Exception ex)
            {
                msg = string.Format("an unexpected error thrown: {0}", ex.Message);
                return Tuple.Create(false, msg);
            }
        }

        internal static Tuple<bool, string> expressionDoesNotMatch<ExpressionType, U>(Expression<ExpressionType> expression, U matcher, string toNot, string description) where U : Matcher<ExpressionType>
        {
            var msg = "";
            try
            {
                var pass = matcher.doesNotMatch(expression, msg);

                return Tuple.Create(pass, msg);
            }
            catch (Exception ex)
            {
                msg = string.Format("an unexpected error thrown: {0}", ex.Message);
                return Tuple.Create(false, msg);
            }
        }
    }

    public partial class NetSpec
    {
        public static Expectation<T> expect<T>(Func<T> expression)
        {
            return new Expectation<T>(new Expression<T>(expression));
        }

        public static MatcherFunc<T> equal<T>(T expectedValue) where T : System.IEquatable<T>
        {
            return new MatcherFunc<T>((actualExpression, failureMessage) =>
            {
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null)
                {
                    return false;
                }

                if (actualValue == null)
                {
                    return false;
                }

                var matches = actualValue.Equals(expectedValue);
                return matches;
            });
        }

    }
}