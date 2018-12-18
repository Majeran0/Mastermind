using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mastermind {
	class Highscores : Game {
		private readonly string scores = System.IO.File.ReadAllText(@"highscores.txt");
		private string score;

		public void DisplayScores() {
			Console.WriteLine(scores);
		}

		public void AddScore(string name) {
			score = $"{name} : {current} tur";
			File.AppendAllText(@"highscores.txt", score + Environment.NewLine); 
		}

		public void ResetScores() {
			File.WriteAllText(@"highscores.txt", String.Empty);
		}
	}
}
