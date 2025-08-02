
using System;

namespace MyMonkeyApp;

/// <summary>
/// Entry point for the Monkey management console application.
/// </summary>
public class Program
{
    private static readonly string[] asciiArts = new[]
    {
        "  (o\\_/o)\n (='.'=)  \n (\")_(\")  Monkey Time!",
        "   w  c( .. )o   (\n    \\__(-)    __)\n        /     (\n       (      )\n        \"\"\"\"\"",
        "   .-\"\"\"-.\n  / .===. \\ \n  \\/ 6 6 \\ \n  ( \\___/ )\n___ooo__ooo___",
        "   (o.o)\n   (   )\n  (___)\\\n  Monkey business!",
    };

    /// <summary>
    /// Main entry point.
    /// </summary>
    public static void Main()
    {
        var exitRequested = false;
        var random = new Random();
        while (!exitRequested)
        {
            Console.Clear();
            ShowRandomAsciiArt(random);
            Console.WriteLine("==== Monkey Management App ====");
            Console.WriteLine("1. 모든 원숭이 나열");
            Console.WriteLine("2. 이름으로 특정 원숭이의 세부 정보 가져오기");
            Console.WriteLine("3. 무작위 원숭이 가져오기");
            Console.WriteLine("4. 앱 종료");
            Console.Write("원하는 옵션을 선택하세요 (1-4): ");

            var userInput = Console.ReadLine();
            Console.WriteLine();
            switch (userInput)
            {
                case "1":
                    ListAllMonkeys();
                    break;
                case "2":
                    GetMonkeyByName();
                    break;
                case "3":
                    GetRandomMonkey();
                    break;
                case "4":
                    exitRequested = true;
                    Console.WriteLine("앱을 종료합니다. 안녕히 가세요!");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 1~4 중에서 선택하세요.");
                    break;
            }
            if (!exitRequested)
            {
                Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                Console.ReadKey();
            }
        }
    }

    private static void ShowRandomAsciiArt(Random random)
    {
        var art = asciiArts[random.Next(asciiArts.Length)];
        Console.WriteLine(art);
        Console.WriteLine();
    }

    private static void ListAllMonkeys()
    {
        var monkeys = MonkeyHelper.GetMonkeys();
        Console.WriteLine($"총 {monkeys.Count}종의 원숭이가 있습니다:\n");
        foreach (var monkey in monkeys)
        {
            Console.WriteLine($"- {monkey.Name} ({monkey.Location}) - 개체수: {monkey.Population}");
        }
    }

    private static void GetMonkeyByName()
    {
        Console.Write("원숭이 이름을 입력하세요: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("이름을 입력해야 합니다.");
            return;
        }
        var monkey = MonkeyHelper.GetMonkeyByName(name);
        if (monkey is null)
        {
            Console.WriteLine($"'{name}' 이름의 원숭이를 찾을 수 없습니다.");
        }
        else
        {
            Console.WriteLine($"\n이름: {monkey.Name}\n서식지: {monkey.Location}\n개체수: {monkey.Population}\n설명: {monkey.Description}");
        }
    }

    private static void GetRandomMonkey()
    {
        var monkey = MonkeyHelper.GetRandomMonkey();
        Console.WriteLine($"무작위로 선택된 원숭이: {monkey.Name}");
        Console.WriteLine($"서식지: {monkey.Location}");
        Console.WriteLine($"개체수: {monkey.Population}");
        Console.WriteLine($"설명: {monkey.Description}");
        Console.WriteLine($"(무작위 선택 횟수: {MonkeyHelper.GetRandomPickCount()})");
    }
}
