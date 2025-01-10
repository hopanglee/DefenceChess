using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUIItemContainer : UIBehaviour, IUIView
{
    [SerializeField]
    private GameObject _container;
    public int ItemMax { get; set; }

    public void Initialized()
    {
        Hide();
    }

    public void ItemUpdate(List<Item> items)
    {
        if(items.Count == 0) Hide();
        else
        {
            Show();
        }
    }

    public void Hide()
    {
        _container.gameObject.SetActive(false);
    }

    public void Show()
    {
        _container.gameObject.SetActive(true);
    }
}
