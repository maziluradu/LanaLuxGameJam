using UnityEngine;

public class FireMark : MonoBehaviour
{
    public Vector3 spreadCenter = Vector3.zero;
    public float spreadRadius = 3f;

#if UNITY_EDITOR
    public bool showRange = false;

    private void OnDrawGizmos()
    {
        if (showRange)
            Gizmos.DrawSphere(transform.position + spreadCenter, spreadRadius);
        Gizmos.DrawSphere(transform.position + spreadCenter, 0.5f); // DELETE ME
    }
#endif

    public virtual void CopyToUnit(CombatUnit unit)
    {
        var copy = unit.gameObject.AddComponent<FireMark>();
        copy.spreadCenter = spreadCenter;
        copy.spreadRadius = spreadRadius;
        copy.showRange = showRange;
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
