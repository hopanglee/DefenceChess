using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public interface IHasItem
{
    List<Item> Items{get; set;}
    int ItemMax { get; }

    event Action<List<Item>> OnUpdateItem;

    bool AddItem(Item item);
    void RemoveItemAll();
}
