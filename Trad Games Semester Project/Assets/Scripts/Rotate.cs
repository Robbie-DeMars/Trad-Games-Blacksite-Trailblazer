using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Rotation speed around each axis (degrees per second)
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    void Update()
    {
        // Rotate the object based on rotationSpeed and deltaTime
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
