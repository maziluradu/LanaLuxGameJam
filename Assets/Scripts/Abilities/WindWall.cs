using UnityEngine;

public class WindWall : ElementalWall
{
    public GameObject defaultVisual;
    public FireWall fireCombo;
    public IceWall iceCombo;
    public float comboDelay = 0.5f;

    protected bool _isFireCombo = false;
    protected bool _isIceCombo = false;

    public bool IsFireCombo => _isFireCombo;
    public bool IsIceCombo => _isIceCombo;

    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Wind;
    }

    public bool TurnIntoFireCombo()
    {
        if (_isFireCombo || _isIceCombo)
            return false;
        else
        {
            _isFireCombo = true;
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

