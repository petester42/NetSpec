using System;

namespace NetSpec.Matchers
{
    public class Expression<T>
    {
        internal Func<bool, T> _expression;
        internal bool _withoutCaching;
        public SourceLocation location;
        public bool isClosure;


        public Expression(Func<T> expression, SourceLocation location, bool isClosure = true)
        {
            this._expression = memoizedClosure(expression);
            this.location = location;
            this._withoutCaching = false;
            this.isClosure = isClosure;
        }

        public Expression(Func<bool, T> memoizedExpression, SourceLocation location, bool withoutCaching, bool isClosure = true)
        {
            this._expression = memoizedExpression;
            this.location = location;
            this._withoutCaching = withoutCaching;
            this.isClosure = isClosure;
        }

        public Expression<U> cast<U>(Func<T, U> block)
        {
            return new Expression<U>(expression: () => block(this.evaluate()), location: this.location, isClosure: this.isClosure);
        }

        public T evaluate()
        {
            return this._expression(_withoutCaching);
        }

        public Expression<T> withoutCaching()
        {
            return new Expression<T>(memoizedExpression: this._expression, location: this.location, withoutCaching: true, isClosure: this.isClosure);
        }

        internal static Func<bool, U> memoizedClosure<U>(Func<U> closure)
        {
            U cache = default(U);
            return withoutCaching =>
            {
                if (withoutCaching || cache == null)
                {
                    cache = closure();
                }
                return cache;
            };
        }
    }
}