using System;
using System.Diagnostics;

namespace NetSpec
{
    public partial class NetSpec
    {
        private Example example;

        public virtual void Spec()
        {

        }

        public void Run()
        {
            // [QuickConfiguration initialize];

            var world = World.sharedWorld;

            world.performWithCurrentExampleGroup(world.rootExampleGroupForSpecClass(GetType()), () =>
            {
                var spec = this;

                try
                {
                    spec.Spec();
                }
                catch (Exception exception)
                {
                    throw exception;
                    //TODO: clean up exception
                    // [NSException raise:NSInternalInconsistencyException
                    //             format:@"An exception occurred when building Quick's example groups.\n"
                    //  @"Some possible reasons this might happen include:\n\n"
                    //  @"- An 'expect(...).to' expectation was evaluated outside of "
                    //  @"an 'it', 'context', or 'describe' block\n"
                    //  @"- 'sharedExamples' was called twice with the same name\n"
                    //  @"- 'itBehavesLike' was called with a name that is not registered as a shared example\n\n"
                    //  @"Here's the original exception: '%@', reason: '%@', userInfo: '%@'",
                    //  exception.name, exception.reason, exception.userInfo];
                }

                testInvocations();
            });
        }

        private void testInvocations()
        {
            var examples = World.sharedWorld.examples(GetType());

            foreach (var example in examples)
            {
                this.example = example;
                example.run();
                this.example = null;
            }
        }

        public void recordFailure(string description, string filePath, uint lineNumber, bool expected)
        {
            if (this.example.isSharedExample)
            {
                filePath = this.example.callsite.file;
                lineNumber = this.example.callsite.line;
            }

            Console.WriteLine($"{description}, {filePath}, {lineNumber}, {expected}");
            
            //         this.currentSpec.testRun re
            // [currentSpec.testRun recordFailureWithDescription: description
            //                                            inFile: filePath
            //                                            atLine: lineNumber
            //                                          expected: expected];
        }
    }
}