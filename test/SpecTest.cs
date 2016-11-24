using System.Collections.Generic;

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
                    expect(() => test).to(equal("test"));

                    var a = new List<string>
                    {
                        "a", "b"
                    };

                    var b = new List<string>
                    {
                        "C", "D"
                    };

                    expect(() => a).to(equal(b));

                });

                it("is a test a2", () =>
                {
                    expect(() => "test a2").to(equal("test a2"));

                    var a = new Dictionary<string, string>
                    {
                        {"a", "b"}
                    };

                    var b = new Dictionary<string, string>
                    {
                        {"C", "D"}
                    };

                    expect(() => a).to(equal(b));
                });
            });

            it("is like a test b", () =>
            {
                expect(() => "test b").to(equal("test b"));

                var a = new string[]
                    {
                        "a", "b"
                    };

                var b = new string[]
                    {
                        "c", "D"
                    };

                expect(() => a).to(equal(b));
            });
        }
    }
}