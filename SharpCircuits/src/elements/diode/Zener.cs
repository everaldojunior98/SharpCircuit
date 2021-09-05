using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	// Zener code contributed by J. Mike Rollins
	// http://www.camotruck.net/rollins/simulator.html
	public class Zener : Diode {

		public const double ZENER_DEFAULT_ZVOLTAGE = 5.6;

		public Zener() : base() {
			diode.leakage = 5e-6; // 1N4004 is 5.0 uAmp
			zvoltage = ZENER_DEFAULT_ZVOLTAGE;
			setup();
		}

		/*public override void getInfo(String[] arr) {
			base.getInfo(arr);
			arr[0] = "Zener diode";
			arr[5] = "Vz = " + getVoltageText(zvoltage);
		}*/

	}
}