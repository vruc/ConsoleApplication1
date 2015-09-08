using System.ComponentModel.Composition;
using SimpleCalculator3.Implementions.Interfaces;

namespace ExtendedOperations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '/')]
    public class Divide : IOperation
    {
        public int Operate(int left, int right)
        {
            return left / right;
        }
    }
}