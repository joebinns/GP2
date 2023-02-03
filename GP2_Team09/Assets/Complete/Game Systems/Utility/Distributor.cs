using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameProject
{
    public sealed class Distributor<T>
    {
        private T _previous;
        private Randomise _random;
        private Dictionary<T, int> _items;

// INITIALISATION

        /// <summary>
        /// Initialises with random seed
        /// </summary>
        public Distributor(T[] items){
            Initialise(items, null);
        }

        /// <summary>
        /// Initialises with custom seed
        /// </summary>
        public Distributor(T[] items, int seed){
            Initialise(items, seed);
        }

        /// <summary>
        /// Initialises with seed
        /// </summary>
        private void Initialise(T[] items, int? seed){
            _random = seed != null ? 
                new Randomise(seed.Value) :
                new Randomise();

            _items = new Dictionary<T, int>();
            AddItems(items);
        }

// DISTRIBUTION

        /// <summary>
        /// Adds additional items to the list of available items
        /// <br>Newer items will, at start, have a higher chance to be picked</br>
        /// </summary>
        public void AddItems(T[] items){
            if (items?.Length == 0) return;

            for (int i = 0; i < items.Length; i++){
                _items.Add(items[i], 0);
            }
        }

        /// <summary>
        /// Returns the next suitable item
        /// </summary>
        public T GetNext(){
            if (_items.Count > 1)
                return GetItem();

            else if (_items.Count == 1)
                return _items.Keys.First();

            return default(T);
        }

        /// <summary>
        /// Returns the next suitable item
        /// </summary>
        private T GetItem(){
            var available = GetSuitable();
            var index = _random.Value(0, available.Count);
            var picked = available[index];

            _items[picked]++;
            _previous = picked;
            return picked;
        }

        /// <summary>
        /// Returns a list of suitable items
        /// </summary>
        private List<T> GetSuitable(){
            int minOccurance = _items.Values.Min();

            var suitable = _items.Keys.Where(
                x => _items[x] == minOccurance
            ).ToList();

            suitable.Remove(_previous);

            if (suitable.Count == 0){
                suitable = _items.Keys.ToList();
                suitable.Remove(_previous);
            }

            return suitable;
        }
    }
}