using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    public GameObject character;
    public Transform abilitiesSource;
    public AbilityTargeting targeting;

    [Header("Abilities")]
    public ProjectileAbility ability1;
    public ProjectileAbility ability2;
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
        ability.UpdateCooldown(Time.deltaTime);

        if (!ability.onCooldown && Input.GetKeyDown(ability.button))
            ability.Use(this, abilitiesSource.position, targeting);
    }
}
