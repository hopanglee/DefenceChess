using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public static class NodeManager
{
    public static Dictionary<Vector2Int, Node> nodes = new();
    public const float x_interval = 1;
    public const float z_interval = 1.732f / 2;

    public static void AddNode(int x, int y, Node node)
    {
        if (nodes.ContainsKey(new(x, y)))
        {
            Debug.LogError("Node Dictionary already has key.");
            return;
        }
        nodes.Add(new(x, y), node);
    }

    public static Node GetNode(int x, int y)
    {
        if (nodes.TryGetValue(new(x, y), out Node node))
        {
            return node;
        }

        Debug.LogWarning($"Node at ({x}, {y}) not found!");
        return null;
    }

    public static bool IsEmptyNode(int x, int y)
    {
        return nodes[new(x, y)].isEmpty;
    }
}
