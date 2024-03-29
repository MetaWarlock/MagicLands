using UnityEngine;
using UnityEngine.Events;

namespace ArkGame.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(_tag))
            {
                if(_action != null)
                {
                    _action.Invoke();
                }
                
            }
        }
    }
}


