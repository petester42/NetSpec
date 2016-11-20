using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NetSpec
{
    internal static class WorldExtensions
    {
        internal static void beforeSuite(this World world, Action closure)
        {
            world.suiteHooks.appendBefore(closure);
        }

        internal static void afterSuite(this World world, Action closure)
        {
            world.suiteHooks.appendAfter(closure);
        }

        internal static void sharedExamples(this World world, string name, Action<Func<Dictionary<string, string>>> closure)
        {
            world.registerSharedExample(name, closure);
        }

        internal static void describe(this World world, string description, FilterFlags flags, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError($"'describe' cannot be used inside '{world.currentPhase()}', 'describe' may only be used inside 'context' or 'describe'. ");
            }
            if (world.currentExampleGroup == null)
            {
                //TODO: add message that isnt quick
                ErrorUtility.raiseError("Error: example group was not created by its parent QuickSpec spec. Check that describe() or context() was used in QuickSpec.spec() and not a more general context (i.e. an XCTestCase test)");
            }
            var group = new ExampleGroup(description, flags);
            world.currentExampleGroup.appendExampleGroup(group);
            world.performWithCurrentExampleGroup(group, closure: closure);
        }

        internal static void context(this World world, string description, FilterFlags flags, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError($"'context' cannot be used inside '{world.currentPhase()}', 'context' may only be used inside 'context' or 'describe'. ");
            }
            world.describe(description, flags, closure: closure);
        }

        internal static void fdescribe(this World world, string description, FilterFlags flags, Action closure)
        {
            var focusedFlags = flags;
            focusedFlags[Filter.focused] = true;
            world.describe(description, focusedFlags, closure);
        }

        internal static void xdescribe(this World world, string description, FilterFlags flags, Action closure)
        {
            var pendingFlags = flags;
            pendingFlags[Filter.pending] = true;
            world.describe(description, pendingFlags, closure);
        }


        internal static void beforeEach(this World world, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError($"'beforeEach' cannot be used inside '{world.currentPhase()}', 'beforeEach' may only be used inside 'context' or 'describe'. ");
            }
            world.currentExampleGroup.hooks.appendBefore(closure);
        }

        internal static void beforeEach(this World world, Action<ExampleMetadata> closure)
        {
            world.currentExampleGroup.hooks.appendBefore(closure);
        }

        internal static void afterEach(this World world, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError("'afterEach' cannot be used inside '{world.currentPhase()}', 'afterEach' may only be used inside 'context' or 'describe'. ");
            }
            world.currentExampleGroup.hooks.appendAfter(closure);
        }

        internal static void afterEach(this World world, Action<ExampleMetadata> closure)
        {
            world.currentExampleGroup.hooks.appendAfter(closure);
        }

        internal static void it(this World world, string description, FilterFlags flags, string file, uint line, Action closure)
        {
            if (world.beforesCurrentlyExecuting)
            {
                ErrorUtility.raiseError("'it' cannot be used inside 'beforeEach', 'it' may only be used inside 'context' or 'describe'. ");
            }

            if (world.aftersCurrentlyExecuting)
            {
                ErrorUtility.raiseError("'it' cannot be used inside 'afterEach', 'it' may only be used inside 'context' or 'describe'. ");
            }

            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError("'it' cannot be used inside 'it', 'it' may only be used inside 'context' or 'describe'. ");
            }
            var callsite = new Callsite(file: file, line: line);
            var example = new Example(description: description, callsite: callsite, flags: flags, closure: closure);
            world.currentExampleGroup.appendExample(example);
        }

        internal static void fit(this World world, string description, FilterFlags flags, string file, uint line, Action closure)
        {
            var focusedFlags = flags;
            focusedFlags[Filter.focused] = true;
            world.it(description, flags: focusedFlags, file: file, line: line, closure: closure);
        }

        internal static void xit(this World world, string description, FilterFlags flags, string file, uint line, Action closure)
        {
            var pendingFlags = flags;
            pendingFlags[Filter.pending] = true;
            world.it(description, flags: pendingFlags, file: file, line: line, closure: closure);
        }

        internal static void itBehavesLike(this World world, string name, Func<Dictionary<string, string>> sharedExampleContext, FilterFlags flags, string file, uint line)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError($"'itBehavesLike' cannot be used inside '{world.currentPhase()}', 'itBehavesLike' may only be used inside 'context' or 'describe'. ");
            }
            var callsite = new Callsite(file, line);
            var closure = World.sharedWorld.sharedExample(name);


            var group = new ExampleGroup(name, flags);
            world.currentExampleGroup.appendExampleGroup(group);
            world.performWithCurrentExampleGroup(group, () =>
            {
                closure(sharedExampleContext);
            });

            group.walkDownExamples(example =>
            {
                example.isSharedExample = true;
                example.callsite = callsite;
            });
        }

        internal static void pending(this World world, string description, Action closure)
        {
            Debug.WriteLine($"Pending: {description}");
        }

        private static string currentPhase(this World world)
        {
            if (world.beforesCurrentlyExecuting)
            {
                return "beforeEach";
            }
            else if (world.aftersCurrentlyExecuting)
            {
                return "afterEach";
            }

            return "it";
        }
    }
}