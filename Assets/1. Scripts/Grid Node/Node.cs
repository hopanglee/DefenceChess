using UnityEngine;

public class Node : MonoBehaviour
{
    public enum Direction { RightUp = 0, RightDown, Down, LeftDown, LeftUp, Up };
    public static readonly Vector3Int[] CubeDirections = new Vector3Int[]
    {
        new Vector3Int(1, -1, 0),  // 오른쪽 위
        new Vector3Int(1, 0, -1),  // 오른쪽 아래
        new Vector3Int(0, 1, -1),  // 아래
        new Vector3Int(-1, 1, 0),  // 왼쪽 아래
        new Vector3Int(-1, 0, 1),  // 왼쪽 위
        new Vector3Int(0, -1, 1)   // 위
    };

    public Vector3Int pos;
    public bool isEmpty { get; private set; } = true;
    private GridPositionable m_gridObject;

    void Awake()
    {
        float _x = transform.localPosition.x;
        float _z = transform.parent.localPosition.z;

        int x_temp = Mathf.RoundToInt(_x / NodeManager.x_interval);
        int z_temp = Mathf.RoundToInt(_z / NodeManager.z_interval);

        int x = (z_temp + 1) / 2;
        int z = -z_temp;
        x += x_temp;
        int y = -(x + z);


        pos = new(x, y, z);

        NodeManager.AddNode(x, y, z, this);
    }

    public void SetGridObject(GridPositionable gridObject)
    {
        this.m_gridObject = gridObject;
        this.isEmpty = false;
    }
    public void EmptyGridObject()
    {
        this.m_gridObject = null;
        this.isEmpty = true;
    }

    public GridPositionable GetGridObject()
    {
        return m_gridObject;
    }

    public static int GetNodeDistance(Node n1, Node n2)
    {
        return GetNodeDistance(n1.pos, n2.pos);
    }

    public static int GetNodeDistance(Vector3Int cube1, Vector3Int cube2)
    {
        return (Mathf.Abs(cube1.x - cube2.x) +
                Mathf.Abs(cube1.y - cube2.y) +
                Mathf.Abs(cube1.z - cube2.z)) / 2;
    }
}
