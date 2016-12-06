using System.Collections.Generic;

namespace NetSpec
{

    internal interface TestSuiteBuilder
    {
        TestSuite buildTestSuite();
    }

    public sealed class TestSuite
    {

        private static HashSet<string> builtTestSuites = new HashSet<string>();

        public static TestSuite selectedTestSuite(string name)
        {
            try
            {
                var builder = new SelectedTestSuiteBuilder(name);

                if (builtTestSuites.Contains(builder.testSuiteClassName)) {
                    return null;
                }
                else
                {
                    builtTestSuites.Add(builder.testSuiteClassName);
                    return builder.buildTestSuite();
                }
            }
            catch
            {
                return null;
            }

        }
    }
}