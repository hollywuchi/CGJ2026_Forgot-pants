using UnityEngine;
using DG.Tweening;

namespace CGJ.Transition
{
    public class Teleport : MonoBehaviour
    {
        public string sceneToGo;
        public Vector3 positionToGo;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                EventHandler.CallTransitionEvent(sceneToGo,positionToGo);
            }
        }

        public void TransitionToScene()
        {
            EventHandler.CallTransitionEvent(sceneToGo, positionToGo);
        }

        public void OpenUI()
        {
            UIManager.Instance.DoorPanel.SetActive(true);

            DOVirtual.DelayedCall(5f, () =>
            {
                if (UIManager.Instance.DoorPanel != null)
                {
                    UIManager.Instance.DoorPanel.SetActive(false);
                }
            });
        }
    }
}