using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    public GameObject character;
    public Transform abilitiesSource;
    public AbilityTargeting targeting;

    [Header("Abilities")]
    public ProjectileAbility ability1;
    public HoldProjectileAbility ability2;
    public ProjectileAbility ability3;
    public ProjectileAbility ability4;

    void Update()
    {
        HandleAbility(ability1);
        HandleAbility(ability2);
        HandleAbility(ability3);
        HandleAbility(ability4);
    }

    private void HandleAbility(Ability ability)
    {
        ability.UpdateTimers(Time.deltaTime);

        if (!ability.onCooldown)
        {
            if (Input.GetKeyDown(ability.button))
                ability.Press(this, targeting);
            else if (Input.GetKey(ability.button))
                ability.Hold(this, targeting);
            else if (Input.GetKeyUp(ability.button))
                ability.Release(this, targeting);
        }
    }
}
