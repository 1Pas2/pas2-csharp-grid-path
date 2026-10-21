using Pas2.GridPath;

static void Check(bool condition, string message)
{
    if (!condition)
        throw new Exception(message);
}
static void Fails(Action action)
{
    try
    {
        action();
    }
    catch (Exception)
    {
        return;
    }
    throw new Exception("Expected failure");
}
var grid = new bool[3, 3];
grid[1, 1] = true;
var path = GridPath.Find(grid, new(0, 0), new(2, 2));
Check(path.Count == 5 && path[0] == new Cell(0, 0) && path[^1] == new Cell(2, 2), "shortest");
var blocked = new bool[1, 3];
blocked[0, 1] = true;
Check(GridPath.Find(blocked, new(0, 0), new(0, 2)).Count == 0, "unreachable");
Check(GridPath.Find(grid, new(0, 0), new(0, 0)).Count == 1, "same");
Fails(() => GridPath.Find(grid, new(-1, 0), new(0, 0)));
Console.WriteLine("All checks passed");
