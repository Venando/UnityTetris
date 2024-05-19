using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Tetris.Messages
{
    public static class Messenger
    {
        private static readonly Dictionary<Type, List<object>> Callbacks = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            Type messageType = typeof(T);
            
            if (Callbacks.TryGetValue(messageType, out List<object> list))
            {
                list.Add(callback);
            }
            else
            {
                Callbacks[messageType] = new List<object>
                {
                    callback
                };
            }
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            Type messageType = typeof(T);
            
            if (Callbacks.TryGetValue(messageType, out List<object> list))
            {
                list.Remove(callback);
            }
        }
        
        public static void Publish<T>(T message)
        {
            Type messageType = typeof(T);
            
            if (!Callbacks.TryGetValue(messageType, out List<object> callbackList))
            {
                return;
            }
            
            for (var i = 0; i < callbackList.Count; i++)
            {
                object callback = callbackList[i];

                if (callback == null)
                {
                    continue;
                }
                
                try
                {
                    UnsafeUtility.As<object, Action<T>>(ref callback)?.Invoke(message);
                }
                catch (Exception exception)
                {
                    Debug.Log(exception);
                }
            }
        }
    }
}