using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using GameProject.Interactions;

namespace GameProject
{
    public class AutoStartInteraction : BaseInteraction
    {
        [SF] private List<ActionInfo> _onStart = null;

        void Start() => Interact(_onStart);
    }
}