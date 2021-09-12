using UnityEngine;

public abstract class ElementalBall : Projectile
{
    public ElementalBall()
    {
        OnWallHit += HandleWallHit;
    }
    ~ElementalBall()
    {
        OnWallHit -= HandleWallHit;
    }

    protected virtual void HandleWallHit(ElementalWall wall)
    {
        switch (wall.ElementalType)
        {
            case ElementalType.Fire:
                HandleFireWallHit((FireWall)wall);
                break;
            case ElementalType.Ice:
                HandleIceWallHit((IceWall)wall);
                break;
            case ElementalType.Wind:
                HandleWindWallHit((WindWall)wall);
                break;
            case ElementalType.Earth:
                HandleEarthWallHit((EarthWall)wall);
                break;
            default:
                break;
        }
    }

    protected abstract void HandleFireWallHit(FireWall wall);
    protected abstract void HandleIceWallHit(IceWall wall);
    protected abstract void HandleWindWallHit(WindWall wall);
    protected abstract void HandleEarthWallHit(EarthWall wall);
}
