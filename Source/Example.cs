using System;

namespace NetSpec
{
    public sealed class Example : IEquatable<Example>
    {
        //TODO: move this to a global variable?
        private static int numberOfExamplesRun = 0;

        public bool isSharedExample = false;

        public Callsite callsite;

        internal ExampleGroup group;

        private string internalDescription { get; }
        private Action closure { get; }
        private FilterFlags flags { get; }

        internal Example(string description, Callsite callsite, FilterFlags flags, Action closure)
        {
            this.internalDescription = description;
            this.closure = closure;
            this.callsite = callsite;
            this.flags = flags;
        }

        public override string ToString()
        {
            return internalDescription;
        }

        public string name
        {
            get
            {
                if (group?.name == null)
                {
                    return ToString();
                }

                var groupName = group?.name;

                return $"{groupName}, {ToString()}";
            }
        }

        public void run()
        {
            var world = World.sharedWorld;


            if (numberOfExamplesRun == 0)
            {
                world.suiteHooks.executeBefores();
            }

            var exampleMetadata = new ExampleMetadata(example: this, exampleIndex: numberOfExamplesRun);
            world.currentExampleMetadata = exampleMetadata;


            world.exampleHooks.executeBefores(exampleMetadata);
            group.phase = HooksPhase.beforesExecuting;
            foreach (var before in group.befores)
            {
                before(exampleMetadata);
            }
            group.phase = HooksPhase.beforesFinished;

            closure();

            group.phase = HooksPhase.aftersExecuting;
            foreach (var after in group.afters)
            {
                after(exampleMetadata);
            }
            group.phase = HooksPhase.aftersFinished;
            world.exampleHooks.executeAfters(exampleMetadata);


            numberOfExamplesRun += 1;


            if (!world.isRunningAdditionalSuites && numberOfExamplesRun >= world.includedExampleCount)
            {
                world.suiteHooks.executeAfters();
            }
        }
        internal FilterFlags filterFlags
        {
            get
            {
                var aggregateFlags = flags;
                foreach (var pair in group.filterFlags)
                {
                    aggregateFlags[pair.Key] = pair.Value;
                }
                return aggregateFlags;
            }
        }

        public bool Equals(Example other)
        {
            return callsite == other.callsite;
        }

        public override bool Equals(Object other)
        {
            var castedOther = other as Example;
            if (castedOther == null)
            {
                return false;
            }
            return Equals(castedOther);
        }

        public override int GetHashCode()
        {
            return callsite.GetHashCode();
        }

        public static bool operator ==(Example leftOperand, Example rightOperand)
        {
            if (ReferenceEquals(null, leftOperand))
            {
                return ReferenceEquals(null, rightOperand);
            }

            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(Example leftOperand, Example rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
    }
}