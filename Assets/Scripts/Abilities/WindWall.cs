using UnityEngine;

public class WindWall : ElementalWall
{
    public GameObject defaultVisual;
    public FireWall fireCombo;
    public IceWall iceCombo;
    public WindWindEffect windCombo;
    public float comboDelay = 0.5f;
    public float pullStrength = 10f;
    public float spinStrength = 360f;

    protected bool _isFireCombo = false;
    protected bool _isIceCombo = false;

    public bool IsFireCombo => _isFireCombo;
    public bool IsIceCombo => _isIceCombo;

    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Wind;
    }
    private void OnTriggerStay(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null)
        {
            unit.transform.position += Time.fixedDeltaTime * pullStrength * (transform.position - unit.transform.position).normalized;
            unit.transform.eulerAngles += Time.fixedDeltaTime * spinStrength * Vector3.up;
        }
    }

    public bool TurnIntoFireCombo()
    {
        if (_isFireCombo || _isIceCombo)
            return false;
        else
        {
            _isFireCombo = true;
            Destroy(windCombo);
            Invoke(nameof(EnableFireCombo), comboDelay);
            return true;
        }
    }
    public bool TurnIntoIceCombo()
    {
        if (_isFireCombo || _isIceCombo)
            return false;
        else
        {
            _isIceCombo = true;
            Destroy(windCombo);
            Invoke(nameof(EnableIceCombo), comboDelay);
            return true;
        }
    }

    private void EnableFireCombo()
    {
        defaultVisual.SetActive(false);
        fireCombo.gameObject.SetActive(true);
    }
    private void EnableIceCombo()
    {
        defaultVisual.SetActive(false);
        iceCombo.gameObject.SetActive(true);
    }
}

