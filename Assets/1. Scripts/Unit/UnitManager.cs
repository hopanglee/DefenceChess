using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public static class UnitManager
{
    public static HashSet<Unit> units = new();

    public static void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public static Unit GetNearestEnemy(bool isEnemy, Vector3 position)
    {
        float distance = float.PositiveInfinity;

        Unit nearestUnit = null;
        foreach (var _unit in units)
        {
            if (_unit.isEnemy != isEnemy)
            {
                var temp = position - _unit.transform.position;
                Vector2 vec = new(temp.x, temp.z);
                var new_distance = Vector2.SqrMagnitude(vec);
                if (new_distance < distance)
                {
                    nearestUnit = _unit;
                    distance = new_distance;
                }
                else if (new_distance == distance)
                {
                    ;
                }
            }
        }

        return nearestUnit;
    }
}
