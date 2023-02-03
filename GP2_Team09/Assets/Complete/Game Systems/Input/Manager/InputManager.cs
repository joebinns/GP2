using InputAction = System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>;
using UnityEngine.InputSystem;
using UnityEngine;

namespace GameProject.Inputs
{
    [CreateAssetMenu(fileName = "Input Manager",
     menuName = "Managers/Input")]
    public sealed partial class InputManager : ScriptableObject
    {
// INITIALISATION

        public readonly InputType[] ActionGroup = {InputType.Use, InputType.Primary, InputType.Secondary, InputType.Switch};
        public readonly InputType[] InterfaceGroup = {InputType.Character, InputType.Inventory, InputType.Skills, InputType.Spells, InputType.Journal, InputType.Map};
        public readonly InputType[] MovementGroup = {InputType.Turn, InputType.Tilt, InputType.Roll, InputType.Lean, InputType.Crouch, InputType.Walk, InputType.Move, InputType.Run, InputType.Jump, InputType.Dodge};
        public readonly InputType[] VariousGroup = {InputType.Pause, InputType.Save, InputType.Load, InputType.Screenshot};

        /// <summary>
        /// Initialises the input manager
        /// </summary>
        public void Initialise(){
            InitActions();
            InitInterfaces();
            InitMovement();
            InitOther();
        }

        /// <summary>
        /// Initialises the input action
        /// </summary>
        private void InitialiseInput<T>(ref Subscription<T> subscription,
        InputInfo input, InputAction action) where T : System.Delegate {
            if (input.Input == null || subscription != null) return;
            subscription = new Subscription<T>(input.Sorted);

            if (input.Pressed)  input.Input.action.started   += ctx => action(ctx);
            if (input.Changed)  input.Input.action.performed += ctx => action(ctx);
            if (input.Released) input.Input.action.canceled  += ctx => action(ctx);

            input.Input.action.Enable();
        }

// DEINITIALISATION

        /// <summary>
        /// Deinitialise the input manager
        /// </summary>
        public void OnDestroy(){
            DeinitActions();
            DeinitInterfaces();
            DeinitMovement();
            DeinitOther();
        }

        /// <summary>
        /// Deinitialises the input action
        /// </summary>
        private void DeinitialiseInput<T>(ref Subscription<T> subscription, 
        InputInfo input, InputAction action) where T : System.Delegate {
            if (input.Input == null || subscription == null) return;
            input.Input.action.Disable();

            if (input.Pressed)  input.Input.action.started   -= action;
            if (input.Changed)  input.Input.action.performed -= action;
            if (input.Released) input.Input.action.canceled  -= action;

            subscription = null;
        }

// INPUT STATES

        /// <summary>
        /// Changes the input state to enabled or disabled
        /// </summary>
        public void SetInputState(InputType input, bool enabled){
            switch (input){
                case InputType.Use:      SetInput(_use.Input, enabled);      break;
                case InputType.Primary:  SetInput(_primary.Input, enabled);  break;
                case InputType.Secondary:SetInput(_secondary.Input, enabled);break;
                case InputType.Switch:   SetInput(_switch.Input, enabled);   break;

                case InputType.Character:SetInput(_character.Input, enabled);break;
                case InputType.Inventory:SetInput(_inventory.Input, enabled);break;
                case InputType.Skills:   SetInput(_skills.Input, enabled);   break;
                case InputType.Spells:   SetInput(_spells.Input, enabled);   break;
                case InputType.Journal:  SetInput(_journal.Input, enabled);  break;
                case InputType.Map:      SetInput(_map.Input, enabled);      break;

                case InputType.Turn:  SetInput(_turn.Input, enabled);  break;
                case InputType.Tilt:  SetInput(_tilt.Input, enabled);  break;
                case InputType.Roll:  SetInput(_roll.Input, enabled);  break;
                case InputType.Lean:  SetInput(_lean.Input, enabled);  break;
                case InputType.Crouch:SetInput(_crouch.Input, enabled);break;
                case InputType.Walk:  SetInput(_walk.Input, enabled);  break;
                case InputType.Move:   SetInput(_move.Input, enabled);   break;
                case InputType.Run:SetInput(_run.Input, enabled);break;
                case InputType.Jump:  SetInput(_jump.Input, enabled);  break;
                case InputType.Dodge: SetInput(_dodge.Input, enabled); break;

                case InputType.Pause: SetInput(_pause.Input, enabled); break;
                case InputType.Save: SetInput(_save.Input, enabled);   break;
                case InputType.Load: SetInput(_load.Input, enabled);   break;
                case InputType.Screenshot: SetInput(_screenshot.Input, enabled); break;

                default: MissingType(input); break;
            }
        }

        /// <summary>
        /// Changes the input states to enabled or disabled
        /// </summary>
        public void SetInputStates(InputType[] inputs, bool enabled){
            for (int i = 0; i < inputs.Length; i++){
                SetInputState(inputs[i], enabled);
            }
        }

        /// <summary>
        /// Changes the input's enabled state
        /// </summary>
        private void SetInput(InputActionReference input, bool enabled){
            if (enabled) input?.action.Enable();
            else input?.action.Disable();
        }

// ERRORS

        /// <summary>
        /// Logs a missing input type error to the console
        /// </summary>
        private void MissingType(InputType input){
            Debug.LogError(new System.MissingFieldException(
                $"Missing InputType.{input} in Input Manager"
            ));
        }
    }
}