using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public abstract class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite ItemSprite;

    public struct ItemStat
    {
        public int MaxHp;
        public int MaxMp;
        public int BaseMp;
        public int AttackPower;       // 공격력
        public int AbilityPower;      // 주문력
        public int Defense;           // 방어력
        public int MagicResistance;   // 마법 저항력
        public int AttackRange;       // 사정거리
        public int AttackSpeed;       // 공격속도
        public int HpDrain; // 체력 흡수
    }

    public ItemStat itemStat;

    public abstract void Active();
}
