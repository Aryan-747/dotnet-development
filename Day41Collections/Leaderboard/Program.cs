using System;
using System.Collections.Generic;
using System.Linq;

class Leaderboard
{
    public static List<(string name, int score)> GetTopK(
        List<(string name, int score)> players, int k)
    {
        return players
            .OrderByDescending(p => p.score)   // Sort by score descending
            .ThenBy(p => p.name)               // Tie-breaker: name ascending
            .Take(k)                           // Take top k
            .ToList();
    }

    static void Main()
    {
        var players = new List<(string, int)>
        {
            ("Raj", 80),
            ("Anu", 95),
            ("Vikram", 95),
            ("Meena", 70)
        };

        int k = 3;

        var topK = GetTopK(players, k);

        foreach (var player in topK)
        {
            Console.WriteLine($"{player.name} - {player.score}");
        }
    }
}
