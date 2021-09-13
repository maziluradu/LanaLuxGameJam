using UnityEngine;

public class FireBall : ElementalBall
{
    public GameObject fireEffect;
    public GameObject iceEffect;
    public GameObject windEffect;
    public GameObject earthEffect;

    [Header("Read only")]
    public bool hitFireWall = false;

    public FireBall() : base()
    {
        OnPostDmg += HandlePostDmg;
    }
    ~FireBall()
    {
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

    protected void HandlePostDmg(CombatUnit unit)
    {
        if (hitFireWall)
            Instantiate(fireEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
