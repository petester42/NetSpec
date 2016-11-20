using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetSpec
{
    internal sealed class World
    {
        internal ExampleGroup currentExampleGroup;

        internal ExampleMetadata currentExampleMetadata;

        internal bool isRunningAdditionalSuites = false;

        private Dictionary<String, ExampleGroup> specs = new Dictionary<String, ExampleGroup>();

        private Dictionary<string, Action<Func<Dictionary<string, string>>>> sharedExamples = new Dictionary<string, Action<Func<Dictionary<string, string>>>>();

        private Configuration configuration = new Configuration();

        private bool isConfigurationFinalized = false;

        internal ExampleHooks exampleHooks
        {
            get
            {
                return configuration.exampleHooks;
            }
        }

        internal SuiteHooks suiteHooks
        {
            get
            {
                return configuration.suiteHooks;
            }
        }

        private static readonly World instance = new World();

        private World()
        {

        }

        public static World sharedWorld
        {
            get
            {
                return instance;
            }
        }

        internal void configure(Action<Configuration> closure)
        {
            //TODO: fix assert message to make sense in c#
            Debug.Assert(!isConfigurationFinalized,
                   "Quick cannot be configured outside of a +[QuickConfiguration configure:] method. You should not call -[World configure:] directly. Instead, subclass QuickConfiguration and override the +[QuickConfiguration configure:] method.");
            closure(configuration);
        }

        internal void finalizeConfiguration()
        {
            isConfigurationFinalized = true;
        }

        internal ExampleGroup rootExampleGroupForSpecClass(Type cls)
        {
            var name = cls.ToString();

            try
            {
                var group = specs[name];
                return group;
            }
            catch
            {
                var group = new ExampleGroup("root example group", flags: new FilterFlags(), isInternalRootExampleGroup: true);
                specs[name] = group;
                return group;
            }
        }

        internal List<Example> examples(Type specClass)
        {
            // 1. Grab all included examples.
            var included = includedExamples;
            // 2. Grab the intersection of (a) examples for this spec, and (b) included examples.
            var spec = rootExampleGroupForSpecClass(specClass).examples.Where(x => included.Contains(x));
            // 3. Remove all excluded examples.
            return spec.Where(example => !this.configuration.exclusionFilters.Aggregate(false, (a, b) => a || b(example))).ToList();
        }

        internal void registerSharedExample(string name, Action<Func<Dictionary<string, string>>> closure)
        {
            raiseIfSharedExampleAlreadyRegistered(name);
            sharedExamples[name] = closure;
        }

        internal Action<Func<Dictionary<string, string>>> sharedExample(string name)
        {
            raiseIfSharedExampleNotRegistered(name);
            return sharedExamples[name];
        }

        internal int includedExampleCount
        {
            get
            {
                return includedExamples.Count;
            }
        }

        internal bool beforesCurrentlyExecuting
        {
            get
            {
                var suiteBeforesExecuting = suiteHooks.phase == HooksPhase.beforesExecuting;
                var exampleBeforesExecuting = exampleHooks.phase == HooksPhase.beforesExecuting;
                var groupBeforesExecuting = false;

                if (currentExampleMetadata?.example?.group != null)
                {
                    var runningExampleGroup = currentExampleMetadata?.example.group;
                    groupBeforesExecuting = runningExampleGroup.phase == HooksPhase.beforesExecuting;
                }

                return suiteBeforesExecuting || exampleBeforesExecuting || groupBeforesExecuting;
            }
        }

        internal bool aftersCurrentlyExecuting
        {
            get
            {
                var suiteAftersExecuting = suiteHooks.phase == HooksPhase.aftersExecuting;
                var exampleAftersExecuting = exampleHooks.phase == HooksPhase.aftersExecuting;
                var groupAftersExecuting = false;
                if (currentExampleMetadata?.example?.group != null)
                {
                    var runningExampleGroup = currentExampleMetadata?.example.group;
                    groupAftersExecuting = runningExampleGroup.phase == HooksPhase.aftersExecuting;
                }

                return suiteAftersExecuting || exampleAftersExecuting || groupAftersExecuting;
            }
        }

        internal void performWithCurrentExampleGroup(ExampleGroup group, Action closure)
        {
            var previousExampleGroup = currentExampleGroup;
            currentExampleGroup = group;

            closure();

            currentExampleGroup = previousExampleGroup;
        }

        private List<Example> allExamples
        {
            get
            {
                var all = new List<Example>();
                foreach (var tuple in specs)
                {
                    var group = tuple.Value;
                    group.walkDownExamples(x => all.Add(x));
                }
                return all;
            }
        }

        private List<Example> includedExamples
        {
            get
            {
                var all = allExamples;
                var included = all.Where(example =>
                {
                    return this.configuration.inclusionFilters.Aggregate(false, (a, b) => a || b(example));
                });

                if (included.Count() == 0 && configuration.runAllWhenEverythingFiltered)
                {
                    return all;
                }
                else
                {
                    return included.ToList();
                }
            }
        }

        private void raiseIfSharedExampleAlreadyRegistered(string name)
        {
            if (sharedExamples[name] != null)
            {
                ErrorUtility.raiseError($"A shared example named '{name}' has already been registered.");
            }
        }

        private void raiseIfSharedExampleNotRegistered(string name)
        {
            if (sharedExamples[name] == null)
            {
                var keys = sharedExamples.Keys.ToList();
                ErrorUtility.raiseError($"No shared example named '{name}' has been registered. Registered shared examples: '{keys}'");
            }
        }
    }
}

