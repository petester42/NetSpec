using System;
using System.Collections.Generic;
using NetSpec.Matchers;

namespace NetSpec
{
    public partial class NetSpec
    {
        public static NonNilMatcherFunc<T> equal<T>(T expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<T>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                var matches = actualValue.Equals(expectedValue) && expectedValue != null;
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return matches;
            });
        }

        public static NonNilMatcherFunc<T[]> equal<T>(T[] expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<T[]>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }


        //Generic Collections
        public static NonNilMatcherFunc<Dictionary<T, C>> equal<T, C>(Dictionary<T, C> expectedValue) where T : IEquatable<T> where C : IEquatable<C>
        {
            return new NonNilMatcherFunc<Dictionary<T, C>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<HashSet<T>> equal<T>(HashSet<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<HashSet<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<LinkedList<T>> equal<T>(LinkedList<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<LinkedList<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<List<T>> equal<T>(List<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<List<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<Queue<T>> equal<T>(Queue<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<Queue<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<SortedDictionary<T, C>> equal<T, C>(SortedDictionary<T, C> expectedValue) where T : IEquatable<T> where C : IEquatable<C>
        {
            return new NonNilMatcherFunc<SortedDictionary<T, C>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<SortedList<T, C>> equal<T, C>(SortedList<T, C> expectedValue) where T : IEquatable<T> where C : IEquatable<C>
        {
            return new NonNilMatcherFunc<SortedList<T, C>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<SortedSet<T>> equal<T>(SortedSet<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<SortedSet<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        public static NonNilMatcherFunc<Stack<T>> equal<T>(Stack<T> expectedValue) where T : IEquatable<T>
        {
            return new NonNilMatcherFunc<Stack<T>>((actualExpression, failureMessage) =>
            {
                failureMessage.postfixMessage = $"equal <{expectedValue.ToString()}>";
                var actualValue = actualExpression.evaluate();
                if (expectedValue == null || actualValue == null)
                {
                    if (expectedValue == null)
                    {
                        failureMessage.postfixActual = " (use beNil() to match nils)";
                    }
                    return false;
                }
                return expectedValue.Equals(actualValue);
            });
        }

        // public func ==<T: Equatable>(lhs: Expectation<T>, rhs: T?) {
        //     lhs.to(equal(rhs))
        // }

        // public func !=<T: Equatable>(lhs: Expectation<T>, rhs: T?) {
        //     lhs.toNot(equal(rhs))
        // }

        // public func ==<T: Equatable>(lhs: Expectation<[T]>, rhs: [T]?) {
        //     lhs.to(equal(rhs))
        // }

        // public func !=<T: Equatable>(lhs: Expectation<[T]>, rhs: [T]?) {
        //     lhs.toNot(equal(rhs))
        // }

        // public func ==<T>(lhs: Expectation<Set<T>>, rhs: Set<T>?) {
        //     lhs.to(equal(rhs))
        // }

        // public func !=<T>(lhs: Expectation<Set<T>>, rhs: Set<T>?) {
        //     lhs.toNot(equal(rhs))
        // }

        // public func ==<T: Comparable>(lhs: Expectation<Set<T>>, rhs: Set<T>?) {
        //     lhs.to(equal(rhs))
        // }

        // public func !=<T: Comparable>(lhs: Expectation<Set<T>>, rhs: Set<T>?) {
        //     lhs.toNot(equal(rhs))
        // }

        // public func ==<T: Equatable, C: Equatable>(lhs: Expectation<[T: C]>, rhs: [T: C]?) {
        //     lhs.to(equal(rhs))
        // }

        // public func !=<T: Equatable, C: Equatable>(lhs: Expectation<[T: C]>, rhs: [T: C]?) {
        //     lhs.toNot(equal(rhs))
        // }

        //     }
        // }
    }
}