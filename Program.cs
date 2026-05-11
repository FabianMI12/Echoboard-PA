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
        selectAll.CommandText = "SELECT RaspunsAfisat, VarianteIntrebari, IntrebareCorecta FROM Intrebari WHERE Categorie LIKE 'Biodiversitate%'";

        var toate = new List<(string raspuns, string variante, string corect)>();

        using (var reader = selectAll.ExecuteReader())
        {
            while (reader.Read())
            {
                toate.Add((
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2)
                ));
            }
        }

        var intrebari = toate.OrderBy(x => random.Next()).ToList();

        foreach (var intrebare in intrebari)
        {
            Console.Clear();

            Console.WriteLine("Raspuns:");
            Console.WriteLine(intrebare.raspuns);
            Console.WriteLine();

            var variante = intrebare.variante.Split(';').OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < variante.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {variante[i]}");
            }

            Console.Write("\nAlegerea ta: ");
            var rasp = Console.ReadLine();

            int alegere;

            if (int.TryParse(rasp, out alegere) && alegere >= 1 && alegere <= variante.Count)
            {
                if (variante[alegere - 1].Trim() == intrebare.corect.Trim())
                {
                    Console.WriteLine("Corect!");
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

            Console.WriteLine("\nApasa orice tasta...");
            Console.ReadKey();
        }

        Console.WriteLine("\nAi terminat toate intrebarile!");
        connection.Close();
    }
}