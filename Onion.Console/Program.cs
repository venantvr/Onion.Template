namespace Onion.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new BootStrapper()
                .SetExternalDataSources()
                .SetInternalStorage()
                .SetServiceBus()
                .Execute(d => d.Process());
        }
    }
}