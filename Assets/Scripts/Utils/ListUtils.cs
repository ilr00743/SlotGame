using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class ListUtils
    {
        public static T[,] NestedListsToArray<T>(List<List<T>> list)
        {
            if (list == null || list.Count == 0 || list[0] == null || list[0].Count == 0)
            {
                throw new ArgumentException("List cannot be null or empty.");
            }
            
            int rows = list.Count;
            int cols = list[0].Count;
            
            if (list.Any(innerList => innerList.Count != cols))
            {
                throw new ArgumentException("All nested lists must have the same number of items.");
            }
            
            T[,] array = new T[rows, cols];
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    array[row, col] = list[row][col];
                }
            }

            return array;
        }
    }
}