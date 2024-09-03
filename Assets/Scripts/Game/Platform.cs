using UnityEngine;

namespace Arkanoid.Game
{
    public class Platform : MonoBehaviour
    {
        #region Unity lifecycle

        private void Update()
        {
            MoveWithMouse();
        }

        #endregion

        #region Private methods

        private void MoveWithMouse()
        {
            Vector3 mousePosition = Input.mousePosition; // Get the mouse position in pixels
            mousePosition.z = Camera.main.nearClipPlane; // Set the z coordinate to the near clipping plane of the camera
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 currentPosition = transform.position;
            currentPosition.x = worldPosition.x; // Update x position only
            transform.position = currentPosition; // Set the new position of the platform
            //transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
        }

        #endregion
    }
}