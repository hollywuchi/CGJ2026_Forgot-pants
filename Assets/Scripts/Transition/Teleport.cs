using UnityEngine;
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
                // EventHandler.CallTransitionEvent(sceneToGo,positionToGo);
            }
        }
    }
}