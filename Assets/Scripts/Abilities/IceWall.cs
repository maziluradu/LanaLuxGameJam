using UnityEngine;

public class IceWall : ElementalWall
{
    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Ice;
    }
}
