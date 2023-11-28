using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfraStructure
{
    public class Calculator : IRekenMachine
    {
        private readonly ICounter _counter;

        public Calculator(ICounter counter)
        {
            _counter = counter;
        }

        public void DoeIets()
        {
            _counter.Increment();
            _counter.Show();
        }
    }

    public interface IRekenMachine
    {
        void DoeIets();
    }
}