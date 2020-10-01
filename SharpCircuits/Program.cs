using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using ServiceStack.Text;

namespace SharpCircuit {
	
	class Program {

		public static double Round(double val, int places) {
			if(places < 0) throw new ArgumentException("places");
			return Math.Round(val - (0.5 / Math.Pow(10, places)), places);
		}

		static void Main(string[] args) {

			Circuit sim = new Circuit();

			var volt0 = sim.Create<VoltageInput>();
			var g = sim.Create<Ground>();
			var res0 = sim.Create<Resistor>();
			res0.resistance = 1;
			
			sim.Connect(volt0.leadPos, res0.leadIn);
			sim.Connect(g.leadIn, res0.leadOut);

			for(int x = 1; x <= 3; x++) {
				sim.doTick();
				// Ohm's Law
				Debug.Log(res0.getVoltageDelta(), res0.resistance * res0.getCurrent()); // V = I x R
				Debug.Log(res0.getCurrent(), res0.getVoltageDelta() / res0.resistance); // I = V / R
				Debug.Log(res0.resistance, res0.getVoltageDelta() / res0.getCurrent()); // R = V / I
				Debug.Log(res0.getLeadVoltage(0),res0.getLeadVoltage(0)); // Leads voltage
			}

			Console.WriteLine("program complete");
			Console.ReadLine();
		}

	}
}

public static class Debug {

	public static void Log(params object[] objs) {
		StringBuilder sb = new StringBuilder();
		foreach(object o in objs)
			sb.Append(o.ToString()).Append(" ");
		Console.WriteLine(sb.ToString());
	}

	public static void LogF(string format, params object[] objs) {
		Console.WriteLine(string.Format(format, objs));
	}

}