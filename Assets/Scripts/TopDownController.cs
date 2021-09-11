using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [Header("Camera")]
    public new Camera camera;
    public Transform cameraTarget;
    public BoxCollider cameraBounds;
    [Header("Character")]
    public CharacterController character;
    public CharacterAnimator animator;

    [Header("Camera Margins")]
    public bool enableCameraMargins = true;
    public float cameraMarginSpeed = 10f;
    public float cameraMarginAcceleration = 10f;
    public float cameraMarginWidth = 10f;

    [Header("Camera offsets")]
    public float cameraHeight = 5f;
    public float cameraDistance = 5f;
    public float cameraAngle = 0f;

    [Header("Character movement")]
    public bool enableCharacterMovement = true;
    public float characterSpeed = 5f;
    public float gravity = 10f;

    private Vector3 actualCameraTarget;

    void Update()
    {
        actualCameraTarget = cameraTarget.position;
        if (cameraBounds != null)
        {
            var actualCameraBounds = Vector3.Scale(cameraBounds.size, cameraBounds.transform.lossyScale);
            actualCameraTarget.x = Mathf.Clamp(
                actualCameraTarget.x,
                cameraBounds.transform.position.x - actualCameraBounds.x,
                cameraBounds.transform.position.x + actualCameraBounds.x
            );
            actualCameraTarget.z = Mathf.Clamp(
                actualCameraTarget.z,
                cameraBounds.transform.position.z - actualCameraBounds.z,
                cameraBounds.transform.position.z + actualCameraBounds.z
            );
        }

        HandleCameraMargins();
        HandleCharacterMovement();

        // +180 so that Vector3.forward matches CameraAngleY == 0
        camera.transform.position = new Vector3(
            actualCameraTarget.x + Mathf.Sin((180 + cameraAngle) * Mathf.Deg2Rad) * cameraDistance,
            actualCameraTarget.y + cameraHeight,
            actualCameraTarget.z + Mathf.Cos((180 + cameraAngle) * Mathf.Deg2Rad) * cameraDistance
        );
        camera.transform.LookAt(actualCameraTarget);
    }

    private void HandleCharacterMovement()
    {
        if (!enableCharacterMovement)
            return;

        var inputDirection = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        ).normalized;

        // MOVEMENT
        var direction = characterSpeed * Time.deltaTime * (Quaternion.Euler(0, cameraAngle, 0) * inputDirection);
        // add gravity
        direction += gravity * Time.deltaTime * Vector3.down;
        // apply movement
        character.Move(direction);

        // ROTATION
        if (inputDirection != Vector3.zero)
        {
            character.transform.eulerAngles = new Vector3(
                character.transform.eulerAngles.x,
                cameraAngle + Mathf.Rad2Deg * Mathf.Atan2(inputDirection.x, inputDirection.z),
                character.transform.eulerAngles.z
            );
        }

        // ANIMATIONS
        var directionXZ = new Vector3(direction.x, 0, direction.z);
        if (directionXZ.magnitude > 0)
            animator.Walk();
        else
            animator.Idle();
    }

    private void HandleCameraMargins()
    {
        if (!enableCameraMargins)
            return;

        var sign = Vector2.zero;
        var direction = Vector3.zero;

        // input
        var cursor = Input.mousePosition;
        var cameraDirection = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        // left
        if (cursor.x <= cameraMarginWidth)
            sign.x = -1;
        // right
        else if (cursor.x >= Screen.width - cameraMarginWidth)
            sign.x = 1;

        // bot
        if (cursor.y <= cameraMarginWidth)
            sign.y = -1;
        // top
        else if (cursor.y >= Screen.height - cameraMarginWidth)
            sign.y = 1;

        // horizontal
        if (sign.x != 0)
        {
            direction.x = cameraMarginSpeed * Time.deltaTime * sign.x;
            // accelerate if moving the cursor outside the screen
            if (Mathf.Sign(cameraDirection.x) == sign.x)
                direction.x += cameraMarginAcceleration * Time.deltaTime * cameraDirection.x;
        }
        // vertical
        if (sign.y != 0)
        {
            direction.z = cameraMarginSpeed * Time.deltaTime * sign.y;
            // accelerate if moving the cursor outside the screen
            if (Mathf.Sign(cameraDirection.y) == sign.y)
                direction.z += cameraMarginAcceleration * Time.deltaTime * cameraDirection.y;
        }

        if (direction != Vector3.zero)
            actualCameraTarget += Quaternion.Euler(0, cameraAngle, 0) * direction;
    }
}
