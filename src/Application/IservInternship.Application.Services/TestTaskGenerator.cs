using System.Collections;
using System.Text.Json;

namespace IservInternship.Application.Services;

public static class TestTaskGenerator
{
    private const int LEVEL_COUNT = 8;

    private static readonly Tree _tree = Tree.GenerateRandomTree(
        minChildren: 1, maxChildren: 2,
        LEVEL_COUNT
        );

    /// <summary>
    /// Генерирует задание со случайными данными и решение.
    /// Задание:
    /// "
    /// Требуется вывести Короткий UID и уровень всех родительских игроков для
    /// игрока с ID = ..., с сортировкой по уровню, и у которых класс Маг или Лучник.
    /// В ответ запишите Короткий UID второго по уровню игрока.
    /// "
    /// </summary>
    /// <returns></returns>
    public static TestTask GenerateTestTask()
    {
        var randomIds = Enumerable.Range(1, _tree.Count()).ToArray();
        Random.Shared.Shuffle(randomIds);

        int index = 0;
        foreach (var node in _tree)
        {
            node.Value = randomIds[index];
            index++;
        }

        var players = _tree
            .Select(n => new Player
            {
                Id = n.Value,
                ShortUid = Guid.NewGuid().ToString("N")[..8],
                Level = Random.Shared.Next(1, 200),
                Class = Player.Classes[Random.Shared.Next(Player.Classes.Length)],
                ReferrerId = n.Parent?.Value ?? null
            })
            .OrderBy(p => p.Id) // Сортируем по Id для предсказуемости
            .ToArray();

        var lastTreeLevelNodes = _tree.Where(n => n.Level == LEVEL_COUNT).ToArray();
        var targetNode = lastTreeLevelNodes[Random.Shared.Next(lastTreeLevelNodes.Length)];

        var ddl = @"CREATE TABLE dbo.Players -- Игроки
(
    [LINK]          [int] IDENTITY NOT NULL PRIMARY KEY,   -- Ид
    [C_ShortUid]    [varchar](8)  NOT NULL,                -- Короткий UID игрока
    [C_Level]       [int] NOT NULL,                        -- Уровень игрока
    [C_Class]       [varchar](16) NOT NULL,                -- Класс игрока (Воин, Маг, Вор)
    [C_ReferrerId]  [int] NULL,                            -- Ид игрока по чей реферальной ссылке пришел игрок
)";

        var insert = string.Join(Environment.NewLine, players.Select(p =>
            $"INSERT dbo.Players SELECT '{p.ShortUid}', {p.Level}, '{p.Class}', {(p.ReferrerId is null ? "NULL" : p.ReferrerId.ToString())}"
        ));

        var description = $@"Дана таблица dbo.Players. В ней содержится информация об игроках в MMO игре, в которой реализована реферальная система.
Требуется вывести короткий UID и уровень, всех игроков перешедших по реферальной ссылке в цепочке от самого первого игрока, до игрока с ShortUid = {players.First(p => p.Id == targetNode.Value).ShortUid}, с сортировкой по уровню, и у которых класс Маг или Вор.
В ответ запишите Короткий UID второго по уровню игрока.";

        var refferPlayers = new List<Player>();
        var currentNode = targetNode.Parent;

        while (currentNode is not null)
        {
            refferPlayers.Add(players.First(p => p.Id == currentNode.Value));
            currentNode = currentNode.Parent;
        }

        var answer = refferPlayers
            .Where(p => p.Class is "Маг" or "Вор")
            .OrderBy(p => p.Level)
            .Skip(1) // Пропускаем первого по уровню
            .Select(p => p.ShortUid)
            .FirstOrDefault() ?? string.Empty;

        return new TestTask
        {
            TestTaskObject = new TestTaskDto
            {
                Ddl = ddl,
                Insert = insert,
                Description = description
            },
            Answer = answer
        };
    }
}

public class TestTaskDto
{
    public string Ddl { get; set; } = string.Empty;

    public string Insert { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static TestTaskDto FromJson(string json)
    {
        return JsonSerializer.Deserialize<TestTaskDto>(json, CachedJsonSerializerOptions)
            ?? throw new InvalidOperationException("Не удалось десериализовать TestTaskDto из JSON.");
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, CachedJsonSerializerOptions);
    }
}

public class TestTask
{
    public TestTaskDto TestTaskObject { get; set; } = null!;

    public string Answer { get; set; } = string.Empty;
}

public class Player
{
    public int Id { get; set; }

    public required string ShortUid { get; set; }

    public int Level { get; set; }

    public required string Class { get; set; }

    public int? ReferrerId { get; set; }

    public static string[] Classes
    {
        get
        {
            return ["Воин", "Маг", "Вор"];
        }
    }
}

public class Tree : IEnumerable<Node>
{
    private readonly Node _root;

    private Tree(Node root)
    {
        _root = root;
    }

    public static Tree GenerateRandomTree(
            int minChildren, int maxChildren,
            int levelCount)
    {
        if (minChildren < 0 || maxChildren < minChildren)
            throw new ArgumentException("Некорректные значения для количества потомков.");
        if (levelCount < 1)
            throw new ArgumentException("Некорректное значение для уровней.");
        
        var queue = new Queue<Node>();

        var root = new Node(null, 1);
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Level >= levelCount)
                continue;

            int childCount = Random.Shared.Next(minChildren, maxChildren + 1);
            for (int i = 0; i < childCount; i++)
            {
                var child = new Node(current, current.Level + 1);
                current.AddChild(child);
                queue.Enqueue(child);
            }
        }

        return new Tree(root);
    }

    public IEnumerator<Node> GetEnumerator()
    {
        return Traverse(_root).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static IEnumerable<Node> Traverse(Node node)
    {
        var queue = new Queue<Node>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            yield return current;

            foreach (var child in current.Children)
                queue.Enqueue(child);
        }
    }
}

public sealed class Node
{
    public int Value { get; set; }

    public Node? Parent { get; set; }

    public List<Node> Children { get; set; } = [];

    public int Level { get; private set; }

    public Node(Node? parent, int level)
    {
        Parent = parent;
        Level = level;
    }

    public void AddChild(Node child)
    {
        Children.Add(child);
    }
}
