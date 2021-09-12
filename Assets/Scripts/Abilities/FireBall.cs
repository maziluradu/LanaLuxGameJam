using UnityEngine;

public class FireBall : Projectile
{
    public GameObject fireEffect;
    public GameObject iceEffect;
    public GameObject windEffect;
    public GameObject earthEffect;

    [Header("Read only")]
    public bool hitFireWall;

    public FireBall()
    {
        OnWallHit += HandleWallHit;
        OnPostDmg += HandlePostDmg;
    }
    ~FireBall()
    {
        OnWallHit -= HandleWallHit;
        OnPostDmg -= HandlePostDmg;
    }

    private void HandleWallHit(ElementalWall wall)
    {
        switch (wall.ElementalType)
        {
            case ElementalType.Fire:
                hitFireWall = true;
                break;
            case ElementalType.Ice:
                break;
            case ElementalType.Wind:
                break;
            case ElementalType.Earth:
                break;
            default:
                break;
        }
    }
    private void HandlePostDmg(CombatUnit unit)
    {
        if (hitFireWall)
            Instantiate(fireEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
