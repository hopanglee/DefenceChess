using UnityEngine;

[RequireComponent(typeof(GridPositionable))]
public class GridMovable : MonoBehaviour
{
    private GridPositionable m_gridPositionable;
    private PathFinder m_pathFinder = new PathFinder();

    private void Awake()
    {
        m_gridPositionable = GetComponent<GridPositionable>();
    }
}
