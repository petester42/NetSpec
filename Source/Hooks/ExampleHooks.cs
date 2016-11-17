using System;
using System.Collections.Generic;

namespace NetSpec.Hooks
{
    internal sealed class ExampleHooks
    {
        internal List<Action<ExampleMetadata>> befores = new List<Action<ExampleMetadata>>();
        internal List<Action<ExampleMetadata>> afters = new List<Action<ExampleMetadata>>();
        internal HooksPhase phase = HooksPhase.nothingExecuted;

        internal void appendBefore(Action<ExampleMetadata> closure)
        {
            befores.Add(closure);
        }

        internal void appendBefore(Action closure)
        {
            befores.Add(exampleMetadata => closure());
        }

        internal void appendAfter(Action<ExampleMetadata> closure)
        {
            afters.Add(closure);
        }

        internal void appendAfter(Action closure)
        {
            befores.Add(exampleMetadata => closure());
        }

        internal void executeBefores(ExampleMetadata exampleMetadata)
        {
            phase = HooksPhase.beforesExecuting;
            foreach (var before in befores)
            {
                before(exampleMetadata);
            }

            phase = HooksPhase.beforesFinished;
        }

        internal void executeAfters(ExampleMetadata exampleMetadata)
        {
            phase = HooksPhase.aftersExecuting;
            foreach (var after in afters)
            {
                after(exampleMetadata);
            }

            phase = HooksPhase.aftersFinished;
        }
    }
}