public class EarthWall : ElementalWall
{
    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Earth;
    }
}
