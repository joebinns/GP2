using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Actions
{
	[DefaultExecutionOrder(-1)]
	public abstract class BaseController : MonoPriority
	{
		[SF] protected UpdateManager update = null;

		protected bool _updating	  = false;
		protected bool _lateUpdating  = false;
		protected bool _fixedUpdating = false;

		protected List<BaseAction> _updateList		= null;
		protected List<BaseAction> _lateUpdateList  = null;
		protected List<BaseAction> _fixedUpdateList = null;

// INITIALIZATION

		/// <summary>
		/// Initializes update lists
		/// </summary>
		protected virtual void Awake(){
			_updateList		 = new List<BaseAction>();
			_fixedUpdateList = new List<BaseAction>();
			_lateUpdateList  = new List<BaseAction>();
		}

		/// <summary>
		/// Enables update callbacks
		/// </summary>
		protected virtual void OnEnable(){
			SetAllUpdates(true);
		}

		/// <summary>
		/// Disables update callbacks
		/// </summary>
		protected virtual void OnDisable(){
			SetAllUpdates(false);
			StopActions(_updateList);
			StopActions(_lateUpdateList);
			StopActions(_fixedUpdateList);
		}

// ACTION HANDLING

        /// <summary>
        /// Adds the action to the controller
        /// </summary>
        public void AddAction(BaseAction action, UpdateMode mode){
            if (mode == UpdateMode.Update){
				if (!_updateList.Contains(action)){ 
					_updateList.Add(action);
					_updateList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
				}
				if (!_updating && _updateList.Count == 1)
					SetUpdate(true);

			} else if (mode == UpdateMode.LateUpdate){
				if (!_lateUpdateList.Contains(action)){
					_lateUpdateList.Add(action);
					_lateUpdateList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
				}
				if (!_lateUpdating && _lateUpdateList.Count == 1)
					SetLateUpdate(true);

			} else if (mode == UpdateMode.FixedUpdate){
				if (!_fixedUpdateList.Contains(action)){ 
					_fixedUpdateList.Add(action);
					_fixedUpdateList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
				}
				if (!_fixedUpdating && _fixedUpdateList.Count == 1)
					SetFixedUpdate(true);
			}
        }

		/// <summary>
		/// Removes the action from the controller
		/// </summary>
		public void RemoveAction(BaseAction action, UpdateMode mode){
			if (mode == UpdateMode.Update){
				int i = _updateList.IndexOf(action);
				if (i >= 0) _updateList.RemoveAt(i);

				if (_updating && _updateList.Count == 0)
					SetUpdate(false);

			} else if (mode == UpdateMode.LateUpdate){
				int i = _lateUpdateList.IndexOf(action);
				if (i >= 0) _lateUpdateList.RemoveAt(i);

				if (_lateUpdating && _lateUpdateList.Count == 0)
					SetLateUpdate(false);

			} else if (mode == UpdateMode.FixedUpdate){
				int i = _fixedUpdateList.IndexOf(action);
				if (i >= 0) _fixedUpdateList.RemoveAt(i);

				if (_fixedUpdating && _fixedUpdateList.Count == 0)
					SetFixedUpdate(false);
			}
        }

		/// <summary>
		/// Calls the actions in the action list
		/// </summary>
		protected void UpdateActions(List<BaseAction> actions, float deltaTime){
            for (int i = actions.Count - 1; i >= 0; i--){
				var action = actions[i];
				
				if (action.OnCheck()){
					if (!action.Active)
						action.OnEnter();

					action.OnUpdate(deltaTime);

                } else if (action.Active){
					action.OnExit();
                }
            }
        }

		/// <summary>
		/// Stops the actions in the action list
		/// </summary>
		protected void StopActions(List<BaseAction> actions){
			for (int i = actions.Count - 1; i >= 0; i--){
				var action = actions[i];

				if (action.Active) 
					action.OnExit();
            }
        }

// UPDATE HANDLING

		/// <summary>
		/// Updates actions on update callback
		/// </summary>
		protected virtual void OnUpdate(float deltaTime){
			UpdateActions(_updateList, deltaTime);
		}

		/// <summary>
		/// Updates actions on late update callback
		/// </summary>
		protected virtual void OnLateUpdate(float deltaTime){
			UpdateActions(_lateUpdateList, deltaTime);
		}

		/// <summary>
		/// Updates actions on fixed update callback
		/// </summary>
		protected virtual void OnFixedUpdate(float deltaTime){
			UpdateActions(_fixedUpdateList, deltaTime);
		}


		/// <summary>
		/// Sets the update callback
		/// </summary>
		protected void SetUpdate(bool enabled){
			if (enabled && !_updating)
				update.Subscribe(OnUpdate, UpdateType.Update, Priority);

			else if (!enabled && _updating)
				update.Unsubscribe(OnUpdate, UpdateType.Update);

			_updating = enabled;
		}

		/// <summary>
		/// Sets the late update callback
		/// </summary>
		protected void SetLateUpdate(bool enabled){
			if (enabled && !_lateUpdating)
                update.Subscribe(OnLateUpdate, UpdateType.LateUpdate, Priority);

			else if (!enabled && _lateUpdating)
                update.Unsubscribe(OnLateUpdate, UpdateType.LateUpdate);

            _lateUpdating = enabled;
		}

		/// <summary>
		/// Sets the fixed update callback
		/// </summary>
		protected void SetFixedUpdate(bool enabled){
			if (enabled && !_fixedUpdating)
                update.Subscribe(OnFixedUpdate, UpdateType.FixedUpdate, Priority);

            else if (!enabled && _fixedUpdating)
				update.Unsubscribe(OnFixedUpdate, UpdateType.FixedUpdate);

            _fixedUpdating = enabled;
		}

		/// <summary>
		/// Sets all updates based on current state
		/// </summary>
		protected void SetAllUpdates(bool enabled){
			if (enabled){
				if (_updating) update.Subscribe(OnUpdate, UpdateType.Update, Priority);
                if (_lateUpdating) update.Subscribe(OnLateUpdate, UpdateType.LateUpdate, Priority);
                if (_fixedUpdating) update.Subscribe(OnFixedUpdate, UpdateType.FixedUpdate, Priority);

            } else {
				if (_updating) update.Unsubscribe(OnUpdate, UpdateType.Update);
                if (_lateUpdating) update.Unsubscribe(OnLateUpdate, UpdateType.LateUpdate);
                if (_fixedUpdating) update.Unsubscribe(OnFixedUpdate, UpdateType.FixedUpdate);
            }
		}
	}
}