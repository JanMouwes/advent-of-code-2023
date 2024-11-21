namespace AdventOfCode2023;

public static class Day1
{
    public static string SolvePart1(string input)
    {
        string[] lines = input.Split('\n');
        IEnumerable<int> numbers = lines.Select(l =>
        {
            char[] lineNums = l.Where(char.IsDigit).ToArray();

            return int.Parse(lineNums[0] + lineNums[^1].ToString());
        });

        return numbers.Sum().ToString();
    }

    public static string SolvePart2(string input)
    {
        string[] lines = input.Split('\n');
        IEnumerable<int> numbers = lines.Select(l => int.Parse(FindFirst(l) + FindLast(l).ToString()));

        return numbers.Sum().ToString();
    }

    private static char FindFirst(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            char? maybe = MatchFromIndex(line, i);
            if (maybe is { } just)
            {
                return just;
            }
        }

        throw new ArgumentException("invalid line");
    }

    private static char? MatchFromIndex(string line, int from)
    {
        (string, char)[] literals =
        [
            ("one", '1'), ("two", '2'),
            ("three", '3'), ("four", '4'),
            ("five", '5'), ("six", '6'),
            ("seven", '7'), ("eight", '8'),
            ("nine", '9'), ("zero", '0')
        ];

        char c = line[from];
        if (char.IsDigit(c))
        {
            return c;
        }

        int charsLeft = line.Length - from;
        foreach ((string literal, char result) in literals)
        {
            if (literal.Length > charsLeft)
            {
                continue;
            }
            bool match = literal.Select((a, i) => (a, i))
                .All(tp => tp.a.Equals(line[from + tp.i]));
            if (match)
            {
                return result;
            }
        }

        return null;
    }

    private static char FindLast(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            char? maybe = MatchFromIndex(line, i);
            if (maybe is { } just)
            {
                return just;
            }
        }

        throw new ArgumentException("invalid line");
    }
}