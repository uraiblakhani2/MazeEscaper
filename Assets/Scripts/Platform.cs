using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 2.0f;  // Speed of movement
    public float distance = 5.0f;  // Distance to move

    private Vector3 startPosition;  // Starting position of platform

    private void Start()
    {
        // Save starting position of platform
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate how far to move based on distance and speed
        float distanceToMove = Mathf.PingPong(Time.time * speed, distance);

        // Calculate new position for platform
        Vector3 newPosition = startPosition + new Vector3(0.0f, 0.0f, distanceToMove);

        // Move platform to new position
        transform.position = newPosition;
    }
}
