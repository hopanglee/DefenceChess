using Mono.Cecil.Cil;
using UnityEngine;

public static class PathFinder
{
    public static Node GetNextNode(Vector3Int curPos, Vector3Int targetPos)
    {
        var cubePos = GetNextPosition(curPos, targetPos);
        if(cubePos != null)
        {
            return NodeManager.GetNode((Vector3Int)cubePos);
        }
        return null;
    }

    public static Vector3Int? GetNextPosition(Vector3Int curPos, Vector3Int targetPos)
    {
        Vector3Int? closestNeighbor = null;
        int minDistance = int.MaxValue;

        foreach (var direction in Node.CubeDirections)
        {
            Vector3Int neighbor = curPos + direction;

            var neighborNode = NodeManager.GetNode(neighbor);
            if(!neighborNode || !neighborNode.isEmpty)
            {
                continue;
            }


            int distanceToTarget = Node.GetNodeDistance(neighbor, targetPos);

            if (distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                closestNeighbor = neighbor;
            }
        }

        return closestNeighbor;
    }
}
