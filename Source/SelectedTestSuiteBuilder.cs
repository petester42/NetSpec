using System;
using System.IO;

namespace NetSpec
{
    internal sealed class SelectedTestSuiteBuilder : TestSuiteBuilder
    {

        internal Type testCaseClass;

        internal string testSuiteClassName
        {
            get
            {
                return testCaseClass.ToString();
            }
        }

        internal SelectedTestSuiteBuilder(string name)
        {
            var testCaseClass = testCaseClassForTestCaseWithName(name);
            if (testCaseClass == null)
            {
                this.testCaseClass = null;
                throw new Exception("no test class");
            }

            this.testCaseClass = testCaseClass;
        }


        public TestSuite buildTestSuite()
        {
            //TODO: build a suite somehow
            return null;
            // return new TestSuite(testCaseClass);
        }

        private Type testCaseClassForTestCaseWithName(string name)
        {
            Func<string, string> extractClassName = (string nameToExtract) =>
            {
                return nameToExtract.Split(Path.DirectorySeparatorChar)[0];
            };

            var className = extractClassName(name);

            if (className == null)
            {
                return null;
            }

            var assembly = AssemblyExtensions.currentTestAssembly();

            if (assembly == null)
            {
                return null;
            }

            var testCaseClass = assembly.GetType(className);

            return testCaseClass;
        }
    }
}