using Data.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Threading.Tasks;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controller
{
    public class WeaponAtackController : MonoBehaviour, IPullObject
    {
        private SerializedDictionary<DefanderType, DefanderCharacterData> _defanderCharacterData;

        private bool _ısStartAttack;

        private Rigidbody _rigidbody;

        private float _timer=0.5f;
        private DefanderType _defanderType;
        private PoolObjectType poolObjectType;

        internal void SetData(SerializedDictionary<DefanderType, DefanderCharacterData> defanderCharacterData)
        {
            _defanderCharacterData = defanderCharacterData;
        }

        internal void StartAtack(DefanderType defanderType, bool ısStartAttack)
        {
            _defanderType = defanderType;

            if (gameObject == null) return;

            _ısStartAttack = ısStartAttack;

            poolObjectType = (PoolObjectType)(int)_defanderCharacterData[defanderType].bulletType;
        }

        private void FixedUpdate()
        {

            if (_ısStartAttack)
            {
                _timer -= Time.fixedDeltaTime;

                if (_timer < 0)
                {
                     Fire();
                    _timer = _defanderCharacterData[_defanderType].Interval;
                }
            }
        }
        private void Fire()
        {
   
            GameObject chosenBullet = PullFromPool(poolObjectType);///lookagain

            _rigidbody = chosenBullet.GetComponent<Rigidbody>();

            chosenBullet.transform.position = transform.position;

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0.5f);
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public int GetPowerToDefender()
        {
            return _defanderCharacterData[_defanderType].Damage;
        }


    }

    
}