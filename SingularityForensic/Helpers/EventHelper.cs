using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System;

namespace SingularityForensic.Helpers {
    public static class PubEventHelper {
        private static IEventAggregator _aggregator;
        public static IEventAggregator Aggregator => _aggregator ?? (_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());

        //public static void Publish<TEvent, TPayload>(TPayload payload) where TEvent : PubSubEvent<TPayload>, new()
        //    => Aggregator?.GetEvent<TEvent>()?.Publish(payload);

        public static void Publish<TEvent>() where TEvent : PubSubEvent, new() => Aggregator?.GetEvent<TEvent>()?.Publish();
        public static void Publish<TEvent, TPayload>(TPayload payload) where TEvent : PubSubEvent<TPayload>,new() => 
            Aggregator?.GetEvent<TEvent>()?.Publish(payload);

        public static TEventType GetEvent<TEventType>() where TEventType : EventBase, new() => Aggregator?.GetEvent<TEventType>();

        public static void SubsToken<TEvent, TPayload>(ref SubscriptionToken token, Action<TPayload> act)
            where TEvent : PubSubEvent<TPayload>, new() {
            var evt = Aggregator.GetEvent<TEvent>();
            if (token != null) {
                evt?.Unsubscribe(token);
            }
            token = evt?.Subscribe(act);
        }

        public static void SubsToken<TEvent>(ref SubscriptionToken token, Action act) where TEvent : PubSubEvent, new() {
            var evt = Aggregator.GetEvent<TEvent>();
            if (token != null) {
                evt.Unsubscribe(token);
            }
            token = evt.Subscribe(act);
        }

        public static void Subscribe<TEvent,TPayload>(Action<TPayload> act) where TEvent:PubSubEvent<TPayload>,new(){
            Aggregator?.GetEvent<TEvent>()?.Subscribe(act);
        }

        public static void Subscribe<TEvent>(Action act) where TEvent : PubSubEvent, new() {
            Aggregator?.GetEvent<TEvent>()?.Subscribe(act);
        }
    }
}
