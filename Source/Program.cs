using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication
{
    public class Program : IDisposable
    {
        public static int Main(string[] args)
        {
            using (var program = new Program())
            {
                return program.Run(args);
            }
        }

        public int Run(string[] args)
        {
            try
            {
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private void Tests()
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(type => type.GetTypeInfo().IsSubclassOf(typeof(NetSpec.NetSpec))).ToList();

            types.ForEach(type =>
            {
                var spec = Activator.CreateInstance(type) as NetSpec.NetSpec;

                spec.Run();
            });

        }

        public void Dispose()
        {

        }
    }
}
