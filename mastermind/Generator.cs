using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind {
	public static class Generator {

		private static readonly Random rand = new Random();

		public static void GenerateSecret(this Game g) {
			for (int i = 0; i < g.colors; i++) g.secret.Add(rand.Next(1, 6));
		}
	}
}

