public class IceBall : ElementalBall
{
    public FireIceEffect fireCombo;

    protected override void HandleWindWallHit(WindWall wall)
    {
        wall.TurnIntoIceCombo();
    }
    protected override void HandleEarthWallHit(EarthWall wall)
    { }
    protected override void HandleFireWallHit(FireWall wall)
    {
        Instantiate(fireCombo, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
    protected override void HandleIceWallHit(IceWall wall)
    { }
}
