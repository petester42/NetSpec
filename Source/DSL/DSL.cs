
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetSpec
{
    public partial class NetSpec
    {
        public void beforeSuite(Action closure)
        {
            World.sharedWorld.beforeSuite(closure);
        }

        public void afterSuite(Action closure)
        {
            World.sharedWorld.afterSuite(closure);
        }

        public void sharedExamples(string name, Action closure)
        {
            World.sharedWorld.sharedExamples(name, closure: (function) => closure());
        }

        public void sharedExamples(string name, Action<Func<Dictionary<string, string>>> closure)
        {
            World.sharedWorld.sharedExamples(name, closure: closure);
        }

        public void describe(string description, Action closure, FilterFlags flags = null)
        {
            World.sharedWorld.describe(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void context(string description, Action closure, FilterFlags flags = null)
        {
            World.sharedWorld.context(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void beforeEach(Action closure)
        {
            World.sharedWorld.beforeEach(closure);
        }

        public void beforeEach(Action<ExampleMetadata> closure)
        {
            World.sharedWorld.beforeEach(closure: closure);
        }

        public void afterEach(Action closure)
        {
            World.sharedWorld.afterEach(closure);
        }

        public void afterEach(Action<ExampleMetadata> closure)
        {
            World.sharedWorld.afterEach(closure: closure);
        }

        public void it(string description, Action closure, FilterFlags flags = null, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            World.sharedWorld.it(description, flags: DefaultFlags(flags), file: file, line: line, closure: closure);
        }

        public void itBehavesLike(string name, FilterFlags flags = null, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            itBehavesLike(name, flags: DefaultFlags(flags), file: file, line: line, sharedExampleContext: () => new Dictionary<string, string>());
        }

        public void itBehavesLike(string name, Func<Dictionary<string, string>> sharedExampleContext, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            World.sharedWorld.itBehavesLike(name, sharedExampleContext: sharedExampleContext, flags: DefaultFlags(flags), file: file, line: line);
        }

        public void pending(string description, Action closure)
        {
            World.sharedWorld.pending(description, closure: closure);
        }

        public void xdescribe(string description, Action closure, FilterFlags flags = null)
        {
            World.sharedWorld.xdescribe(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void xcontext(string description, Action closure, FilterFlags flags = null)
        {
            xdescribe(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void xit(string description, Action closure, FilterFlags flags = null, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            World.sharedWorld.xit(description, flags: DefaultFlags(flags), file: file, line: line, closure: closure);
        }

        public void fdescribe(string description, Action closure, FilterFlags flags = null)
        {
            World.sharedWorld.fdescribe(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void fcontext(string description, Action closure, FilterFlags flags = null)
        {
            fdescribe(description, flags: DefaultFlags(flags), closure: closure);
        }

        public void fit(string description, Action closure, FilterFlags flags = null, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            World.sharedWorld.fit(description, flags: DefaultFlags(flags), file: file, line: line, closure: closure);
        }

        private FilterFlags DefaultFlags(FilterFlags flags)
        {
            if (flags == null)
            {
                return new FilterFlags();
            }

            return flags;
        }
    }
}