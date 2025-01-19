using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private VirtualJoystick joystick;

    private void Start()
    {
        joystick = Object.FindFirstObjectByType<VirtualJoystick>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(joystick.Horizontal(), joystick.Vertical(), 0f);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}