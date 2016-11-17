using System;

namespace NetSpec.DSL
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

        internal func sharedExamples(_ name: String, closure: @escaping SharedExampleClosure)
        {
            registerSharedExample(name, closure: closure)
        }

        internal func describe(_ description: String, flags: FilterFlags, closure: () -> ())
        {
            guard currentExampleMetadata == nil else {
                raiseError("'describe' cannot be used inside '\(currentPhase)', 'describe' may only be used inside 'context' or 'describe'. ")
            }
            guard currentExampleGroup != nil else {
                raiseError("Error: example group was not created by its parent QuickSpec spec. Check that describe() or context() was used in QuickSpec.spec() and not a more general context (i.e. an XCTestCase test)")
            }
            let group = ExampleGroup(description: description, flags: flags)
            currentExampleGroup.appendExampleGroup(group)
            performWithCurrentExampleGroup(group, closure: closure)
        }

        internal func context(_ description: String, flags: FilterFlags, closure: () -> ())
        {
            guard currentExampleMetadata == nil else {
                raiseError("'context' cannot be used inside '\(currentPhase)', 'context' may only be used inside 'context' or 'describe'. ")
            }
            self.describe(description, flags: flags, closure: closure)
        }

        internal func fdescribe(_ description: String, flags: FilterFlags, closure: () -> ())
        {
            var focusedFlags = flags
            focusedFlags[Filter.focused] = true
            self.describe(description, flags: focusedFlags, closure: closure)
        }

        internal func xdescribe(_ description: String, flags: FilterFlags, closure: () -> ())
        {
            var pendingFlags = flags
            pendingFlags[Filter.pending] = true
            self.describe(description, flags: pendingFlags, closure: closure)
        }


        internal static void beforeEach(this World world, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError($"'beforeEach' cannot be used inside '{world.currentPhase()}', 'beforeEach' may only be used inside 'context' or 'describe'. ");
            }
            world.currentExampleGroup.hooks.appendBefore(closure);
        }

        internal func beforeEach(closure: @escaping BeforeExampleWithMetadataClosure)
        {
            currentExampleGroup.hooks.appendBefore(closure)
    }

        internal static void afterEach(this World world, Action closure)
        {
            if (world.currentExampleMetadata != null)
            {
                ErrorUtility.raiseError("'afterEach' cannot be used inside '{world.currentPhase()}', 'afterEach' may only be used inside 'context' or 'describe'. ");
            }
            world.currentExampleGroup.hooks.appendAfter(closure);
        }

        internal func afterEach(closure: @escaping AfterExampleWithMetadataClosure)
        {
            currentExampleGroup.hooks.appendAfter(closure)
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

        internal func fit(_ description: String, flags: FilterFlags, file: String, line: UInt, closure: @escaping () -> ())
        {
            var focusedFlags = flags
               focusedFlags[Filter.focused] = true
               self.it(description, flags: focusedFlags, file: file, line: line, closure: closure)
           }

        internal func xit(_ description: String, flags: FilterFlags, file: String, line: UInt, closure: @escaping () -> ())
        {
            var pendingFlags = flags
            pendingFlags[Filter.pending] = true
            self.it(description, flags: pendingFlags, file: file, line: line, closure: closure)
        }

        internal func itBehavesLike(_ name: String, sharedExampleContext: @escaping SharedExampleContext, flags: FilterFlags, file: String, line: UInt)
        {
            guard currentExampleMetadata == nil else {
                raiseError("'itBehavesLike' cannot be used inside '\(currentPhase)', 'itBehavesLike' may only be used inside 'context' or 'describe'. ")
            }
            let callsite = Callsite(file: file, line: line)
            let closure = World.sharedWorld.sharedExample(name)


        let group = ExampleGroup(description: name, flags: flags)
            currentExampleGroup.appendExampleGroup(group)
            performWithCurrentExampleGroup(group) {
                closure(sharedExampleContext)
        }

            group.walkDownExamples {
                (example: Example) in
            example.isSharedExample = true
                example.callsite = callsite
            }
        }


        internal func pending(_ description: String, closure: () -> ())
        {
            print("Pending: \(description)")
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