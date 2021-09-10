using UnityEngine;

public class AbilityTargeting : MonoBehaviour
{
    public new Camera camera;
    public Transform character;

    public Transform pointTarget;
    public Transform arrowTarget;
    public LayerMask floorLayerMask;

    [Header("Read only")]
    public bool isTargeting = false;
    public bool usingArrowTarget = false;
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 targetDirection = Vector3.zero;

    private void Start()
    {
        pointTarget.gameObject.SetActive(false);
        arrowTarget.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isTargeting)
            return;

        var cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, floorLayerMask))
        {
            targetPosition = hit.point;
            targetDirection = hit.point - character.position;

            if (usingArrowTarget)
            {
                var direction = hit.point - character.position;
                arrowTarget.position = character.position;
                arrowTarget.eulerAngles = new Vector3(0, 180 + Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z), 0);
            }
            else
            {
                pointTarget.position = hit.point;
            }
        }
    }

    public void StartPointTargeting()
    {
        isTargeting = true;
        usingArrowTarget = false;
        pointTarget.gameObject.SetActive(true);
    }
    public void StartArrowTargeting()
    {
        isTargeting = true;
        usingArrowTarget = true;
        arrowTarget.gameObject.SetActive(true);
    }
    public void StopTargeting()
    {
        isTargeting = false;
        if (usingArrowTarget)
            arrowTarget.gameObject.SetActive(false);
        else
            pointTarget.gameObject.SetActive(false);
    }
}
