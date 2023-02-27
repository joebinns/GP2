using ActionAxis = System.Action<UnityEngine.Vector3>;
using ActionPlane = System.Action<UnityEngine.Vector2>;
using ActionValue = System.Action<float>;
using ActionIndex = System.Action<int>;
using ActionState = System.Action<bool>;
using ActionKey = System.Action;
using UnityEngine;

namespace GameProject.Inputs {
    public sealed partial class InputManager : ScriptableObject
    {
// SUBSCRIPTIONS

        /// <summary>
        /// Subscribes to the input manager
        /// <br>Output: 3D direction</br>
        /// </summary>
        public void SubscribeVec3(ActionAxis subscriber, InputType input, int priority = 0){
            switch (input){
                case InputType.Move: Subscribe<ActionAxis>(_onMove, subscriber, priority); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribeVec3(ActionAxis subscriber, InputType input){
            switch (input){
                case InputType.Move: Unsubscribe<ActionAxis>(_onMove, subscriber); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }


        /// <summary>
        /// Subscribes to the input manager
        /// <br>Output: 2D direction or position</br>
        /// </summary>
        public void SubscribesVec2(ActionPlane subscriber, InputType input, int priority = 0){
            switch (input){
                case InputType.Cursor:  Subscribe<ActionPlane>(_onCursor, subscriber, priority);  break;
                case InputType.Pointer: Subscribe<ActionPlane>(_onPointer, subscriber, priority); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribesVec2(ActionPlane subscriber, InputType input){
            switch (input){
                case InputType.Cursor:  Unsubscribe<ActionPlane>(_onCursor, subscriber);  break;
                case InputType.Pointer: Unsubscribe<ActionPlane>(_onPointer, subscriber); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }


        /// <summary>
        /// Subscribes to the input manager
        /// <br>Output: Float direction or duration</br>
        /// </summary>
        public void SubscribeFloat(ActionValue subscriber, InputType input, int priority = 0){
            switch (input){
                case InputType.Turn: Subscribe<ActionValue>(_onTurn, subscriber, priority); break;
                case InputType.Tilt: Subscribe<ActionValue>(_onTilt, subscriber, priority); break;
                case InputType.Roll: Subscribe<ActionValue>(_onRoll, subscriber, priority); break;
                case InputType.Lean: Subscribe<ActionValue>(_onLean, subscriber, priority); break;
                case InputType.Jump: Subscribe<ActionValue>(_onJump, subscriber, priority); break;
                case InputType.Zoom: Subscribe<ActionValue>(_onZoom, subscriber, priority); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribeFloat(ActionValue subscriber, InputType input){
            switch (input){
                case InputType.Turn: Unsubscribe<ActionValue>(_onTurn, subscriber); break;
                case InputType.Tilt: Unsubscribe<ActionValue>(_onTilt, subscriber); break;
                case InputType.Roll: Unsubscribe<ActionValue>(_onRoll, subscriber); break;
                case InputType.Lean: Unsubscribe<ActionValue>(_onLean, subscriber); break;
                case InputType.Jump: Unsubscribe<ActionValue>(_onJump, subscriber); break;
                case InputType.Zoom: Unsubscribe<ActionValue>(_onZoom, subscriber); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }


        /// <summary>
        /// Subscribes to the input manager
        /// <br>Output: Integer direction or index</br>
        /// </summary>
        public void SubscribeInt(ActionIndex subscriber, InputType input, int priority = 0){
            switch (input){
                case InputType.Switch: Subscribe<ActionIndex>(_onSwitch, subscriber, priority); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribeInt(ActionIndex subscriber, InputType input){
            switch (input){
                case InputType.Switch: Unsubscribe<ActionIndex>(_onSwitch, subscriber); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }


        /// <summary>
        /// Subscribes to the input manager
        /// <br>Output: Bool state</br>
        /// </summary>
        public void SubscribeBool(ActionState subscriber, InputType input, int priority = 0){
            switch (input){
                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribeBool(ActionState subscriber, InputType input){
            switch (input){
                default: MissingAction(subscriber.GetType(), input); break;
            }
        }


        /// <summary>
        /// Subscribes to the input manager
        /// </summary>
        public void SubscribeKey(ActionKey subscriber, InputType input, int priority = 0){
            switch (input){
                case InputType.Use:       Subscribe<ActionKey>(_onUse, subscriber);       break;
                case InputType.Primary:   Subscribe<ActionKey>(_onPrimary, subscriber);   break;
                case InputType.Secondary: Subscribe<ActionKey>(_onSecondary, subscriber); break;

                case InputType.Character: Subscribe<ActionKey>(_onCharacter, subscriber); break;
                case InputType.Inventory: Subscribe<ActionKey>(_onInventory, subscriber); break;
                case InputType.Skills:    Subscribe<ActionKey>(_onSkills, subscriber);    break;
                case InputType.Spells:    Subscribe<ActionKey>(_onSpells, subscriber);    break;
                case InputType.Journal:   Subscribe<ActionKey>(_onJournal, subscriber);   break;
                case InputType.Map:       Subscribe<ActionKey>(_onMap, subscriber);       break;

                case InputType.Crouch: Subscribe<ActionKey>(_onCrouch, subscriber); break;
                case InputType.Sneak:  Subscribe<ActionKey>(_onSneak, subscriber);  break;
                case InputType.Walk:   Subscribe<ActionKey>(_onWalk, subscriber);   break;
                case InputType.Run: Subscribe<ActionKey>(_onRun, subscriber); break;
                case InputType.Dodge:  Subscribe<ActionKey>(_onDodge, subscriber);  break;

                case InputType.Pause: Subscribe<ActionKey>(_onPause, subscriber); break;

                default: MissingAction(subscriber.GetType(), input); break;
            }
        }

        /// <summary>
        /// Unsubscribes from the input manager
        /// </summary>
        public void UnsubscribeKey(ActionKey subscriber, InputType input){
            switch (input){
                case InputType.Use:       Unsubscribe<ActionKey>(_onUse, subscriber);       break;
                case InputType.Primary:   Unsubscribe<ActionKey>(_onPrimary, subscriber);   break;
                case InputType.Secondary: Unsubscribe<ActionKey>(_onSecondary, subscriber); break;

                case InputType.Character: Unsubscribe<ActionKey>(_onCharacter, subscriber); break;
                case InputType.Inventory: Unsubscribe<ActionKey>(_onInventory, subscriber); break;
                case InputType.Skills:    Unsubscribe<ActionKey>(_onSkills, subscriber);    break;
                case InputType.Spells:    Unsubscribe<ActionKey>(_onSpells, subscriber);    break;
                case InputType.Journal:   Unsubscribe<ActionKey>(_onJournal, subscriber);   break;
                case InputType.Map:       Unsubscribe<ActionKey>(_onMap, subscriber);       break;

                case InputType.Crouch: Unsubscribe<ActionKey>(_onCrouch, subscriber); break;
                case InputType.Sneak:  Unsubscribe<ActionKey>(_onSneak, subscriber);  break;
                case InputType.Walk:   Unsubscribe<ActionKey>(_onWalk, subscriber);   break;
                case InputType.Run: Unsubscribe<ActionKey>(_onRun, subscriber); break;
                case InputType.Dodge:  Unsubscribe<ActionKey>(_onDodge, subscriber);  break;

                case InputType.Pause: Unsubscribe<ActionKey>(_onPause, subscriber); break;

                default: MissingAction(subscriber.GetType(), input);       break;
            }
        }


        /// <summary>
        /// Subscribes to input
        /// </summary>
        private void Subscribe<T>(Subscription<T> subscription, T subscriber, int priority = 0) where T : System.Delegate {
            if (subscription == null || subscriber == null) return;
            subscription.AddSubscriber(subscriber, priority);
        }

        /// <summary>
        /// Unsubscribes from input
        /// </summary>
        private void Unsubscribe<T>(Subscription<T> subscription, T subscriber) where T : System.Delegate {
            if (subscription == null || subscriber == null) return;
            subscription.RemoveSubscriber(subscriber);
        }

// ERRORS

        /// <summary>
        /// Logs a missing input implementation error to the console
        /// </summary>
        private void MissingAction(System.Type subscriber, InputType input){
            Debug.LogError(new System.MissingFieldException(
                $"InputType.{input} does not have an implementation for {subscriber}"
            ));
        }
    }
}