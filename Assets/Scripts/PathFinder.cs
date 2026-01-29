using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;

    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int endCoordinates;

    public Vector2Int EndCoordinates { get { return endCoordinates; } }
    
    Node currentSearchNode;
    Node startNode;
    Node endNode;

    Vector2Int[] directions = 
        { 
        Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left
        };

    Queue<Node> frontier = new Queue<Node>();
    
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();


    GridManager gridManager;

    void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        if(gridManager != null )
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            endNode = grid[endCoordinates];
        }

        //startNode = new Node(startCoordinates, true);
        //endNode = new Node(endCoordinates, true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startNode = gridManager.Grid[startCoordinates];
        endNode = gridManager.Grid[endCoordinates];

        BreadthFirstSearch();
        BuildPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(grid[neighbourCoords]);

            }
        }
        foreach(Node neighbour in neighbours)
        {
            if(!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    void BreadthFirstSearch()
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startNode.coordinates, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;

            ExploreNeighbours();


            if(currentSearchNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("FindPath", SendMessageOptions.DontRequireReceiver);
    }
}
