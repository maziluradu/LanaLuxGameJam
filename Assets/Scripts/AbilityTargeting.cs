using UnityEngine;

public class AbilityTargeting : MonoBehaviour
{
    public new Camera camera;
    public LayerMask floorLayerMask;
    public float range = 10f;

    [Header("Read only")]
    public bool isTargeting = false;
    public bool usingArrowTarget = false;
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 targetDirection = Vector3.zero;

    private Transform origin = null;

    private void Update()
    {
        CalculateTargeting();
    }

    public void StartPointTargeting(Transform origin, bool quickCast = false)
    {
        this.origin = origin;

        if (isTargeting)
            StopTargeting();

        isTargeting = true;
        usingArrowTarget = false;

        CalculateTargeting();
    }
    public void StartArrowTargeting(Transform origin, bool quickCast = false)
    {
        this.origin = origin;

        if (isTargeting)
            StopTargeting();

        isTargeting = true;
        usingArrowTarget = true;

        CalculateTargeting();
    }
    public void StopTargeting()
    {
        isTargeting = false;
    }

    private void CalculateTargeting()
    {
        if (!isTargeting)
            return;

        var cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, floorLayerMask))
        {
            targetPosition = origin.position + Vector3.ClampMagnitude(hit.point - origin.position, range);
            targetDirection = hit.point - origin.position;
        }
    }
}
