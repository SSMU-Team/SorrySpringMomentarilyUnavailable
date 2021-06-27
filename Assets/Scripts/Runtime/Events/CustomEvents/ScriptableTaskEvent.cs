using System.Collections.Generic;
using UnityEngine;

namespace CustomEvents
{
     public delegate void ScriptableTaskEventCallback(ScriptableTask param);

    [CreateAssetMenu(fileName = "ScriptableTaskEvent", menuName = "Events/ScriptableTaskEvent")]
    public class ScriptableTaskEvent : ScriptableEvent
    {
        private ScriptableTaskEventCallback m_call;

        public void AddListener(ScriptableTaskEventListener listener)
        {
            m_call += listener.OnEventInvoke;
        }

        public void AddListener(ScriptableTaskEventCallback listener)
        {
            m_call += listener;
        }

        public void Invoke(ScriptableTask param)
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

        public void RemoveListener(ScriptableTaskEventListener listener)
        {
            m_call -= listener.OnEventInvoke;
        }

        public void RemoveListener(ScriptableTaskEventCallback listener)
        {
            m_call -= listener;
        }
    }
}