using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Scriptable Objects/UnitInfo")]
public abstract class UnitInfo : ScriptableObject
{
    [System.Serializable]
    public struct UnitBaseStat
    {
        public int MaxHp;
        public int MaxMp;
        public int MaxItem;
        public int AttackPower;       // 공격력
        public int AbilityPower;      // 주문력
        public int Defense;           // 방어력
        public int MagicResistance;   // 마법 저항력
        public int AttackRange;       // 사정거리
        public float HpDrain;
        public int Speed;
        public float AttackSpeed;     // 공격속도
    }

    [Header("Unit Stats by Level")]
    public UnitBaseStat[] LvUnitStat = new UnitBaseStat[4]; // 배열 초기화

    [Header("General Unit Info")]
    public string Name;
    public Sprite UnitSprite;
    public int Price;
}
