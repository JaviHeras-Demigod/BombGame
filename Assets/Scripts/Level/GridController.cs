using System.Collections.Generic;
using UnityEngine;
using Bomberman.Data;

namespace Bomberman.Level
{

    public class GridController
    {
        private static Vector3[] grid;
        private static List<int> freePositions;
        private static GridController instance;

        public static Vector3[] Grid { get => grid; }
        public static List<int> GridFreePositions { get => freePositions; }

        public GridController(StandardLevel levelToLoad)
        {
            CalculateGrid(levelToLoad);
        }

        private void CalculateGrid(StandardLevel levelToLoad)
        {
            grid = new Vector3[CalculateGridDimension(levelToLoad.dimensions)];
            freePositions = new List<int>();

            int currentGridIndex = 0;
            Vector3 currentGridPosition = Vector3.zero;
            Vector3 distanceBetwenPoints = Vector3.right;

            for (int i = 0; i < levelToLoad.dimensions.y; i++)
            {
                for (int o = 0; o < levelToLoad.dimensions.x / distanceBetwenPoints.x; o++)
                {
                    grid[currentGridIndex] = currentGridPosition;
                    freePositions.Add(currentGridIndex);

                    currentGridIndex++;
                    currentGridPosition += distanceBetwenPoints;
                }
                distanceBetwenPoints = i % 2 != 0 ? Vector3.right : Vector3.right * 2;
                currentGridPosition = new Vector3(0, 0, 1 * i + 1);
            }
        }

        private int CalculateGridDimension(Vector2 dimension)
        {
            int gridDimension = 0;
            int pointsPerRow = (int)dimension.x;
            for (int i = 0; i < dimension.y; i++)
            {
                gridDimension += pointsPerRow;
                pointsPerRow = (int)(i % 2 == 0 ? dimension.x : dimension.x / 2 + 1);
            }

            return gridDimension;
        }

        public static bool HasNearestsGridFree(Vector3 position)
        {

            Vector2 difference;
            for (int i = 0; i < freePositions.Count; i++)
            {
                difference = new Vector2(grid[freePositions[i]].x, grid[freePositions[i]].z) - new Vector2(position.x, position.z);
                if (difference.sqrMagnitude < 1.56f)
                    return true;
            }
            return false;
        }

        public static GridPoint GetNearestGridFreePosition(Vector3 position)
        {
            GridPoint nearestGridPoint = new GridPoint();

            float nearestDistance = 100;
            Vector2 difference;
            float sqrDistance;

            for (int i = 0; i < freePositions.Count; i++)
            {
                difference = new Vector2(grid[freePositions[i]].x, grid[freePositions[i]].z) - new Vector2(position.x, position.z);
                sqrDistance = difference.sqrMagnitude;
                if (sqrDistance < 4 && sqrDistance < nearestDistance)
                {
                    nearestGridPoint.index = freePositions[i];
                    nearestGridPoint.position = grid[freePositions[i]];
                    nearestDistance = sqrDistance;
                }

            }

            return nearestGridPoint;
        }

        public static void TakeGridPosition(int gridIndex)
        {
            freePositions.Remove(gridIndex);
        }

        public static void LeaveFreeGrid(int index)
        {
            if (!freePositions.Contains(index))
                freePositions.Add(index);
        }

        public static Vector3 TakeRandomFreePosition()
        {
            return grid[freePositions[Random.Range(0, freePositions.Count)]];
        }
    }
    
}
