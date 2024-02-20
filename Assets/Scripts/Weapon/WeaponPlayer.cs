using System;
using DefaultNamespace;
using Tools;
using Units;
using Units.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    [RequireComponent(typeof(BulletManager))]
    public class WeaponPlayer : AWeapon
    {
        private PlayerController _playerController;
        private BulletManager _bulletManager;
        [SerializeField, Tooltip("Bullet for weapon")] private Bullet _bullet;

        public Bullet bullet => _bullet;

        [SerializeField] private BulletStats _bulletStats;


        private void Start()
        {
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _bulletManager = GetComponent<BulletManager>();
            _bulletManager.Init(bullet, _bulletStats.bulletSpawn.transform);
        }

        private void OnEnable()
        {
            InputManager.PlayerActions.Fire.performed += Attack;
        }
        
        private void OnDisable()
        {
            InputManager.PlayerActions.Fire.performed -= Attack;
        }

        private void Attack(InputAction.CallbackContext obj)
        {
            _playerController.anim.SetTrigger("attack");
            Shot();
        }

        private void Shot()
        {
            _bulletManager.AddBullet(_bulletStats, PlayerController.mousePos);
        }
        
    }
}