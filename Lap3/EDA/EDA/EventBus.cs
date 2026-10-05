using EDA.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Action<IEvent>>> _handlers = new();
        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Action<IEvent>>();
            }
            _handlers[eventType].Add(e => handler((TEvent)e));
        }
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handler in _handlers[eventType])
                {
                    handler(@event);
                }
            }
        }
    }
}
