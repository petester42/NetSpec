using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(type => type.GetTypeInfo().IsSubclassOf(typeof(NetSpec.NetSpec))).ToList();

            types.ForEach(type =>
            {
                var spec = Activator.CreateInstance(type) as NetSpec.NetSpec;
                
                spec.Run();
            });
        }
    }
}
