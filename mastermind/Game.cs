using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace mastermind {


	public class Game {
		private static Game Instance = null;
		protected Game() { }

		public static Game getInstance() {
			if (Instance == null) Instance = new Game();
			return Instance;
		}

		public List<int> secret;
		private List<List<int>> questions;
		private List<List<int>> answers;
		protected private int colors;
		private bool is_initialized = false;
		private int current;
		private int turns_left;

		private readonly Config config = new Config();
		readonly Generator generator = new Generator();

		public void launch() {
			if (is_initialized) return;
			secret = new List<int>(config.MaxColors);                    //TODO pobieranie układu z SecretGenerator.cs
			questions = new List<List<int>>(config.MaxTurns);
			answers = new List<List<int>>(config.MaxTurns);
			for (int i = 0; i < config.MaxTurns; i++) {               //TODO X=Config.MaxTurns Y=Config.MaxColors
				questions.Add(new List<int>(config.MaxColors));
				answers.Add(new List<int>(config.MaxColors));
			}
			is_initialized = true;
		}

		public void start() {
			current = 0;
			colors = config.MaxColors;
			turns_left = config.MaxTurns;
			secret.Clear();

			for (int i = 0; i < config.MaxTurns; i++) {
				questions[i].Clear();
				answers[i].Clear();
				for (int j = 0; j < 4; j++) {
					questions[j].Add(0);
					answers[i].Add(0);
				}

			}
			generator.generateSecret();
		}
		private List<int> getLastQuestion() {
			return questions.Last(x => x.All(y => y != 0));
		}

		public bool isWin() {
			return getLastQuestion().SequenceEqual(secret);
		}

		public bool isLose() {
			return getCurrentTurns() == 0 ? true : false;
		}

		public List<int> getLastAnswer() {
			return answers.Last();
		}

		private void generateAnswer() {
			var last_question = getLastQuestion();
			List<int> answer = new List<int>(4)
			{
				0,
				0,
				0,
				0
			};

			for (int i = 0; i < 4; i++) {
				if (last_question[i] == secret[i]) answer[i] = 2;
			}

			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					if (answer[i] != 2 && last_question[i] == secret[j] && i != j) answer[i] = 1;
				}
			}

			if (answer.Count() != 4) throw new Exception("Błąd! Odpowiedź różna od 4 !");
			answers.Add(answer);
			turns_left--;
		}

		public void addQuestion(string question) {
			if (question == null) throw new Exception("Puste zapytanie !");
			if (question.Length != 4) throw new Exception("Zapytanie musi mieć dokładnie 4 liczby!");
			var is_digits_only = question.All(x => x >= '1' && x <= '8');
			if (!is_digits_only) throw new Exception("Zapytanie może zawierać tylko liczby z zakresu 1-8!");

			//questions.Add(question.Cast<int>().ToList()); // mozliwe uzycie arraylist
			List<int> i_question = new List<int>(4);
			foreach (char sign in question) i_question.Add((int)sign - 48); // 49 to '1'
			questions.Add(i_question);
			current++;

			generateAnswer();
		}

		public int getCurrentTurns() {
			return this.turns_left;
		}

		public static void updateTurns(int turns) {
			int old_top = Console.CursorTop;
			int old_left = Console.CursorLeft;

			Console.CursorTop = 0;
			Console.CursorLeft = Console.WindowWidth - 3;
			Console.Write(" ");
			Console.Write(" ");

			Console.CursorLeft = Console.WindowWidth - 3;
			Console.Write(turns);

			Console.CursorTop = old_top;
			Console.CursorLeft = old_left;
		}

		public void printQuestions() {
			Debug.WriteLine(questions.Count());
		}
	}
}




