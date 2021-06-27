using System.Collections.Generic;
using UnityEngine;

namespace CustomEvents
{
     public delegate void MenuTypeEventCallback(MenuType param);

    [CreateAssetMenu(fileName = "MenuTypeEvent", menuName = "Events/MenuTypeEvent")]
    public class MenuTypeEvent : ScriptableEvent
    {
        private MenuTypeEventCallback m_call;

        public void AddListener(MenuTypeEventListener listener)
        {
            m_call += listener.OnEventInvoke;
        }

        public void AddListener(MenuTypeEventCallback listener)
        {
            m_call += listener;
        }

        public void Invoke(MenuType param)
        {
            if (m_call != null)
            {
                m_call(param);
            }
            else
            {
                Debug.LogWarning("No event registered on " + name);
            }
        }

        public void RemoveListener(MenuTypeEventListener listener)
        {
            m_call -= listener.OnEventInvoke;
        }

        public void RemoveListener(MenuTypeEventCallback listener)
        {
            m_call -= listener;
        }
    }
}