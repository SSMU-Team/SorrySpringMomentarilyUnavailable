using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
    public class MenuTypeEventListener : MonoBehaviour
    {
        #region Inspector
#pragma warning disable 0649

        [SerializeField]
        private MenuTypeEvent m_event;

        [SerializeField]
        private UnityEvent<MenuType> m_func;
        
#pragma warning restore 0649
        #endregion

        public MenuTypeEvent GetEvent => m_event;

		public UnityEvent<MenuType> Func { get => m_func; }

        public void OnEnable()
        {
            GetEvent.AddListener(this);
        }

        public void OnDisable()
        {
            GetEvent.RemoveListener(this);
        }

        public void OnEventInvoke(MenuType parameters)
        {
#if EVENT_DEBUG
            DebugInvoke();
#endif
            m_func.Invoke(parameters);
        }
        
        public void DebugInvoke()
        {
            Debug.Log("Invoked " + m_event.name + " in " + gameObject.name, gameObject);
        }
    }
}