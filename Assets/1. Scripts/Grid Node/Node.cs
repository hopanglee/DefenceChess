using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Vector2Int m_pos;
    public bool isEmpty { get; private set; } = true;
    private GridPositionable m_gridObject;

    void Awake()
    {
        float _x = transform.localPosition.x;
        float _z = transform.parent.localPosition.z;

        int x = Mathf.RoundToInt(_x / NodeManager.x_interval);
        int z = Mathf.RoundToInt(_z / NodeManager.z_interval);
        m_pos = new(x, z);

        NodeManager.AddNode(x, z, this);
    }

    public void SetGridObject(GridPositionable gridObject)
    {
        this.m_gridObject = gridObject;
        this.isEmpty = false;
    }

    public GridPositionable GetGridObject()
    {
        return m_gridObject;
    }
}
