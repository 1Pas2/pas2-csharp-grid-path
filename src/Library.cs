namespace Pas2.GridPath;

public readonly record struct Cell(int Row, int Column);

public static class GridPath
{
    public static IReadOnlyList<Cell> Find(bool[,] blocked, Cell start, Cell end)
    {
        ArgumentNullException.ThrowIfNull(blocked);
        int rows = blocked.GetLength(0),
            columns = blocked.GetLength(1);
        bool Inside(Cell c) => c.Row >= 0 && c.Row < rows && c.Column >= 0 && c.Column < columns;
        if (!Inside(start) || !Inside(end))
            throw new ArgumentOutOfRangeException(nameof(start));
        if (blocked[start.Row, start.Column] || blocked[end.Row, end.Column])
            return Array.Empty<Cell>();
        var previous = new Dictionary<Cell, Cell>();
        var queue = new Queue<Cell>();
        previous[start] = start;
        queue.Enqueue(start);
        (int R, int C)[] moves = [(-1, 0), (0, 1), (1, 0), (0, -1)];
        while (queue.TryDequeue(out Cell cell))
        {
            if (cell == end)
            {
                var path = new List<Cell>();
                for (var p = end; ; p = previous[p])
                {
                    path.Add(p);
                    if (p == start)
                        break;
                }
                path.Reverse();
                return path;
            }
            foreach (var move in moves)
            {
                var next = new Cell(cell.Row + move.R, cell.Column + move.C);
                if (Inside(next) && !blocked[next.Row, next.Column] && previous.TryAdd(next, cell))
                    queue.Enqueue(next);
            }
        }
        return Array.Empty<Cell>();
    }
}
