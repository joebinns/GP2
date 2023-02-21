using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.HUD;

namespace GameProject.Interactions
{
    public class UITimerInteraction : BaseInteraction
    {
        [SF] private CrewHUDController _hud = null;

// TIMER HANDLING

        public override void Begin() => _hud.StartTimer();
        public override void End()   => _hud.StopTimer();
    }
}