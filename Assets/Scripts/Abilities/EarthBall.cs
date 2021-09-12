using UnityEngine;

public class EarthBall : ElementalBall
{
    public FireEarthTrail fireEffect;
    public GameObject iceEffect;
    public GameObject windEffect;
    public GameObject earthEffect;

    public float fireTrailInterval = 0.1f;

    [Header("Read only")]
    [SerializeField] protected bool hitFireWall = false;
    [SerializeField] protected float fireTrailTimer = 0f;

    protected override void Update()
    {
        base.Update();

        if (hitFireWall)
            fireTrailTimer += Time.deltaTime;

        if (fireTrailTimer >= fireTrailInterval)
        {
            fireTrailTimer = 0;
            Instantiate(fireEffect, transform.position, transform.rotation, transform.parent);
        }
    }

    protected override void HandleWindWallHit(WindWall wall)
    { }
    protected override void HandleEarthWallHit(EarthWall wall)
    { }
    protected override void HandleFireWallHit(FireWall wall)
    {
        Instantiate(fireEffect, transform.position, transform.rotation, transform.parent);
        hitFireWall = true;
        fireTrailTimer = 0f;
    }
    protected override void HandleIceWallHit(IceWall wall)
    { }
}
