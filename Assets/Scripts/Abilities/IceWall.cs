using UnityEngine;

public class IceWall : ElementalWall
{
    public GameObject softWallVisual;
    public GameObject hardWallVisual;

    [Header("Read only")]
    [SerializeField] protected bool _isHard = false;

    public bool IsHard
    {
        get => _isHard;
        set
        {
            _isHard = value;

            // change visual
            softWallVisual.SetActive(!_isHard);
            hardWallVisual.SetActive(_isHard);
        }
    }

    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Ice;
        IsHard = _isHard;
    }
}
