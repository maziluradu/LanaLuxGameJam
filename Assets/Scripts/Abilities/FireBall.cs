using UnityEngine;

public class FireBall : ElementalBall
{
    public GameObject fireEffect;
    public FireMark markEffect;

    [Header("Read only")]
    public bool hitFireWall = false;

    public FireBall() : base()
    {
        OnPreDmg += HandlePreDmg;
        OnPostDmg += HandlePostDmg;
    }
    ~FireBall()
    {
        OnPreDmg -= HandlePreDmg;
        OnPostDmg -= HandlePostDmg;
    }

    protected override void HandleWindWallHit(WindWall wall)
    {
        wall.TurnIntoFireCombo();
    }
    protected override void HandleEarthWallHit(EarthWall wall)
    { }
    protected override void HandleFireWallHit(FireWall wall)
    {
        hitFireWall = true;
    }
    protected override void HandleIceWallHit(IceWall wall)
    { }

    protected void HandlePreDmg(CombatUnit unit)
    {
        var mark = unit.GetComponent<FireMark>();
        if (mark == null)
            markEffect.CopyToUnit(unit);
        else
        {
            mark.Spread();
        }
    }
    protected void HandlePostDmg(CombatUnit unit)
    {
        if (hitFireWall)
            Instantiate(fireEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
