public abstract class DamageModifier
{
    public bool Expired { get; set; } = false;
    public float Duration { get; set; } = float.PositiveInfinity;
    public float Timer { get; set; } = 0f;

    public abstract float Apply(CombatUnit unit, float damage);
    public virtual void UpdateTimers(float deltaTime)
    {
        Timer += deltaTime;
        if (Timer >= Duration)
            Expired = true;
    }
}
