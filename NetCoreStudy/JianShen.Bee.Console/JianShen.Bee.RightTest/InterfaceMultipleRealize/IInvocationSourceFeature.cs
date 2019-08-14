using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JianShen.Bee.RightTest.InterfaceMultipleRealize
{
    public interface IInvocationSourceFeature
    {
        string Source { get; }
    }

    public class InvocationSourceFeature : IInvocationSourceFeature
    {
        public string Source { get; }

        public InvocationSourceFeature(string source)
        {
            Source = source;
        }
    }   
}
