using GameProject.Interactions;

namespace GameProject.Environment
{
    public class Door : BaseInteraction
    {
        public override void Open() {
            gameObject.SetActive(false);
        }

        public override void Close() {
            gameObject.SetActive(true);
        }
    }
}
