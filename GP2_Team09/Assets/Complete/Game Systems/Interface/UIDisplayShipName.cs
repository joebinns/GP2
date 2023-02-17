using SF = UnityEngine.SerializeField;
using UnityEngine.UI;
using UnityEngine;
using GameProject.Game;

namespace GameProject.Interface
{
    public class UIDisplayShipName : MonoBehaviour
    {
        [SF] private Text _text = null;
        [SF] private GameManager _game = null;

        private void Start() => _text.text = _game.ShipName;
    }
}