using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Vector3Int m_pos;
    public bool isEmpty { get; private set; } = true;
    private GridPositionable m_gridObject;

    void Awake()
    {
        float _x = transform.localPosition.x;
        float _z = transform.parent.localPosition.z;

        int x_temp = Mathf.RoundToInt(_x / NodeManager.x_interval);
        int z_temp = Mathf.RoundToInt(_z / NodeManager.z_interval);

        int x = (z_temp + 1) / 2;
        int z = -z_temp ;
        x += x_temp;
        int y = -(x + z);
        

        m_pos = new(x, y, z);

        NodeManager.AddNode(x, y, z, this);
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
