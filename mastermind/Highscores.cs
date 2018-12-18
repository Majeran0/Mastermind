using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mastermind {
	public static class Highscores {
		private static readonly string scores = File.ReadAllText(@"highscores.txt");
		private static string score;

		public static void DisplayScores() {
			Console.WriteLine(scores);
		}

		public static void AddScore(this Game g, string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException("Pusta nazwa", nameof(name));
			} else {
				score = $"{name} : {g.current} tur";
				File.AppendAllText(@"highscores.txt", score + Environment.NewLine);
			}
		}

		public static void ResetScores() {
			File.WriteAllText(@"highscores.txt", string.Empty);
		}
	}
}