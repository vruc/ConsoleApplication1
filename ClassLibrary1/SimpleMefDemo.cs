using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace ClassLibrary1
{
    public class SimpleMefDemo
    {
        [Import]
        public IMessageSender MessageSender { get; set; }

        public void Run()
        {
            Compose();
            MessageSender.Send("Message Sent");
        }

        private void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}