using System;
using System.Collections.Generic;

namespace NetSpec
{
    internal sealed class SuiteHooks
    {
        internal List<Action> befores = new List<Action>();
        internal List<Action> afters = new List<Action>();
        internal HooksPhase phase = HooksPhase.nothingExecuted;

        internal void appendBefore(Action closure)
        {
            befores.Add(closure);
        }

        internal void appendAfter(Action closure)
        {
            afters.Add(closure);
        }

        internal void executeBefores()
        {
            phase = HooksPhase.beforesExecuting;
            foreach (var before in befores)
            {
                before();
            }
            phase = HooksPhase.beforesFinished;
        }

        internal void executeAfters()
        {
            phase = HooksPhase.aftersExecuting;
            foreach (var after in afters)
            {
                after();
            }
            phase = HooksPhase.aftersFinished;
        }
    }
}