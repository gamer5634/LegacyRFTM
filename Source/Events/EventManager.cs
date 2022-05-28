using System.Collections.Generic;

namespace MucciArena.Events
{
    public static class EventManager
    {
        private static bool _notInit = true;

        private static List<IEventListener> _listeners;

        public static void Initialize()
        {
            if (_notInit)
            {
                _notInit = false;
                _listeners = new List<IEventListener>();
            }
        }

        public static void RegisterEventListener(IEventListener eventListener)
        {
            if (!_listeners.Contains(eventListener))
            {
                _listeners.Add(eventListener);
            }
        }

        public static void SendMessage(string Event)
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                var before = _listeners.Count;
                _listeners[i].FetchEvent(Event);
                if (_listeners.Count > before) i--;
                else if (_listeners.Count < before) i++;
            }
        }
    }
}
