using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sk : SkeletonAI
{
    protected override void SpawnAttackPrefab()
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 spawnPosition = (attackDirection.normalized) + transform.position + Vector3.up;

        GameObject newAttack = Instantiate(attackprefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);
        newAttack.GetComponent<CombatActor>().InitializeDamage(damage);
        newAttack.GetComponent<FireballCA>().SetShootDirection(attackDirection.normalized);
    }
}
