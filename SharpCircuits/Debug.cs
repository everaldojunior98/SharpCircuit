using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpCircuit
{
	public static class Debug
	{
		[Conditional("DEBUG")]
		public static void Log(params object[] objs)
		{
			StringBuilder sb = new StringBuilder();
			foreach (object o in objs)
				sb.Append(o.ToString()).Append(" ");
			Console.WriteLine(sb.ToString());
		}

		[Conditional("DEBUG")]
		public static void LogF(string format, params object[] objs)
		{
			Console.WriteLine(string.Format(format, objs));
		}

	}
}
