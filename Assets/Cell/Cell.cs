using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell
{
    public int x { get; private set; }
    public int y { get; private set; }
    public int value {  get; private set; }

    public event Action OnPositionChanged;
    public event Action OnValueChanged;
    public event Action OnDelete;

    public Cell((int x, int y) coords, int value)
    {
        this.x = coords.x;
        this.y = coords.y;
        this.value = value;
    }

    public void Move(int x, int y)
    {
        this.x = x;
        this.y = y;
        OnPositionChanged();
        return;
    }
    public void Double()
    {
        value += value;
        OnValueChanged();
        return;
    }

    public void Delete()
    {
        OnDelete();
        return;
    }
}
