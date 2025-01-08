using UnityEngine;

[RequireComponent(typeof(GridPositionable))]
public class GridMovable : MonoBehaviour
{
    private GridPositionable m_gridPositionable;

    private void Awake()
    {
        m_gridPositionable = GetComponent<GridPositionable>();
    }
}
