using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace ClassLibrary1
{
    public class ComposeBase
    {
        public ComposeBase()
        {
            Console.WriteLine("Invoking ComposeBase ctor");
            Compose();
        }

        protected void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}