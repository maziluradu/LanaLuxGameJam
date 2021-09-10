using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public new Camera camera;
    public Transform cameraTarget;

    [Header("Camera Margins")]
    public bool enableCameraMargins = true;
    public float cameraMarginSpeed = 10f;
    public float cameraMarginAcceleration = 10f;
    public float cameraMarginWidth = 10f;

    [Header("Camera offsets")]
    public float cameraHeight = 5f;
    public float cameraDistance = 5f;
    public float cameraAngle = 0f;

    void Update()
    {
        HandleCameraMargins();

        // +180 so that Vector3.forward matches CameraAngleY == 0
        camera.transform.position = new Vector3(
            cameraTarget.position.x + Mathf.Sin((180 + cameraAngle) * Mathf.Deg2Rad) * cameraDistance,
            cameraHeight,
            cameraTarget.position.z + Mathf.Cos((180 + cameraAngle) * Mathf.Deg2Rad) * cameraDistance
        );
        camera.transform.LookAt(cameraTarget);
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
            cameraTarget.position += Quaternion.Euler(0, cameraAngle, 0) * direction;
    }
}
