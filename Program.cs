Console.WriteLine("Analysing tree grid...");

var lines = File.ReadAllLines("tree_grid.txt");
var treeGrid = lines.Select(line =>
{
    var numbers = line.Select(c => (int) char.GetNumericValue(c)).ToList();
    return numbers;
}).ToList();

// Puzzle 1
var visibleTreeCount = 0;

for (var row = 0; row < treeGrid.Count; row++)
for (var col = 0; col < treeGrid[row].Count; col++)
{
    var treeHeight = treeGrid[row][col];
    var visibleFromLeft = !Enumerable.Range(0, col).Any(leftIndex => treeGrid[row][leftIndex] >= treeHeight);
    var visibleFromRight = !Enumerable.Range(col + 1, treeGrid[row].Count - col - 1).Any(rightIndex => treeGrid[row][rightIndex] >= treeHeight);
    var visibleFromTop = !Enumerable.Range(0, row).Any(topIndex => treeGrid[topIndex][col] >= treeHeight);
    var visibleFromBottom = !Enumerable.Range(row + 1, treeGrid.Count - row - 1).Any(bottomIndex => treeGrid[bottomIndex][col] >= treeHeight);

    if (visibleFromBottom || visibleFromLeft || visibleFromRight || visibleFromTop) visibleTreeCount++;
}

Console.WriteLine($"There are {visibleTreeCount} visible trees");

// Puzzle 2
var highestScenicScore = 0;

for (var row = 0; row < treeGrid.Count; row++)
for (var col = 0; col < treeGrid[row].Count; col++)
{
    if (row == 0 || col == 0 || row == treeGrid.Count - 1 || col == treeGrid[row].Count - 1) continue; // skip edges
    var treeHeight = treeGrid[row][col];

    var leftTrees = treeGrid[row].Skip(0).Take(col).ToList();
    var firstLeftBlockingTreeIdx = leftTrees.FindLastIndex(height => height >= treeHeight);
    var viewCountFromLeft = firstLeftBlockingTreeIdx == -1 ? leftTrees.Count : col - firstLeftBlockingTreeIdx;

    var rightTrees = treeGrid[row].Skip(col + 1).Take(treeGrid[row].Count - col - 1).Reverse().ToList();
    var firstRightBlockingTreeIdx = rightTrees.FindLastIndex(height => height >= treeHeight);
    var viewCountFromRight = firstRightBlockingTreeIdx == -1 ? rightTrees.Count : treeGrid[row].Count - col - firstRightBlockingTreeIdx - 1;

    var topTrees = treeGrid.Skip(0).Take(row).Select(ints => ints[col]).ToList();
    var firstTopBlockingTreeIdx = topTrees.FindLastIndex(height => height >= treeHeight);
    var viewCountFromTop = firstTopBlockingTreeIdx == -1 ? topTrees.Count : row - firstTopBlockingTreeIdx;

    var bottomTrees = treeGrid.Skip(row + 1).Take(treeGrid.Count - row - 1).Select(ints => ints[col]).Reverse().ToList();
    var firstBottomBlockingTreeIdx = bottomTrees.FindLastIndex(height => height >= treeHeight);
    var viewCountFromBottom = firstBottomBlockingTreeIdx == -1 ? bottomTrees.Count : treeGrid.Count - row - firstBottomBlockingTreeIdx - 1;

    var scenicScore = viewCountFromLeft * viewCountFromRight * viewCountFromTop * viewCountFromBottom;
    if (scenicScore > highestScenicScore) highestScenicScore = scenicScore;
}

Console.WriteLine($"HighestScenicScore is {highestScenicScore}");

Console.WriteLine("Finished analysing tree grid.");