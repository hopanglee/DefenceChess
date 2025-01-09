using UnityEngine;

public class GridPositionable : MonoBehaviour
{
    public Vector3Int gridPosition { get; private set; }

    private void Start()
    {
        if (gridPosition != null)
        {
            SetGridPosition(gridPosition.x, gridPosition.y, gridPosition.z);
        }
    }

    public void SetGridPosition(int x, int y, int z)
    {
        NodeManager.nodes[gridPosition].EmptyGridObject();
        gridPosition = new(x, y, z);
        NodeManager.nodes[gridPosition].SetGridObject(this);
    }

    public void SetGridPosition(Vector3Int cubePos)
    {
        SetGridPosition(cubePos.x, cubePos.y, cubePos.z);
    }
}
