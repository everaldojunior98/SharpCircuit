using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	public class Jfet : Mosfet {

		public const double JFET_DEFAULT_THRESHOLD = -4;
		public const double JFET_BETA = 0.00125;

		public Jfet(bool pnpflag) : base(pnpflag) {

		}

		// These values are taken from Hayes+Horowitz p155
		public override double getDefaultThreshold() {
			return JFET_DEFAULT_THRESHOLD;
		}

		public override double getBeta() {
			return JFET_BETA;
		}

		/*public override void getInfo(String[] arr) {
			getFetInfo(arr, "JFET");
		}*/
	}

	public class NJfetElm : Jfet {

		public NJfetElm() : base(false) {

		}

	}

	public class PJfetElm : Jfet {

		public PJfetElm() : base(false) {

		}

	}
}