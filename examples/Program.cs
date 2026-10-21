using Pas2.GridPath;

var grid = new bool[3, 3];
grid[1, 1] = true;
foreach (var cell in GridPath.Find(grid, new(0, 0), new(2, 2)))
    Console.WriteLine(cell);
