using System;
using UnityEngine;

namespace Units
{
    [Serializable]
    public struct DamageStats
    {
        public int hitDamage;
        [Range(0f,1f)] public float armorPenetration;
        // public Vector3 HitPosition { get; set; }
    }
}