using System;
using System.Collections.Generic;

namespace NetSpec
{
    public sealed class ExampleGroup
    {
        internal ExampleGroup parent;

        internal ExampleHooks hooks = new ExampleHooks();

        internal HooksPhase phase = HooksPhase.nothingExecuted;

        private string internalDescription { get; }

        private FilterFlags flags { get; }

        private bool isInternalRootExampleGroup { get; }

        private List<ExampleGroup> childGroups = new List<ExampleGroup>();

        private List<Example> childExamples = new List<Example>();


        internal ExampleGroup(string description, FilterFlags flags, bool isInternalRootExampleGroup = false)
        {
            this.internalDescription = description;
            this.flags = flags;
            this.isInternalRootExampleGroup = isInternalRootExampleGroup;
        }

        public override string ToString()
        {
            return internalDescription;
        }

        public List<Example> examples
        {
            get
            {
                var examples = childExamples;
                foreach (var group in childGroups)
                {
                    examples.AddRange(group.examples);
                }
                return examples;
            }
        }

        internal string name
        {
            get
            {
                if (parent != null)
                {
                    if (parent.name == null)
                    {
                        return ToString();
                    }

                    return $"{name}, {ToString()}";
                }
                else
                {
                    return isInternalRootExampleGroup ? null : ToString();
                }
            }
        }

        internal FilterFlags filterFlags
        {
            get
            {
                var aggregateFlags = flags;

                walkUp(group =>
                {
                    foreach (var pair in group.flags)
                    {
                        aggregateFlags[pair.Key] = pair.Value;
                    }
                });

                return aggregateFlags;
            }
        }

        internal List<Action<ExampleMetadata>> befores
        {
            get
            {
                var closures = new List<Action<ExampleMetadata>>();
                closures.AddRange(hooks.befores.Reversed());

                walkUp(group =>
                {
                    closures.AddRange(group.hooks.befores.Reversed());
                });

                return closures;
            }
        }

        internal List<Action<ExampleMetadata>> afters
        {
            get
            {
                var closures = hooks.afters;

                walkUp(group =>
                {
                    closures.AddRange(group.hooks.afters);
                });

                return closures;
            }
        }

        internal void walkDownExamples(Action<Example> callback)
        {
            foreach (var example in childExamples)
            {
                callback(example);
            }
            foreach (var group in childGroups)
            {
                group.walkDownExamples(callback);
            }
        }

        internal void appendExampleGroup(ExampleGroup group)
        {
            group.parent = this;
            childGroups.Add(group);
        }

        internal void appendExample(Example example)
        {
            example.group = this;
            childExamples.Add(example);
        }

        private void walkUp(Action<ExampleGroup> callback)
        {
            var group = this;
            var parent = group.parent;

            while (parent != null)
            {
                callback(parent);
                group = parent;
                parent = group.parent;
            }
        }
    }
}