using MyMonkeyApp.Helpers;
using MyMonkeyApp.Models;

namespace MyMonkeyApp;

/// <summary>
/// 원숭이 콘솔 애플리케이션의 진입점입니다.
/// </summary>
class Program
{
    /// <summary>
    /// 애플리케이션의 진입점입니다.
    /// </summary>
    /// <param name="args">명령줄 인수</param>
    static void Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DisplayWelcome();
            RunApplication();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n오류가 발생했습니다: {ex.Message}");
            Console.WriteLine("애플리케이션을 다시 시작해주세요.");
        }
        finally
        {
            Console.WriteLine("\n아무 키나 눌러서 종료하세요...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 환영 메시지와 ASCII 아트를 표시합니다.
    /// </summary>
    private static void DisplayWelcome()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    🐵 원숭이 정보 시스템 🐵                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(MonkeyHelper.GetLargeMonkeyAsciiArt());
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("원숭이들의 세계에 오신 것을 환영합니다!");
        Console.WriteLine($"현재 {MonkeyHelper.GetMonkeyCount()}종의 원숭이 정보를 제공합니다.\n");
        Console.ResetColor();
    }

    /// <summary>
    /// 애플리케이션의 메인 루프를 실행합니다.
    /// </summary>
    private static void RunApplication()
    {
        bool isRunning = true;

        while (isRunning)
        {
            try
            {
                DisplayMenu();
                string? input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("올바른 메뉴 번호를 입력해주세요.");
                    PauseForUser();
                    continue;
                }

                switch (input.Trim())
                {
                    case "1":
                        ListAllMonkeys();
                        break;
                    case "2":
                        SearchMonkeyByName();
                        break;
                    case "3":
                        ShowRandomMonkey();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("올바른 메뉴 번호를 입력해주세요 (1-4).");
                        break;
                }
                
                if (isRunning)
                {
                    PauseForUser();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"작업 중 오류가 발생했습니다: {ex.Message}");
                PauseForUser();
            }
        }
    }

    /// <summary>
    /// 메뉴를 표시합니다.
    /// </summary>
    private static void DisplayMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("══════════════════════════════════════");
        Console.WriteLine("           🐵 메인 메뉴 🐵");
        Console.WriteLine("══════════════════════════════════════");
        Console.ResetColor();
        
        Console.WriteLine("1. 모든 원숭이 목록 보기");
        Console.WriteLine("2. 이름으로 원숭이 검색");
        Console.WriteLine("3. 무작위 원숭이 선택");
        Console.WriteLine("4. 종료");
        Console.Write("\n선택하세요 (1-4): ");
    }

    /// <summary>
    /// 모든 원숭이의 목록을 표시합니다.
    /// </summary>
    private static void ListAllMonkeys()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🐵 모든 원숭이 목록");
        Console.WriteLine("════════════════════════════════════════════════════════════════");
        Console.ResetColor();

        var monkeys = MonkeyHelper.GetAllMonkeys();
        
        for (int i = 0; i < monkeys.Count; i++)
        {
            var monkey = monkeys[i];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{i + 1}. {monkey.Name}");
            Console.ResetColor();
            
            Console.WriteLine($"   서식지: {monkey.Location}");
            Console.WriteLine($"   개체수: {monkey.Population:N0}마리");
            Console.WriteLine($"   크기: {monkey.Size}cm, 무게: {monkey.Weight}kg");
            Console.WriteLine($"   설명: {monkey.Description}");
        }
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n총 {monkeys.Count}종의 원숭이가 있습니다.");
        Console.ResetColor();
    }

    /// <summary>
    /// 이름으로 원숭이를 검색합니다.
    /// </summary>
    private static void SearchMonkeyByName()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🔍 이름으로 원숭이 검색");
        Console.WriteLine("════════════════════════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.Write("검색할 원숭이 이름을 입력하세요: ");
        string? searchName = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(searchName))
        {
            Console.WriteLine("올바른 이름을 입력해주세요.");
            return;
        }

        try
        {
            var monkey = MonkeyHelper.GetMonkeyByName(searchName);
            
            if (monkey != null)
            {
                Console.WriteLine("\n✅ 원숭이를 찾았습니다!");
                DisplayMonkeyDetails(monkey);
            }
            else
            {
                Console.WriteLine($"\n❌ '{searchName}' 이름의 원숭이를 찾을 수 없습니다.");
                Console.WriteLine("\n💡 사용 가능한 원숭이 이름:");
                var allMonkeys = MonkeyHelper.GetAllMonkeys();
                foreach (var m in allMonkeys)
                {
                    Console.WriteLine($"   - {m.Name}");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"입력 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// 무작위로 선택된 원숭이를 표시합니다.
    /// </summary>
    private static void ShowRandomMonkey()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🎲 무작위 원숭이 선택");
        Console.WriteLine("════════════════════════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.WriteLine("랜덤 원숭이를 선택하고 있습니다...");
        Thread.Sleep(1000); // 약간의 서스펜스 효과
        
        var randomMonkey = MonkeyHelper.GetRandomMonkey();
        Console.WriteLine("\n🎉 선택된 원숭이:");
        DisplayMonkeyDetails(randomMonkey);
    }

    /// <summary>
    /// 원숭이의 상세 정보를 표시합니다.
    /// </summary>
    /// <param name="monkey">표시할 원숭이 객체</param>
    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        if (monkey == null)
        {
            Console.WriteLine("원숭이 정보를 표시할 수 없습니다.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(MonkeyHelper.GetMonkeyAsciiArt());
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"🐵 이름: {monkey.Name}");
        Console.ResetColor();
        Console.WriteLine($"🌍 서식지: {monkey.Location}");
        Console.WriteLine($"📊 개체수: {monkey.Population:N0}마리");
        Console.WriteLine($"📏 크기: {monkey.Size}cm");
        Console.WriteLine($"⚖️ 무게: {monkey.Weight}kg");
        Console.WriteLine($"📝 설명: {monkey.Description}");
    }

    /// <summary>
    /// 사용자 입력을 기다립니다.
    /// </summary>
    private static void PauseForUser()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n계속하려면 아무 키나 눌러주세요...");
        Console.ResetColor();
        Console.ReadKey();
    }
}
