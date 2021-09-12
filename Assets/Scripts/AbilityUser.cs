using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    public GameObject character;
    public Transform abilitiesSource;
    public AbilityTargeting targeting;

    [Header("Abilities")]
    public WallSpell wallSpell = new WallSpell();
    public FireBallSpell fireBallSpell = new FireBallSpell();
    public IceBallSpell iceBallSpell = new IceBallSpell();
    public WindBallSpell windBallSpell = new WindBallSpell();

    [SerializeField] private ElementalType _lastElementalType;

    public ElementalType LastElementalType
    {
        get => _lastElementalType;
        protected set => _lastElementalType = value;
    }

    void Update()
    {
        HandleWall(wallSpell);
        HandleElementalBall(fireBallSpell);
        HandleElementalBall(iceBallSpell);
        HandleElementalBall(windBallSpell);
    }
    private void OnGUI()
    {
        GUILayout.Button(LastElementalType.ToString());
    }

    private void HandleWall(WallSpell ability)
    {
        ability.UpdateTimers(Time.deltaTime);

        if (!ability.onCooldown)
        {
            if (Input.GetKeyDown(ability.button))
            {
                ability.Press(this, targeting);
            }
        }
    }
    private void HandleElementalBall(ElementalBallSpell ability)
    {
        ability.UpdateTimers(Time.deltaTime);

        if (!ability.onCooldown)
        {
            if (Input.GetKeyDown(ability.button))
            {
                ability.Press(this, targeting);
                LastElementalType = ability.ElementalType;
            }
        }
    }
}
