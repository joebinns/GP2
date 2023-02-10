using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Game;

namespace GameProject.Interactions
{
    public class UIWinLoseInteraction : BaseInteraction
    {
        [SF] private GameManager _game = null;

        public override void Win()  => _game.PlayerWin();
        public override void Lose() => _game.PlayerLose();
    }
}