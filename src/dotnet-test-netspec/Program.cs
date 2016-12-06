using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication
{
    public class Program : IDisposable
    {
        public static int Main(string[] args)
        {
            for (var i = 0; i < args.Count(); ++i)
            {
                var arg = args[i];
                Console.WriteLine($"{i} : {arg}");
            }

            using (var program = new Program())
            {
                return program.Run(args);
            }
        }

        public int Run(string[] args)
        {
            try
            {
                Tests(args[0]);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private void Tests(string testdll)
        {
            Console.WriteLine("Tests");

            var assembyName = Path.GetFileNameWithoutExtension(testdll);
            
            var assembly = Assembly.Load(new AssemblyName(assembyName));

            var types = assembly.GetTypes().Where(type => type.GetTypeInfo().IsSubclassOf(typeof(NetSpec.NetSpec))).ToList();

            Console.WriteLine(types);

            types.ForEach(type =>
            {
                Console.WriteLine(type);

                var spec = Activator.CreateInstance(type) as NetSpec.NetSpec;

                spec.Run();
            });

        }

        public void Dispose()
        {

        }
    }
}
