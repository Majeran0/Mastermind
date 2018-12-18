using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind {
	internal class Generator : Game {

		private static readonly Random rand = new Random();

		public void GenerateSecret() {
			for (int i = 0; i < colors; i++) secret.Add(rand.Next(1, colors));
		}
	}
}

