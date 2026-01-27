using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] GameObject towerPrefab;

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            //Debug.Log("Clicked On" + transform.name);
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
