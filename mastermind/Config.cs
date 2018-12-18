using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind {
	public sealed class Config {
		public int MaxTurns { get; set; } = 12;
		public int MaxColors { get; set; } = 4;
	}
}