using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	public class Jfet : Mosfet {
		
		public Jfet(bool pnpflag) : base(pnpflag) {

		}

		// These values are taken from Hayes+Horowitz p155
		public override double getDefaultThreshold() {
			return -4;
		}

		public override double getBeta() {
			return 0.00125;
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