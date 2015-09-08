using System.ComponentModel.Composition;
using SimpleCalculator3.Implementions.Interfaces;

namespace SimpleCalculator3.Implementions
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    class Subtract : IOperation
    {

        public int Operate(int left, int right)
        {
            return left - right;
        }

    }
}