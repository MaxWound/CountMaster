using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wall : MonoBehaviour
{
    [SerializeField] private WallPart[] _wallPart = new WallPart[2];

    [SerializeField] private int[] _spawnCoefficient = new int[2];
    public void SetWallValue(int leftValue, int rightValue)
    {
        WallPart left;
        WallPart right;

        for (int i = 0; i < _wallPart.Length; i++)
        {
            if (_wallPart[i].Side == WallSide.Right)
            {
                right = _wallPart[i];
                right.SpawnCoefficient = leftValue;
            }else
            {
                left = _wallPart[i];
                left.SpawnCoefficient = rightValue;
            }
        }
    }

    #region OnValidate
    private void OnValidate()
    {
        if (_wallPart[0] != null && _wallPart[1] != null)
        {
            if (_wallPart[0].Side == _wallPart[1].Side)
                throw new ArgumentException("Wall doesn't contains two identity sides. Please change the sides correctly");
        }
    }
    #endregion

    private void Awake()
    {
        for(int i = 0; i < _wallPart.Length; i++)
        {
            _wallPart[i].Wall = this;
        }
        SetWallValue(_spawnCoefficient[0], _spawnCoefficient[1]);
    }

    private void Start()
    {
       
    }

    public void Triggered()
    {
        foreach(WallPart part in _wallPart)
        {
           bool _ = part != null ? part.Collider.enabled = false : part.Collider.enabled = true;
        }
    }
}


public enum WallOperation : int
{
    Sum = 2,
    Substruction = 3,
    Division = 1,
    Multiply = 0,

}
