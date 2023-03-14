namespace SharpCircuit
{
    public class InductorBase
    {
        private double inductance;
        private bool isTrapezoidal;

        private int[] nodes;
        private double compResistance;
        private double current;
        private double curSourceValue;

        public InductorBase(double induc, double c, bool trapezoidal)
        {
            nodes = new int[2];
            inductance = induc;
            current = c;
            isTrapezoidal = trapezoidal;
        }

        public void reset()
        {
            current = 0;
        }

        public void stamp(Circuit sim, int n0, int n1)
        {
            nodes[0] = n0;
            nodes[1] = n1;

            if (isTrapezoidal)
                compResistance = 2 * inductance / sim.timeStep;
            else
                compResistance = inductance / sim.timeStep;

            sim.stampResistor(nodes[0], nodes[1], compResistance);
            sim.stampRightSide(nodes[0]);
            sim.stampRightSide(nodes[1]);
        }

        public void beginStep(Circuit sim, double voltdiff)
        {
            if (isTrapezoidal)
                curSourceValue = voltdiff / compResistance + current;
            else
                curSourceValue = current;
        }

        public double calculateCurrent(double voltdiff)
        {
            if (compResistance > 0)
                current = voltdiff / compResistance + curSourceValue;
            return current;
        }

        public void doStep(Circuit sim)
        {
            sim.stampCurrentSource(nodes[0], nodes[1], curSourceValue);
        }
    }
}