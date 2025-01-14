using UnityEngine;

public class UnitStateDeath : UnitState
{
    public UnitStateDeath(Unit unit) : base(unit)
    {

    }

    public override void Update()
    {
        //Debug.Log($"{m_unit.name} > Death");

    }
}