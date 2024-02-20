using System;
using UnityEngine;

namespace Tools
{
    public class CustomBehaviour : MonoBehaviour
    {
        private Transform _transform;

        public Transform transform1
        {
            get => _transform;
            set => _transform = value;
        }

        protected void Awake()
        {
            // Debug.Log("Awake");
            transform1 = transform;
        }
    }
}