using static SharpCircuit.Circuit;

namespace SharpCircuit.elements
{
    // based on https://github.com/sharpie7/circuitjs1/blob/master/src/com/lushprojects/circuitjs1/client/DCMotorElm.java

    public class DCMotor : CircuitElement
    {
        public Lead leadIn => lead0;

        public Lead leadOut => lead1;

        public double angle { get; private set; }
        public double speed { get; private set; }

        private InductorBase ind;
        private InductorBase indInertia;

        // Electrical parameters
        private double resistance;
        private double inductance;

        // Electro-mechanical parameters
        private double K;
        private double Kb;
        private double J;
        private double B;

        private double coilCurrent;
        private double inertiaCurrent;
        private readonly int[] voltSources;

        public DCMotor()
        {
            angle = pi / 2;
            speed = 0;

            inductance = 0.5;
            resistance = 1;
            K = 0.15;
            B = 0.05;
            J = 0.02;
            Kb = 0.15;

            voltSources = new int[2];

            ind = new InductorBase(inductance, 0, false);
            indInertia = new InductorBase(J, 0, false);

            allocLeads();
        }

        public DCMotor(double induc, double res, double k, double b, double j, double kb)
        {
            angle = pi / 2;
            speed = 0;

            inductance = induc;
            resistance = res;
            K = k;
            B = b;
            J = j;
            Kb = kb;

            voltSources = new int[2];

            ind = new InductorBase(inductance, 0, false);
            indInertia = new InductorBase(J, 0, false);

            allocLeads();
        }

        public double getAngle()
        {
            return angle;
        }

        public override int getInternalLeadCount()
        {
            return 4;
        }

        public override int getVoltageSourceCount()
        {
            return 2;
        }

        public override void setVoltageSource(int leadX, int voltSourceNdx)
        {
            voltSources[leadX] = voltSourceNdx;
        }

        public override void reset()
        {
            ind.reset();
            indInertia.reset();
            coilCurrent = 0;
            inertiaCurrent = 0;
        }

        public override void stamp(Circuit sim)
        {
            // stamp a bunch of internal parts to help us simulate the motor.  It would be better to simulate this mini-circuit in code to reduce
            // the size of the matrix.

            //nodes[0] nodes [1] are the external nodes
            //Electrical part:
            // inductor from motor nodes[0] to internal nodes[2]
            ind.stamp(sim, lead_node[0], lead_node[2]);
            // resistor from internal nodes[2] to internal nodes[3] // motor post 2
            sim.stampResistor(lead_node[2], lead_node[3], resistance);
            // Back emf voltage source from internal nodes[3] to external nodes [1]
            sim.stampVoltageSource(lead_node[3], lead_node[1], voltSources[0]); // 

            //Mechanical part:
            // inertia inductor from internal nodes[4] to internal nodes[5]
            indInertia.stamp(sim, lead_node[4], lead_node[5]);
            // resistor from  internal nodes[5] to  ground 
            sim.stampResistor(lead_node[5], 0, B);
            // Voltage Source from  internal nodes[4] to ground
            sim.stampVoltageSource(lead_node[4], 0, voltSources[1]);
        }

        public override void beginStep(Circuit sim)
        {
            ind.beginStep(sim, lead_volt[0] - lead_volt[2]);
            indInertia.beginStep(sim, lead_volt[4] - lead_volt[5]);

            angle += speed * sim.timeStep;
        }

        public override void step(Circuit sim)
        {
            sim.updateVoltageSource(lead_node[4], 0, voltSources[1], coilCurrent * K);
            sim.updateVoltageSource(lead_node[3], lead_node[1], voltSources[0], inertiaCurrent * Kb);
            ind.doStep(sim);
            indInertia.doStep(sim);
        }

        public override void calculateCurrent()
        {
            coilCurrent = ind.calculateCurrent(lead_volt[0] - lead_volt[2]);
            inertiaCurrent = indInertia.calculateCurrent(lead_volt[4] - lead_volt[5]);

            speed = inertiaCurrent;
        }

        public override void setCurrent(int voltSourceNdx, double c)
        {
            if (voltSourceNdx == voltSources[0])
                current = c;
        }
    }
}