using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Mastermind";
                Console.WriteLine("Gra Mastermind\nSzare gwiazdki - brak\nŻółte gwiazdki - złe miejsce\nZielone gwiazdki - poprawne miejsce");
                Console.ReadKey();
                Console.Clear();

                Game game = Game.GetInstance();
                game.Launch();
                game.Start();
                Console.CursorLeft = Console.WindowWidth - 10; // RUCHY:_XY_
                Console.Write("Ruchy: " + game.GetCurrentTurns());
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                StringBuilder question = new StringBuilder();
                while (true) // główna pętla gry
                {
                    game.SecretDebug();
                    question.Clear();
                    for (int i = 0; i < 4; i++) // zbieranie kolorów do pytania
                    {
                        ConsoleKeyInfo c = Console.ReadKey(true);
                        while (c.KeyChar < '1' || c.KeyChar > '8')
                        {
                            if (Console.CursorLeft > 0)
                            {
                                Console.Write(" ");
                                Console.CursorLeft--;
                            }
                            c = Console.ReadKey(true);
                        }
                        Console.Write(c.KeyChar);
                        question.Append(c.KeyChar);
                        Console.CursorLeft += 2;
                    }
                    Console.Write("|");
                    Console.CursorLeft += 2;
                    try
                    {
                        game.AddQuestion(question.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    List<int> answer = game.GetLastAnswer();
                    for (int i = 0; i < 4; i++) // wydrukowanie odpowiedzi
                    {
                        Console.ResetColor();
                        switch (answer[i])
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write('*');
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write('*');
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write('*');
                                break;
                        }
                        Console.CursorLeft += 2;
                    }

                    Console.ResetColor();
                    Console.CursorLeft = 0;
                    Console.CursorTop++;
                    if (game.IsWin())
                    {
                        MessageBox.Show("Odgadłeś układ!", "Zwycięstwo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear();
                        Console.CursorTop = 0;
                        Console.CursorLeft = 0;
                        game.Start();
                        Console.CursorLeft = Console.WindowWidth - 10; // RUCHY:_XY_
                        Console.Write("Ruchy: " + game.GetCurrentTurns());
                        Console.CursorLeft = 0;
                        Console.CursorTop = 0;
                        continue;
                    }
                    if (game.IsLose())
                    {
                        MessageBox.Show("Koniec gry! Przegrałeś!", "Przegrana!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear();
                        Console.CursorTop = 0;
                        Console.CursorLeft = 0;
                        game.Start();
                        Console.CursorLeft = Console.WindowWidth - 10; // RUCHY:_XY_
                        Console.Write("Ruchy: " + game.GetCurrentTurns());
                        Console.CursorLeft = 0;
                        Console.CursorTop = 0;
                        continue;
                    }
                    Game.UpdateTurns(game.GetCurrentTurns());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                MessageBox.Show(e.Message, "Wyjątek!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}