using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random random = new Random();

    static void Main()
    {
        var connection = new SqliteConnection("Data Source=quiz.db");
        connection.Open();

        var sql = File.ReadAllText("intrebari.sql");

        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();

        var selectAll = connection.CreateCommand();
        selectAll.CommandText = "SELECT RaspunsAfisat, VarianteIntrebari, IntrebareCorecta, Punctaj FROM Intrebari WHERE Categorie = 'Biologie'";

        var toate = new List<(string raspuns, string variante, string corect, int punctaj)>();

        using (var reader = selectAll.ExecuteReader())
        {
            while (reader.Read())
            {
                toate.Add((
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                ));
            }
        }

        var afisate = toate.OrderBy(x => random.Next()).Take(25).ToList();
        var ramase = toate.Except(afisate).ToList();

        int scor = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== JEOPARDY ===\n");

            for (int i = 0; i < afisate.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ({afisate[i].punctaj}) {afisate[i].raspuns}");
            }

            Console.WriteLine("\nAlege o intrebare (numar) sau 0 pentru exit:");
            var input = Console.ReadLine();

            if (input == "0")
                break;

            int index;

            if (!int.TryParse(input, out index) || index < 1 || index > afisate.Count)
            {
                Console.WriteLine("Input invalid!");
                Console.ReadKey();
                continue;
            }

            var intrebare = afisate[index - 1];

            var variante = intrebare.variante.Split(';').ToList();
            variante = variante.OrderBy(x => random.Next()).ToList();

            Console.Clear();
            Console.WriteLine("Raspuns:");
            Console.WriteLine(intrebare.raspuns);
            Console.WriteLine();

            for (int i = 0; i < variante.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {variante[i]}");
            }

            Console.Write("\nAlegerea ta: ");
            var rasp = Console.ReadLine();

            int alegere;

            if (int.TryParse(rasp, out alegere) && alegere >= 1 && alegere <= variante.Count)
            {
                var selectata = variante[alegere - 1];

                if (selectata.Trim() == intrebare.corect.Trim())
                {
                    Console.WriteLine("Corect!");
                    scor += intrebare.punctaj;
                }
                else
                {
                    Console.WriteLine("Gresit!");
                    Console.WriteLine("Corect era: " + intrebare.corect);
                }
            }
            else
            {
                Console.WriteLine("Input invalid!");
            }

            afisate.RemoveAt(index - 1);

            if (ramase.Count > 0)
            {
                var noua = ramase.OrderBy(x => random.Next()).First();
                afisate.Add(noua);
                ramase.Remove(noua);
            }

            Console.WriteLine("\nScor curent: " + scor);
            Console.WriteLine("Apasa orice tasta...");
            Console.ReadKey();
        }

        Console.WriteLine("\n=== FINAL ===");
        Console.WriteLine("Scor final: " + scor);

        Console.Write("\nIntrodu numele tau: ");
        var nume = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nume))
            nume = "Anonim";

        var save = connection.CreateCommand();
        save.CommandText = "INSERT INTO Scoruri (Nume, Puncte) VALUES (@n, @p)";
        save.Parameters.AddWithValue("@n", nume);
        save.Parameters.AddWithValue("@p", scor);
        save.ExecuteNonQuery();

        var top = connection.CreateCommand();
        top.CommandText = "SELECT Nume, Puncte FROM Scoruri ORDER BY Puncte DESC LIMIT 5";

        using (var r = top.ExecuteReader())
        {
            Console.WriteLine("\n=== LEADERBOARD ===\n");

            int poz = 1;

            while (r.Read())
            {
                Console.WriteLine($"{poz}. {r.GetString(0)} - {r.GetInt32(1)}");
                poz++;
            }
        }

        connection.Close();
    }
}