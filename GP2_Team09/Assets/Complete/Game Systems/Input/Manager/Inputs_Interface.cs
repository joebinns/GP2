using SF = UnityEngine.SerializeField;
using ActionAxis = System.Action<UnityEngine.Vector3>;
using ActionPlane = System.Action<UnityEngine.Vector2>;
using ActionValue = System.Action<float>;
using ActionIndex = System.Action<int>;
using ActionState = System.Action<bool>;
using ActionKey = System.Action;
using UnityEngine.InputSystem;
using UnityEngine;

namespace GameProject.Inputs {
    public sealed partial class InputManager : ScriptableObject
    {
        [Header("INTERFACE INPUTS")]
        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _character = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _inventory = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _skills = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _spells = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _journal = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _map = null;

        private Subscription<ActionKey> _onCharacter = null;
        private Subscription<ActionKey> _onInventory = null;
        private Subscription<ActionKey> _onSkills    = null;
        private Subscription<ActionKey> _onSpells    = null;
        private Subscription<ActionKey> _onJournal   = null;
        private Subscription<ActionKey> _onMap       = null;

// INITIALISATION AND DENITIALISATION

        /// <summary>
        /// Initialises the interface inputs
        /// </summary>
        private void InitInterfaces(){
            InitialiseInput<ActionKey>(ref _onCharacter, _character, OnCharacterInput);
            InitialiseInput<ActionKey>(ref _onInventory, _inventory, OnInventoryInput);
            InitialiseInput<ActionKey>(ref _onSkills, _skills, OnSkillsInput);
            InitialiseInput<ActionKey>(ref _onSpells, _spells, OnSpellsInput);
            InitialiseInput<ActionKey>(ref _onJournal, _journal, OnJournalInput);
            InitialiseInput<ActionKey>(ref _onMap, _map, OnMapInput);
        }

        /// <summary>
        /// Deinitialises the interface inputs
        /// </summary>
        private void DeinitInterfaces(){
            DeinitialiseInput<ActionKey>(ref _onCharacter, _character, OnCharacterInput);
            DeinitialiseInput<ActionKey>(ref _onInventory, _inventory, OnInventoryInput);
            DeinitialiseInput<ActionKey>(ref _onSkills, _skills, OnSkillsInput);
            DeinitialiseInput<ActionKey>(ref _onSpells, _spells, OnSpellsInput);
            DeinitialiseInput<ActionKey>(ref _onJournal, _journal, OnJournalInput);
            DeinitialiseInput<ActionKey>(ref _onMap, _map, OnMapInput);
        }

// INTERFACE INPUTS

        /// <summary>
        /// On character interface input action callback
        /// </summary>
        private void OnCharacterInput(InputAction.CallbackContext context){
            _onCharacter.NotifySubscribers();
        }

        /// <summary>
        /// On inventory interface input action callback
        /// </summary>
        private void OnInventoryInput(InputAction.CallbackContext context){
            _onInventory.NotifySubscribers();
        }

        /// <summary>
        /// On skills interface input action callback
        /// </summary>
        private void OnSkillsInput(InputAction.CallbackContext context){
            _onSkills.NotifySubscribers();
        }

        /// <summary>
        /// On spells interface input action callback
        /// </summary>
        private void OnSpellsInput(InputAction.CallbackContext context){
            _onSpells.NotifySubscribers();
        }

        /// <summary>
        /// On journal interface input action callback
        /// </summary>
        private void OnJournalInput(InputAction.CallbackContext context){
            _onJournal.NotifySubscribers();
        }

        /// <summary>
        /// On map interface input action callback
        /// </summary>
        private void OnMapInput(InputAction.CallbackContext context){
            _onMap.NotifySubscribers();
        }
    }
}