using UnityEngine;

public class GridPositionable : MonoBehaviour
{
    public Vector2Int gridPosition { get; private set; }

    private void Start()
    {
        if (gridPosition != null)
        {
            SetGridPosition(gridPosition.x, gridPosition.y);
        }
    }

    public void SetGridPosition(int x, int y)
    {
        gridPosition = new(x, y);
        NodeManager.nodes[gridPosition].SetGridObject(this);
    }
}
