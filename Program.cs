namespace ConsoleApplication
{
    public class TestSpec : NetSpec.NetSpec
    {
        public override void Spec()
        {
            it("is like", () =>
            {
                it("will do", () =>
                {
                    expect(() => "test").to(equal("test"));
                    expect(() => "test").to(equal("3"));
                });
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // NetSpec.World.Run();

            // var assemblies = assembly.GetReferencedAssemblies().Select(name => Assembly.Load(name)).ToList();

            // var x = new TestSpec();
            // x.Spec();
        }
    }
}
