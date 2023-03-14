namespace SharpCircuit
{

    public class Inductor : CircuitElement
    {

        public Circuit.Lead leadIn
        {
            get { return lead0; }
        }

        public Circuit.Lead leadOut
        {
            get { return lead1; }
        }

        public double inductance { get; private set; }
        public bool isTrapezoidal { get; private set; }

        private InductorBase inductor;

        public Inductor(double induc, bool trapezoidal) : base()
        {
            inductance = induc;
            isTrapezoidal = trapezoidal;

            inductor = new InductorBase(induc, 0, isTrapezoidal);
        }

        public override void reset()
        {
            inductor.reset();
            current = lead_volt[0] = lead_volt[1] = 0;
        }

        public override void stamp(Circuit sim)
        {
            inductor.stamp(sim, lead_node[0], lead_node[1]);
        }

        public override void beginStep(Circuit sim)
        {
            var voltdiff = lead_volt[0] - lead_volt[1];
            inductor.beginStep(sim, voltdiff);
        }

        public override bool nonLinear()
        {
            return true;
        }

        public override void calculateCurrent()
        {
            var voltdiff = lead_volt[0] - lead_volt[1];
            current = inductor.calculateCurrent(voltdiff);
        }

        public override void step(Circuit sim)
        {
            inductor.doStep(sim);
        }
    }
}