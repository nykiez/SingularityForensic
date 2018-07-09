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

        public static void SubsToken<TEvent, TPayload>(ref SubscriptionToken token, Action<TPayload> subscriber)
            where TEvent : PubSubEvent<TPayload>, new() {
            var evt = Aggregator.GetEvent<TEvent>();
            if (token != null) {
                evt?.Unsubscribe(token);
            }
            
            token = evt?.Subscribe(subscriber);
        }

        public static void SubsToken<TEvent>(ref SubscriptionToken token, Action subscriber) where TEvent : PubSubEvent, new() {
            var evt = Aggregator.GetEvent<TEvent>();
            if (token != null) {
                evt.Unsubscribe(token);
            }
            token = evt.Subscribe(subscriber);
        }

        public static SubscriptionToken Subscribe<TEvent,TPayload>(Action<TPayload> subscriber) where TEvent:PubSubEvent<TPayload>,new(){
            return Aggregator?.GetEvent<TEvent>()?.Subscribe(subscriber);
        }

        public static void Subscribe<TEvent>(Action subscriber) where TEvent : PubSubEvent, new() {
            Aggregator?.GetEvent<TEvent>()?.Subscribe(subscriber);
        }
        
        /// <summary>
        /// 向事件处理器发布事件;
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="eventHandlers"></param>
        public static void PublishEventToHandlers<TEventHandler, TEventArgs>(TEventArgs args, IEnumerable<TEventHandler> eventHandlers)
            where TEventHandler : class,IEventHandler<TEventArgs> {
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
        /// <summary>
        /// 向事件处理契发布事件;将会自动寻找事件处理器队列;
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="args"></param>
        public static void PublishEventToHandlers<TEventHandler,TEventArgs>(TEventArgs args)
            where TEventHandler : class,IEventHandler<TEventArgs> {

            if (args == null) {
                return;
            }

            var handlers = GenericServiceStaticInstances<TEventHandler>.Currents;
            PublishEventToHandlers(args, handlers);
        }

        /// <summary>
        /// 向事件处理器发布事件;
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="eventHandlers"></param>
        public static void PublishEventToHandlers<TEventHandler>(IEnumerable<TEventHandler> eventHandlers) where TEventHandler:class, IEventHandler {
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
        /// <summary>
        /// 向事件处理契发布事件;将会自动寻找事件处理器队列;
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="args"></param>
        public static void PublishEventToHandlers<TEventHandler>() where TEventHandler :class, IEventHandler {
            var handlers = GenericServiceStaticInstances<TEventHandler>.Currents;
            PublishEventToHandlers<TEventHandler>(handlers);
        }

        /// <summary>
        /// 订阅事件;若订阅者已经订阅,则将不会订阅;
        /// 此方法适合在内存中常驻的对象(比如各种稳定的服务)进行订阅操作;
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TPayLoad"></typeparam>
        /// <param name="evt"></param>
        /// <param name="subscriber"></param>
        public static void SubscribeCheckingSubscribed<TEvent,TPayLoad>(this TEvent evt,Action<TPayLoad> subscriber) where TEvent:PubSubEvent<TPayLoad> {
            if (evt.Contains(subscriber)) {
                return;
            }
            evt.Subscribe(subscriber);
        }
    }
}
