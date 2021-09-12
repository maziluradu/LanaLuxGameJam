public class WindWall : ElementalWall
{
    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Wind;
    }
}

