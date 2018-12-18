using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mastermind {
	class Highscores : Game {
		private readonly string scores = File.ReadAllText(@"highscores.txt");
		private string score;

		public void DisplayScores() {
			Console.WriteLine(scores);
		}

		public void AddScore(string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException("Pusta nazwa", nameof(name));
			} else {
				score = $"{name} : {current} tur";
				File.AppendAllText(@"highscores.txt", score + Environment.NewLine);
			}
		}

		public void ResetScores() {
			File.WriteAllText(@"highscores.txt", string.Empty);
		}
	}
}
