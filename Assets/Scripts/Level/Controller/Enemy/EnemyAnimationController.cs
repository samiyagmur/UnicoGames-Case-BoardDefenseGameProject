using Type;
using UnityEngine;

namespace Controller
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private void OnEnable() => ChangeAnimaimation(EnemyAnimType.Run);

        private void OnDisable() => ChangeAnimaimation(EnemyAnimType.Die);

        private void ChangeAnimaimation(EnemyAnimType animType)
        {
            //animator.Play((int)animType);
        }
    }
}