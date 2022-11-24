using Data.ValueObject;
using Interfaces;
using Signals;
using Type;
using UnityEngine;

namespace Controller
{
    public class WeaponAtackController : MonoBehaviour, IPullObject
    {
        private DefanderCharacterData _defanderCharacterData;

        private bool _isAttack;

        private GameObject _gameObject;

        private Rigidbody _rigidbody;

        private float _timer;

        private PoolObjectType _poolObjectType;

        internal void SetData(DefanderCharacterData defanderCharacterData)
        {
            _defanderCharacterData = defanderCharacterData;
        }

        internal void StartAtack(GameObject gameObject)
        {
            if (gameObject == null) return;

            _isAttack = true;

            _gameObject = gameObject;

            _poolObjectType = (PoolObjectType)(int)_defanderCharacterData.bulletType;
        }

        private void FixedUpdate()
        {
            if (_gameObject == null) return;

            if (!_gameObject.activeInHierarchy) return;

            if (true)
            {
                _timer -= Time.fixedDeltaTime;

                if (_timer < 0)
                {
                    _timer = _defanderCharacterData.Interval;
                    Fire();
                }
            }
        }

        private void Fire()
        {
            Debug.Log("ss");
            GameObject chosenBullet = PullFromPool(_poolObjectType);///lookagain

            _rigidbody = chosenBullet.GetComponent<Rigidbody>();

            _rigidbody.transform.position = transform.position;

            _rigidbody.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public int GetPowerToDefender()
        {
            return _defanderCharacterData.Damage;
        }

        internal void ResetAtack()
        {
            _isAttack = false;
        }

        internal void StopAtack()
        {
            _isAttack = false;
        }
    }
}