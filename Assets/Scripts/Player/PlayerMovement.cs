using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Joystick joystick;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;

        if (joystick == null) Debug.LogError("Joystick non assigné !");
        if (cameraTransform == null) Debug.LogError("CameraTransform non assigné !");
    }

    private Vector3 velocity;
    public float gravity = -9.81f;

    void Update()
    {
        if (joystick == null || cameraTransform == null) return;

        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        direction = cameraTransform.TransformDirection(direction);
        direction.y = 0f;

        controller.Move(direction * speed * Time.deltaTime);

        // Gravité
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reset si chute
        if (transform.position.y < -10f)
        {
            controller.enabled = false;
            transform.position = startPosition;
            velocity = Vector3.zero;
            controller.enabled = true;
        }
    }

}