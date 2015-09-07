using System.Collections.Generic;
using System.ComponentModel.Composition;
using ClassLibrary1.Implementions.Interfaces;

namespace ClassLibrary1
{
    public class Notifier : ComposeBase
    {
        [ImportMany]
        public IEnumerable<IMessageSender> Senders { get; protected set; }

        public void Notify(string message)
        {
            foreach (var sender in Senders)
            {
                sender.Send(message);
            }
        }
    }
}