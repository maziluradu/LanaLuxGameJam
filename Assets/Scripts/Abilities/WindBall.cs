using UnityEngine;

public class WindBall : ElementalBall
{
    public FireWindEffect fireEffect;
    public WindWindEffect windEffect;

    protected override void HandleWindWallHit(WindWall wall)
    {
        var combo = wall.GetComponent<WindWindEffect>();
        if (combo != null)
        {
            combo.Trigger(transform);
            Destroy(gameObject);
        }
    }
    protected override void HandleEarthWallHit(EarthWall wall)
    { }
    protected override void HandleFireWallHit(FireWall wall)
    {
        Instantiate(fireEffect, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
    protected override void HandleIceWallHit(IceWall wall)
    { }
}
