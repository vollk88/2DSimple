using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using Units;
using UnityEngine;

public class AUnit : CustomBehaviour
{
    [SerializeField] protected Health _health;
    protected Animator _anim;

    public Animator anim => _anim;
    

    public Health health
    {
        get => _health;
        set => _health = value;
    }

    protected void Start()
    {
        transform1 = transform;
    }

    public override string ToString()
    {
        return base.ToString() + "hp: " + health;
    }
}
