using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions {
    public abstract class BaseInteraction : MonoBehaviour
    {
// INTERACTION HANDLING

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list){
            for (int i = 0; i < list.Count; i++){
                var action = list[i];

                switch (action.Action){
                    case InteractionType.Trigger:
                        action.Target.Trigger();
                        break;

                    case InteractionType.Begin:
                        action.Target.Begin();
                        break;

                    case InteractionType.End:
                        action.Target.End();
                        break;

                    case InteractionType.Add:
                        action.Target.Add();
                        break;

                    case InteractionType.Remove:
                        action.Target.Remove();
                        break;

                    case InteractionType.Open:
                        action.Target.Open();
                    break;

                    case InteractionType.Close:
                        action.Target.Close();
                        break;

                    case InteractionType.Reset:
                        action.Target.Restore();
                        break;
                }
            }
        }

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list, int value){
            for (int i = 0; i < list.Count; i++){
                var action = list[i];

                switch (action.Action){
                    case InteractionType.Changed:
                        action.Target.Changed(value);
                        break;
                }
            }
        }

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list, float value){
            for (int i = 0; i < list.Count; i++){
                var action = list[i];

                switch (action.Action){
                    case InteractionType.Changed:
                        action.Target.Changed(value);
                        break;
                }
            }
        }

// INTERACTION ACTIONS

        public virtual void Trigger(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Trigger action"
            ));
        }
        public virtual void Begin(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Begin action"
            ));
        }
        public virtual void End(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement End action"
            ));
        }
        public virtual void Add(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Add action"
            ));
        }
        public virtual void Remove(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Remove action"
            ));
        }
        public virtual void Increment(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Increment action"
            ));
        }
        public virtual void Decrement(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Decrement action"
            ));
        }
        public virtual void Open(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Open action"
            ));
        }
        public virtual void Close(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Close action"
            ));
        }
        public virtual void Restore(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Restore action"
            ));
        }
        public virtual void Changed(int value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Int action"
            ));
        }
        public virtual void Changed(float value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Float action"
            ));
        }


// DATA HANDLING
        
        /// <summary>
        /// Returns the lists of all action lists
        /// </summary>
        public virtual List<List<ActionInfo>> GetActions() => null;
    }
}