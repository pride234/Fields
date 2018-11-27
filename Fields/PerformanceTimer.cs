using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fields 
{
	public class PerformanceTimer : IDisposable 
	{
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public Stopwatch _stopwatch;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public PerformanceTimer(){
			_stopwatch = new Stopwatch();
			_stopwatch.Start(); 
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public void Dispose() {
			Console.WriteLine(_stopwatch.ElapsedTicks);
			_stopwatch.Stop();
		}
	}
}
