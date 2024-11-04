using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [Range(1,10)] public float smoothFactor;

    public Vector3 minValue, maxValue;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        //Sets the camera's desired target (the player) and includes the offset of the camera's original position (0,0,-10)
        Vector3 targetPosition = player.position + offset;

        //Sets boundaries for the target position variable to stop the camera from seeing past floors/walls/ceilings that shouldn't be looked past
        Vector3 boundedPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));
        //Smoothens the way the camera follows the desired target
        //Player movements are very sharp so the Lerp function makes up for that and makes it easier on the eyes
        //The higher the smooth factor the faster it snaps to position
        Vector3 smoothedOutcome = Vector3.Lerp(transform.position, boundedPosition, smoothFactor*Time.fixedDeltaTime);
        //Applies the above code
        transform.position = smoothedOutcome;
    }
}
