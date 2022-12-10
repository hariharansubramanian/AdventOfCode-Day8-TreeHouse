Console.WriteLine("Analysing tree grid...");

// var lines = File.ReadAllLines("tree_grid.txt");
var lines = File.ReadAllLines("test_input.txt");

var treeGrid = lines.Select(line =>
{
    var numbers = line.Select(c => (int) char.GetNumericValue(c)).ToList();
    return numbers;
}).ToList();

var visibleTreeCount = 0;

for (var i = 0; i < treeGrid.Count; i++)
{
    for (var j = 0; j < treeGrid[i].Count; j++)
    {
        var treeHeight = treeGrid[i][j];
        if (i == 0 || j == 0 || i == treeGrid.Count - 1 || j == treeGrid[i].Count - 1) visibleTreeCount++; // edge detected
        else
        {
            var isVisibleFromLeft = true;
            var isVisibleFromRight = true;
            var isVisibleFromTop = true;
            var isVisibleFromBottom = true;

            // check left
            for (var leftIndex = j - 1; leftIndex >= 0; leftIndex--)
            {
                var height = treeGrid[i][leftIndex];
                if (treeHeight <= height)
                {
                    isVisibleFromLeft = false;
                    break;
                }
            }

            // check top
            for (var topIndex = i - 1; topIndex >= 0; topIndex--)
            {
                var height = treeGrid[topIndex][j];
                if (treeHeight <= height)
                {
                    isVisibleFromTop = false;
                    break;
                }
            }

            // check right
            for (var rightIndex = j + 1; rightIndex < treeGrid[i].Count; rightIndex++)
            {
                var height = treeGrid[i][rightIndex];
                if (treeHeight <= height)
                {
                    isVisibleFromRight = false;
                    break;
                }
            }

            // check bottom
            for (var bottomIndex = i + 1; bottomIndex < treeGrid.Count; bottomIndex++)
            {
                var height = treeGrid[bottomIndex][j];
                if (treeHeight <= height) isVisibleFromBottom = false;
            }

            if (isVisibleFromBottom || isVisibleFromLeft || isVisibleFromRight || isVisibleFromTop)
            {
                visibleTreeCount++;
            }
        }
    }

    Console.WriteLine($"There are {visibleTreeCount} visible trees");
}

Console.WriteLine("Finished analysing tree grid.");