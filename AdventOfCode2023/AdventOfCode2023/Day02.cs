namespace AdventOfCode2023;

using Hand = (int r, int g, int b);
using Game = (int id, (int r, int g, int b)[] hands);

public static class Day02
{
    private static Game ParseGame(string line)
    {
        (string head, string sets) = SplitFirst(line, ":");
        Hand[] hands = sets.Split(';').Select(ParseHand).ToArray();

        int id = int.Parse(string.Concat(head.Skip("Game ".Length)));

        return (id, hands);
    }

    private static Hand ParseHand(string hand)
    {
        int r = 0, g = 0, b = 0;
        IEnumerable<string> fingers = hand.Split(',').Select(f => f.Trim());
        foreach (string finger in fingers)
        {
            (string num, string colour) = SplitFirst(finger, " ");
            switch (colour)
            {
                case "red":
                    r = int.Parse(num);
                    break;
                case "green":
                    g = int.Parse(num);
                    break;
                case "blue":
                    b = int.Parse(num);
                    break;
            }
        }

        return (r, g, b);
    }

    private static (string, string) SplitFirst(string str, string delimiter)
    {
        int i = str.IndexOf(delimiter, StringComparison.Ordinal);
        return i == -1 ? (str, "") : (str[..i], str[(i + 1)..]);
    }

    public static string SolvePart1(string input)
    {
        const int reds = 12, greens = 13, blues = 14;

        IEnumerable<Game> valids = input.Split('\n').Select(ParseGame).Where(IsValid);

        return valids.Select(g => g.id).Sum().ToString();

        bool IsValid(Game g)
        {
            return g.hands.All(h => h is
            {
                r: <= reds,
                g: <= greens,
                b: <= blues
            });
        }
    }

    public static string SolvePart2(string input)
    {
        IEnumerable<int> valids = input.Split('\n').Select(ParseGame).Select(Power);

        return valids.Sum().ToString();

        int Power(Game game)
        {
            int maxR = 0, maxG = 0, maxB = 0;
            foreach ((int r, int g, int b) in game.hands)
            {
                if (r > maxR) maxR = r;
                if (g > maxG) maxG = g;
                if (b > maxB) maxB = b;
            }

            return maxR * maxG * maxB;
        }
    }
}