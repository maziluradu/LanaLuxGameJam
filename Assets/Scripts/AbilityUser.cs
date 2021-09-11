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
        CheckAbility(ability1);
        CheckAbility(ability2);
        CheckAbility(ability3);
        CheckAbility(ability4);
    }

    private void CheckAbility(Ability ability)
    {
        if (!ability.onCooldown && Input.GetKeyDown(ability.button))
            ability.Use(this, abilitiesSource.position, targeting);
    }
}
