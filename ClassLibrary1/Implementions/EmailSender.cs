using System;
using System.ComponentModel.Composition;
using ClassLibrary1.Implementions.Interfaces;

namespace ClassLibrary1.Implementions
{
    [Export(typeof(IMessageSender))]
    class EmailSender : IMessageSender
    {
        public void Send(string message)
        {
            Console.WriteLine("{0} : {1}", "EmailSender", message);
        }
    }
}
