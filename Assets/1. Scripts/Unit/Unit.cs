using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(GridPositionable))]
public abstract class Unit : MonoBehaviour
{
    [HorizontalGroup("Buttons", Width = 130)]
    [Button("StartTurn", ButtonSizes.Large)]
    private void DebugStartTurn()
    {
        StartTurn();
    }

    [HorizontalGroup("Buttons", Width = 130)]
    [Button("StopTurn", ButtonSizes.Large)]
    private void DebugStopTurn()
    {
        StopTurn();
    }

    public event Action OnAttack;

    // hp mp 제외한 stat
    [System.Serializable]
    public class UnitStat
    {
        public int Lv;
        public int AttackPower;       // 공격력
        public int AbilityPower;      // 주문력
        public int Defense;           // 방어력
        public int MagicResistance;   // 마법 저항력
        public int AttackRange;       // 사정거리
        public float HpDrain; // 체력 흡수
        public float AttackSpeed; // 공격 속도
        public int Speed; // 이동속도
    }

    public UnitStat unitStat;
    protected UnitState unitState;
    public bool isEnemy;

    public UnitInfo unitInfo;

    private GridPositionable _gridPositionable;

    public Animator animator;

    public virtual void Attack(IHasHP target)
    {
        OnAttack?.Invoke();
    }
    protected virtual void Awake()
    {
        UnitManager.AddUnit(this);
        _gridPositionable = GetComponent<GridPositionable>();
        unitStat.Lv = 1;

        unitState = new UnitStateIdle(this);
        ReloadStat();
    }

    public Vector3Int GetPosition()
    {
        return _gridPositionable.gridPosition;
    }

    public virtual void StartTurn()
    {
    }

    public virtual void StopTurn()
    {
        unitState = new UnitStateIdle(this);


    }

    private void Update()
    {
        unitState?.Update();
    }

    public void ChangeState(UnitState newState)
    {
        unitState = newState;
    }

    protected int CalculateDamage(AttackInfo attackInfo)
    {
        switch (attackInfo.damageType)
        {
            case AttackInfo.DamageType.True:
                return attackInfo.amount;

            case AttackInfo.DamageType.Physical:

                float physicalReducuction = (float)unitStat.Defense / (unitStat.Defense + 100);
                return Mathf.Max((int)(attackInfo.amount * (1 - physicalReducuction)), 1);

            case AttackInfo.DamageType.Magic:
                float magicalReducuction = (float)unitStat.MagicResistance / (unitStat.MagicResistance + 100);
                return Mathf.Max((int)(attackInfo.amount * (1 - magicalReducuction)), 1);

            default:
                return 0;
        }
    }

    protected virtual void ReloadStat()
    {
    }

}
