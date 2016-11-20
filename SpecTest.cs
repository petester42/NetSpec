
using System.Diagnostics;

namespace Tests
{
    public class SpecTest : NetSpec.NetSpec
    {
        string test = "test";

        public override void Spec()
        {
            beforeSuite(() =>
            {
                test = "one";
            });

            describe("is like a tests", () =>
            {
                it("is a test a1", () =>
                {
                    Debug.WriteLine("test a1");
                    Debug.WriteLine($"{test}");
                });

                it("is a test a2", () =>
                {
                    Debug.WriteLine("test a2");
                    Debug.WriteLine($"{test}");
                });
            });

            it("is like a test b", () =>
            {
                Debug.WriteLine("test b1");
                Debug.WriteLine($"{test}");
            });
        }
    }
}