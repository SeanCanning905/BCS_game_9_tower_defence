using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CordinatesLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.green;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.orange;

    TextMeshPro label;
    Vector3Int coordinates = new Vector3Int();
    Tile waypoint;
    GridManager gridManager;

    void Awake()
    {
        //label.enabled = false;
        waypoint = GetComponentInParent<Tile>();

        label = GetComponent<TextMeshPro>();
        DisplayCordinates();

        gridManager = FindFirstObjectByType<GridManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCordinates();
            UpdateObjectName();
        }
        ToggleLabels();
        SetLabelColor();
    }

    void DisplayCordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        coordinates.z = Mathf.RoundToInt(transform.parent.position.y / UnityEditor.EditorSnapSettings.move.y);

        label.text = $"{coordinates.x}; {coordinates.y}; {coordinates.z}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null) return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
}
