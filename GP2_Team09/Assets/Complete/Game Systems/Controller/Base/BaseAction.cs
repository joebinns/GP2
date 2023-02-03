using UnityEngine;

namespace GameProject.Actions {
	public abstract class BaseAction : MonoPriority
	{
		protected bool _active = false;

// GETTERS & SETTERS

		/// <summary>
		/// If the action is running
		/// </summary>
		public bool Active => _active;

// ACTION HANDLING

		/// <summary>
		/// On conditions check
		/// </summary>
		public virtual bool OnCheck() => false;

		/// <summary>
		/// Set active on action entered
		/// </summary>
		public virtual void OnEnter() => _active = true;

		/// <summary>
		/// Set inactive on action exited
		/// </summary>
		public virtual void OnExit()  => _active = false;

		/// <summary>
		/// On action updated
		/// </summary>
		public virtual void OnUpdate(float deltaTime){}
	}
}