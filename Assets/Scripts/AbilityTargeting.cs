using UnityEngine;

public class AbilityTargeting : MonoBehaviour
{
    public new Camera camera;

    public Transform pointTarget;
    public Transform arrowTarget;
    public LayerMask floorLayerMask;

    [Header("Read only")]
    public bool isTargeting = false;
    public bool usingArrowTarget = false;
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 targetDirection = Vector3.zero;

    private Transform origin = null;
    private Transform currentTarget = null;

    private void Update()
    {
        CalculateTargeting();
    }

    public void StartPointTargeting(bool quickCast = false)
    {
        if (isTargeting)
            StopTargeting();

        isTargeting = true;
        usingArrowTarget = false;
        if (!quickCast)
            currentTarget = Instantiate(pointTarget, gameObject.transform);

        CalculateTargeting();
    }
    public void StartArrowTargeting(Transform origin, bool quickCast = false)
    {
        this.origin = origin;

        if (isTargeting)
            StopTargeting();

        isTargeting = true;
        usingArrowTarget = true;
        if (!quickCast)
            currentTarget = Instantiate(arrowTarget, gameObject.transform);

        CalculateTargeting();
    }
    public void StopTargeting()
    {
        if (isTargeting && currentTarget)
            Destroy(currentTarget.gameObject);

        isTargeting = false;
    }

    private void CalculateTargeting()
    {
        if (!isTargeting)
            return;

        var cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, floorLayerMask))
        {
            targetPosition = hit.point;
            targetDirection = hit.point - origin.position;

            if (currentTarget != null)
            {
                if (usingArrowTarget)
                {
                    var direction = hit.point - origin.position;
                    currentTarget.position = origin.position;
                    currentTarget.eulerAngles = new Vector3(0, 180 + Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z), 0);
                }
                else
                {
                    currentTarget.position = hit.point;
                }
            }
        }
    }
}
