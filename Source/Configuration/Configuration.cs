using System;
using System.Collections.Generic;

namespace NetSpec.Configuration
{
    public sealed class Configuration
    {
        internal ExampleHooks exampleHooks = new ExampleHooks();
        internal SuiteHooks suiteHooks = new SuiteHooks();
        internal List<Func<Example, bool>> exclusionFilters = new List<Func<Example, bool>>()
        {
            example => {
                try {
                    var pending = example.filterFlags[Filter.pending];
                    return pending;
                }   catch {
                    return false;
                }
            }
        };

        internal List<Func<Example, bool>> inclusionFilters = new List<Func<Example, bool>>()
        {
            example => {
                try {
                    var focused = example.filterFlags[Filter.focused];
                    return focused;
                }   catch {
                    return false;
                }
            }
        };

        public bool runAllWhenEverythingFiltered = true;

        public void include(Func<Example, bool> filter)
        {
            inclusionFilters.Add(filter);
        }

        public void exclude(Func<Example, bool> filter)
        {
            exclusionFilters.Add(filter);
        }

        public void beforeEach(Action<ExampleMetadata> closure)
        {
            exampleHooks.appendBefore(closure);
        }

        public void beforeEach(Action closure)
        {
            exampleHooks.appendBefore(closure);
        }

        public void afterEach(Action<ExampleMetadata> closure)
        {
            exampleHooks.appendAfter(closure);
        }
        public void afterEach(Action closure)
        {
            exampleHooks.appendAfter(closure);
        }

        public void beforeSuite(Action closure)
        {
            suiteHooks.appendBefore(closure);
        }

        public void afterSuite(Action closure)
        {
            suiteHooks.appendAfter(closure);
        }
    }

}