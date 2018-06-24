using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingularityForensic.Contracts.Helpers {
    public static class PubEventHelper {
        private static IEventAggregator _aggregator;
        public static IEventAggregator Aggregator => _aggregator ?? (_aggregator = ServiceProvider.Current.GetInstance<IEventAggregator>());

        //public static void Publish<TEvent, TPayload>(TPayload payload) where TEvent : PubSubEvent<TPayload>, new()
        //    => Aggregator?.GetEvent<TEvent>()?.Publish(payload);

        public static void Publish<TEvent>() where TEvent : PubSubEvent, new() => Aggregator?.GetEvent<TEvent>()?.Publish();
        public static void Publish<TEvent, TPayload>(TPayload payload) where TEvent : PubSubEvent<TPayload>,new () => 
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

        public static SubscriptionToken Subscribe<TEvent,TPayload>(Action<TPayload> act) where TEvent:PubSubEvent<TPayload>,new(){
            return Aggregator?.GetEvent<TEvent>()?.Subscribe(act);
        }

        public static void Subscribe<TEvent>(Action act) where TEvent : PubSubEvent, new() {
            Aggregator?.GetEvent<TEvent>()?.Subscribe(act);
        }
        
        /// <summary>
        /// 向事件处理器发布事件;
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="eventHandlers"></param>
        public static void PublishEventToHandlers<TEventHandler, TEventArgs>(TEventArgs args, IEnumerable<TEventHandler> eventHandlers)
            where TEventHandler : IEventHandler<TEventArgs> {
            if (eventHandlers == null) {
                return;
            }
            
            foreach (var handler in eventHandlers.OrderBy(p => p.Sort)) {
                if (handler == null) {
                    LoggerService.WriteCallerLine($"{nameof(handler)} coudn't be null.");
                    continue;
                }

                try {
                    if (!handler.IsEnabled) {
                        continue;
                    }
                    handler.Handle(args);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine($"{handler.GetType()}:{ex.Message}");
                    LoggerService.WriteException(ex);
                }
            }
        }
        public static void PublishEventToHandlers<TEventHandler>( IEnumerable<TEventHandler> eventHandlers) where TEventHandler:IEventHandler {
            if (eventHandlers == null) {
                return;
            }

            foreach (var handler in eventHandlers.OrderBy(p => p.Sort)) {
                if (handler == null) {
                    LoggerService.WriteCallerLine($"{nameof(handler)} coudn't be null.");
                    continue;
                }

                try {
                    if (!handler.IsEnabled) {
                        continue;
                    }
                    handler.Handle();
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine($"{handler.GetType()} ex.Message");
                    LoggerService.WriteException(ex);
                }
            }
        }
    }
}
