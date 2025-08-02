using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Provides static methods to manage and retrieve monkey data.
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> monkeys = new()
    {
        new Monkey { Name = "Capuchin", Location = "Central & South America", Population = 100000, Description = "Intelligent and social monkeys often seen in movies." },
        new Monkey { Name = "Mandrill", Location = "Central Africa", Population = 20000, Description = "Known for their colorful faces and large size." },
        new Monkey { Name = "Golden Lion Tamarin", Location = "Brazil", Population = 3500, Description = "Small, golden-orange monkeys native to the Atlantic coastal forests." },
        new Monkey { Name = "Japanese Macaque", Location = "Japan", Population = 114431, Description = "Also called snow monkeys, famous for bathing in hot springs." }
    };

    private static int randomPickCount = 0;

    /// <summary>
    /// Gets all available monkeys.
    /// </summary>
    public static IReadOnlyList<Monkey> GetMonkeys() => monkeys;

    /// <summary>
    /// Gets a monkey by its name.
    /// </summary>
    /// <param name="name">The name of the monkey.</param>
    /// <returns>The monkey with the specified name, or null if not found.</returns>
    public static Monkey? GetMonkeyByName(string name) =>
        monkeys.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Gets a random monkey and increments the random pick count.
    /// </summary>
    /// <returns>A randomly selected monkey.</returns>
    public static Monkey GetRandomMonkey()
    {
        var random = new Random();
        randomPickCount++;
        return monkeys[random.Next(monkeys.Count)];
    }

    /// <summary>
    /// Gets the number of times a random monkey has been picked.
    /// </summary>
    public static int GetRandomPickCount() => randomPickCount;
}
