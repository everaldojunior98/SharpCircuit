using System.Collections.Generic;

namespace SharpCircuit
{
    public class CircuitNode
    {
        public long id;
        public List<CircuitNodeLink> links;
        public bool isInternal;

        public CircuitNode()
        {
            links = new List<CircuitNodeLink>();
        }
    }

    public class CircuitNodeLink
    {
        public ICircuitElement elm;
        public int leadNdx;
    }
}