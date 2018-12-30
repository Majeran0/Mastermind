using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace mastermind
{
    public sealed class Game
    {

        private static Game Instance = null;
        private Game() { }
        public static Game GetInstance()
        {
            if (Instance == null) Instance = new Game();
            return Instance;
        }

        public List<int> secret;
        private List<List<int>> questions;
        private List<List<int>> answers;
        public int colors;
        private bool is_initialized = false;
        public int current;
        private int turns_left;

        private readonly Config config = new Config();

        public void Launch()
        {
            if (is_initialized) return;
            secret = new List<int>(config.MaxColors);
            questions = new List<List<int>>(config.MaxTurns);
            answers = new List<List<int>>(config.MaxTurns);
            for (int i = 0; i < config.MaxTurns; i++)
            {
                questions.Add(new List<int>(config.MaxColors));
                answers.Add(new List<int>(config.MaxColors));
            }
            is_initialized = true;
        }

        public void Start()
        {
            current = 0;
            colors = config.MaxColors;
            turns_left = config.MaxTurns;
            secret.Clear();

            for (int i = 0; i < config.MaxTurns; i++)
            {
                questions[i].Clear();
                answers[i].Clear();
                for (int j = 0; j < config.MaxColors; j++)
                {
                    questions[j].Add(0);
                    answers[i].Add(0);
                }

            }
            this.GenerateSecret();
        }
        private List<int> GetLastQuestion()
        {
            return questions.Last(x => x.All(y => y != 0));
        }

        public bool IsWin()
        {
            return GetLastQuestion().SequenceEqual(secret);
        }

        public bool IsLose()
        {
            return GetCurrentTurns() == 0 ? true : false;
        }

        public List<int> GetLastAnswer()
        {
            return answers.Last();
        }

        private void GenerateAnswer()
        {
            var last_question = GetLastQuestion();
            List<int> answer = new List<int>()
            {
                0,
                0,
                0,
                0
            };

            for (int i = 0; i < config.MaxColors; i++)
            {
                if (last_question[i] == secret[i]) answer[i] = 2;
            }

            for (int i = 0; i < config.MaxColors; i++)
            {
                for (int j = 0; j < config.MaxColors; j++)
                {
                    if (answer[i] != 2 && last_question[i] == secret[j] && i != j && answer[j] != 2) answer[i] = 1;
                }
            }

            answer.Sort();
            answer.Reverse();

            if (answer.Count() != config.MaxColors) throw new Exception("Błąd! Odpowiedź różna od maxymalnej liczby znaków!");
            answers.Add(answer);
            turns_left--;
        }

        public void AddQuestion(string question)
        {
            if (question == null) throw new Exception("Puste zapytanie!");
            if (question.Length != config.MaxColors) throw new Exception("Zapytanie ma złą liczbę znaków!");
            var is_digits_only = question.All(x => x >= '1' && x <= '6');
            if (!is_digits_only) throw new Exception("Zapytanie może zawierać tylko liczby z zakresu 1-6!");


            List<int> i_question = new List<int>(config.MaxColors);
            foreach (char sign in question) i_question.Add((int)sign - 48); //Możliwe że -48 jest zbędne, Stack overflow podpowiada że zmieni to "1" na 49 
            questions.Add(i_question);
            current++;

            GenerateAnswer();
        }

        public int GetCurrentTurns()
        {
            return turns_left;
        }

        public static void UpdateTurns(int turns)
        {
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
       public void SecretDebug()
        {
            Debug.Flush();
            Debug.WriteLine(secret.Select(f => f.ToString()).Aggregate((x, y) => x + " " + y));
        }
    }
}