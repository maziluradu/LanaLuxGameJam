using UnityEngine;

public class WindBall : ElementalBall
{
    public FireWindEffect fireEffect;
    public GameObject iceEffect;
    public GameObject windEffect;
    public GameObject earthEffect;

    protected override void HandleWindWallHit(WindWall wall)
    { }
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
