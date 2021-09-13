using UnityEngine;

public class FireMark : MonoBehaviour
{
    public Vector3 spreadCenter = Vector3.zero;
    public float spreadRadius = 3f;

    [ColorUsage(true, hdr: true)]
    public Color fresnelColor = Color.red;
    public float fresnelPower = 1f;

    private Material material;
    private Color oldFresnelColor;
    private float oldFresnelPower;

    private readonly string fresnelPowerId = "Vector1_b87927ef809d4fe4a6fb4d2b7ecd442c";
    private readonly string fresnelColorId = "Color_fc1a8ee52f504c528a9e999e53a52ae6";

    private void OnEnable()
    {
        material = transform.GetChild(0).GetComponent<Renderer>().material;
        oldFresnelPower = material.GetFloat(fresnelPowerId);
        oldFresnelColor = material.GetColor(fresnelColorId);

        material.SetFloat(fresnelPowerId, fresnelPower);
        material.SetColor(fresnelColorId, fresnelColor);
    }
    private void OnDisable()
    {
        material.SetFloat(fresnelPowerId, oldFresnelPower);
        material.SetColor(fresnelColorId, oldFresnelColor);
    }

    public virtual void CopyToUnit(CombatUnit unit)
    {
        if (unit.isPlayer)
            return;

        var copy = unit.gameObject.AddComponent<FireMark>();
        copy.spreadCenter = spreadCenter;
        copy.spreadRadius = spreadRadius;
    }

    public virtual void Spread()
    {
        var hits = Physics.OverlapSphere(transform.position + spreadCenter, spreadRadius);
        if (hits != null && hits.Length > 0)
        {
            var myUnit = GetComponent<CombatUnit>();

            foreach (var hit in hits)
            {
                var unit = hit.GetComponent<CombatUnit>();
                if (unit != null && unit != myUnit)
                    CopyToUnit(unit);
            }
        }
    }
}
