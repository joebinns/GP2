//#define SHOW_ERRORS
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameProject
{
    public class Subscription<T> where T : System.Delegate
    {
        private List<T> _unsorted = null;
        private List<Set<T>> _sorted = null;

        private readonly bool _sorting = false;
        private readonly struct Set<X> : 
        IComparable<Set<X>>, IEquatable<Set<X>> {
            public readonly int Key;
            public readonly X Item;

            public Set(int key, X item){ Key = key; Item = item; }
            public int CompareTo(Set<X> other) => Key.CompareTo(other.Key);
            public bool Equals(Set<X> other) => Key.Equals(other.Key);
        }

// PROPERTIES

        public bool Sorted => _sorting;
        public int Count => _sorting ? _sorted.Count : _unsorted.Count;

// INITIALISATION

        /// <summary>
        /// Initialises a new subscription
        /// </summary>
        public Subscription(bool sorted = false){
            if (sorted) _sorted = new();
            else _unsorted = new();

            _sorting = sorted;
        }

        /// <summary>
        /// Initialises the subscription
        /// </summary>
        public Subscription(int capacity, bool sorted = false){
            if (sorted) _sorted = new(capacity);
            else _unsorted = new(capacity);

            _sorting = sorted;
        }

// SUBSCRIPTION

        /// <summary>
        /// Adds the subscriber to the list of subscribers
        /// </summary>
        /// <param name="priority">Executed from highest to lowest</param>
        public void AddSubscriber(T subscriber, int priority = 0){
            if (subscriber != null){
                if (_sorting){ 
                       AddSorted(subscriber, priority);
                } else AddUnsorted(subscriber);

            } else NullSubscription();
        }

        /// <summary>
        /// Adds the subscriber to the sorted list
        /// </summary>
        /// <param name="priority">Executed from highest to lowest</param>
        private void AddSorted(T subscriber, int priority){
            if (!_sorted.Exists(s => s.Item == subscriber)){
                _sorted.Add(new(priority, subscriber));
                _sorted.Sort();

            } else ExistingSubscriber(subscriber);
        }

        /// <summary>
        /// Adds the subscriber to the unsorted list
        /// </summary>
        private void AddUnsorted(T subscriber){
            if (!_unsorted.Contains(subscriber)){
                _unsorted.Add(subscriber);

            } else ExistingSubscriber(subscriber);
        }


        /// <summary>
        /// Removes the subscriber from the list of subscribers
        /// </summary>
        public void RemoveSubscriber(T subscriber){
            if (subscriber != null) {
                if (_sorting){
                       RemoveSorted(subscriber);
                } else RemoveUnsorted(subscriber);

            } else NullSubscription();
        }

        /// <summary>
        /// Removes the subscriber from the sorted list
        /// </summary>
        private void RemoveSorted(T subscriber){
            int index = _sorted.FindIndex(
                s => s.Item == subscriber
            );
            
            if (index > -1){ 
                _sorted.RemoveAt(index);

            } else MissingSubscriber(subscriber);
        }

        /// <summary>
        /// Removes the subscriber from the unsorted list
        /// </summary>
        private void RemoveUnsorted(T subscriber){
            if (_unsorted.Contains(subscriber)){
                _unsorted.Remove(subscriber);

            } else MissingSubscriber(subscriber);
        }


        /// <summary>
        /// Removes all current subscribers from the list of subscribers
        /// </summary>
        public void ClearSubscribers(){
            if (_sorting) _sorted.Clear();
            else _unsorted.Clear();
        }

// NOTIFICATION

        /// <summary>
        /// Notifies all current subscribers
        /// </summary>
        public void NotifySubscribers(params object[] args){
            if (_sorting) NotifySorted(args);
            else NotifyUnsorted(args);
        }

        /// <summary>
        /// Notifies all sorted subscribers
        /// </summary>
        private void NotifySorted(params object[] args){
            for (int i = _sorted.Count - 1; i >= 0; i--){
                _sorted[i].Item.DynamicInvoke(args);
            }
        }

        /// <summary>
        /// Notifies all unsorted subscribers
        /// </summary>
        private void NotifyUnsorted(params object[] args){
            for (int i = _unsorted.Count - 1; i >= 0; i--){
                _unsorted[i].DynamicInvoke(args);
            }
        }

// DEBUG ERRORS

        /// <summary>
        /// Logs a null subscriber error to the console
        /// </summary>
        private void NullSubscription(){
        #if SHOW_ERRORS
            Debug.LogError(new System.NullReferenceException(
                $"Null is an invalid subscriber"
            ));
        #endif
        }

        /// <summary>
        /// Logs an existing subscriber error to the console
        /// </summary>
        private void ExistingSubscriber(T action){
        #if SHOW_ERRORS
            Debug.LogError(new System.ArgumentException(
                $"{action.Method.Name} is already a current subscriber"
            ));
        #endif
        }

        /// <summary>
        /// Logs a missing subscriber error to the console
        /// </summary>
        private void MissingSubscriber(T action){
        #if SHOW_ERRORS
            Debug.LogError(new System.InvalidOperationException(
                $"{action.Method.Name} is not a current subscriber"
            ));
        #endif
        }
    }
}