using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions {
    [RequireComponent(typeof(ObjectID))]
    public abstract class BaseInteraction : MonoBehaviour
    {
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
        public virtual void Changed(bool value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Bool action"
            ));
        }
        public virtual void Changed(float value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Float action"
            ));
        }
        public virtual void Changed(BaseInteraction sender, bool value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Bool action"
            ));
        }
        public virtual void Changed(BaseInteraction sender, float value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Changed Float action"
            ));
        }
        
        public virtual void Finalise(){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Finalise action"
            ));
        } 
        public virtual void Compare(BaseInteraction value){
            Debug.LogError(new System.NotImplementedException(
                $"{this.GetType().Name} does not implement Compare action"
            ));
        }

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

                    case InteractionType.Increment:
                        action.Target.Increment();
                        break;

                    case InteractionType.Decrement:
                        action.Target.Decrement();
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

                    case InteractionType.Finalize:
                        action.Target.Finalise();
                        break;

                    case InteractionType.Compare:
                        action.Target.Compare(this);
                        break;
                }
            }
        }

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list, bool value){
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

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list, BaseInteraction sender, bool value){
            for (int i = 0; i < list.Count; i++){
                var action = list[i];

                switch (action.Action){
                    case InteractionType.Changed:
                        action.Target.Changed(sender, value);
                        break;
                }
            }
        }

        /// <summary>
        /// Performs interactions from action list
        /// </summary>
        protected void Interact(List<ActionInfo> list, BaseInteraction sender, float value){
            for (int i = 0; i < list.Count; i++){
                var action = list[i];

                switch (action.Action){
                    case InteractionType.Changed:
                        action.Target.Changed(sender, value);
                        break;
                }
            }
        }

// DATA HANDLING
        
        /// <summary>
        /// Returns the lists of all action lists
        /// </summary>
        public virtual List<List<ActionInfo>> GetActions() => null;

        /// <summary>
        /// Assigns the lists of actions
        /// </summary>
        public virtual void SetActions(List<List<ActionInfo>> actions){}
    }
}