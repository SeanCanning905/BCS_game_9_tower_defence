using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform enemyPosition;

    GridManager gridManager;
    Vector3Int coordinates = new Vector3Int();

    PathFinder pathFinder;


    void Awake()
    {
        pathFinder = FindFirstObjectByType<PathFinder>();
        gridManager = FindFirstObjectByType<GridManager>();
        enemyPosition = GetComponentInChildren<Transform>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(enemyPosition.transform.position);
            
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            //Debug.Log("Clicked On" + transform.name);
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
