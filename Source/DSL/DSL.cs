
using System;
using System.Runtime.CompilerServices;

namespace NetSpec.DSL
{
    //TODO: revisit default params
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
            World.sharedWorld.sharedExamples(name, closure: { (NSDictionary) in closure() });
        }

        public void sharedExamples(string name, closure: @escaping SharedExampleClosure)
        {
            World.sharedWorld.sharedExamples(name, closure: closure);
        }

        public void describe(string description, FilterFlags flags, Action closure)
        {
            World.sharedWorld.describe(description, flags: flags, closure: closure)
        }

        public void context(string description, FilterFlags flags, Action closure)
        {
            World.sharedWorld.context(description, flags: flags, closure: closure);
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


        public void it(string description, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0, Action closure = null)
        {
            World.sharedWorld.it(description, flags: flags, file: file, line: line, closure: closure);
        }


        public void itBehavesLike(string name, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0)
        {
            itBehavesLike(name, flags: flags, file: file, line: line, sharedExampleContext: { return [:] });
        }

        public void itBehavesLike(string name, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0, sharedExampleContext: @escaping SharedExampleContext)
        {
            World.sharedWorld.itBehavesLike(name, sharedExampleContext: sharedExampleContext, flags: flags, file: file, line: line);
        }

        public void pending(string description, Action closure)
        {
            World.sharedWorld.pending(description, closure: closure);
        }

        public void xdescribe(string description, FilterFlags flags, Action closure)
        {
            World.sharedWorld.xdescribe(description, flags: flags, closure: closure);
        }

        public void xcontext(string description, FilterFlags flags, Action closure)
        {
            xdescribe(description, flags: flags, closure: closure);
        }

        public void xit(string description, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0, Action closure = null)
        {
            World.sharedWorld.xit(description, flags: flags, file: file, line: line, closure: closure);
        }

        public void fdescribe(string description, FilterFlags flags, Action closure = null)
        {
            World.sharedWorld.fdescribe(description, flags: flags, closure: closure);
        }


        public void fcontext(string description, FilterFlags flags, Action closure = null)
        {
            fdescribe(description, flags: flags, closure: closure);
        }


        public void fit(string description, FilterFlags flags, [CallerFilePath] string file = null, [CallerLineNumber] uint line = 0, Action closure = null)
        {
            World.sharedWorld.fit(description, flags: flags, file: file, line: line, closure: closure);
        }
    }
}