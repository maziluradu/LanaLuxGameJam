public class IceBall : ElementalBall
{
    protected override void HandleWindWallHit(WindWall wall)
    {
        wall.TurnIntoIceCombo();
    }
    protected override void HandleEarthWallHit(EarthWall wall)
    { }
    protected override void HandleFireWallHit(FireWall wall)
    { }
    protected override void HandleIceWallHit(IceWall wall)
    { }
}
