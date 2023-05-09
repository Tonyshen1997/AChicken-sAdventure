using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] GameObject exitPrefab;
    [SerializeField] GameObject player;
    
    public int width = 10;
    public int height = 10;
    private float cellSize = 1.0f;

    private GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[width, height];
        GenerateMaze();
    }

    void GenerateMaze()
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Vector2Int start = new Vector2Int(1, 1);

        // Instantiate walls initially
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                InstantiateCell(x, y, wallPrefab);
            }
        }

        // Create entrance and exit
        InstantiateCell(0, 1, spawnPointPrefab);
        InstantiateCell(width - 1, height - 2, exitPrefab);

        stack.Push(start);
        visited.Add(start);
        InstantiateCell(start.x, start.y, pathPrefab);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> unvisitedNeighbors = GetUnvisitedNeighbors(current, visited);

            if (unvisitedNeighbors.Count > 0)
            {
                stack.Push(current);
                Vector2Int chosen = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWallBetween(current, chosen);
                visited.Add(chosen);
                stack.Push(chosen);
            }
        }
    }


    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell, HashSet<Vector2Int> visited)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        List<Vector2Int> offsets = new List<Vector2Int> { new Vector2Int(0, 2), new Vector2Int(2, 0), new Vector2Int(0, -2), new Vector2Int(-2, 0) };

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int neighbor = cell + offset;

            if (neighbor.x >= 0 && neighbor.x < width && neighbor.y >= 0 && neighbor.y < height && !visited.Contains(neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    void RemoveWallBetween(Vector2Int cellA, Vector2Int cellB)
    {
        Vector2Int middleCell = (cellA + cellB) / 2;
        InstantiateCell(middleCell.x, middleCell.y, pathPrefab);
        InstantiateCell(cellB.x, cellB.y, pathPrefab);
    }

    void InstantiateCell(int x, int y, GameObject prefab)
    {
        float offsetY = (prefab == pathPrefab || prefab == spawnPointPrefab) ? -1f : 0f;
        Vector3 position = new Vector3(x * cellSize, offsetY, y * cellSize);
        if (grid[x, y] != null)
        {
            Destroy(grid[x, y]);
        }
        grid[x, y] = Instantiate(prefab, position, Quaternion.identity, transform);

        if (prefab == spawnPointPrefab)
        {
            PlayerScript script = player.GetComponent<PlayerScript>();
            script.setSpawn(grid[x, y]);
        }
    }
}



