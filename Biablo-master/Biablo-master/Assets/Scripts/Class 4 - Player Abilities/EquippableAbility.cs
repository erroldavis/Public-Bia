using UnityEngine;

public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;

    protected virtual  void Update()
    {
        
        if (targetedReceiver != null) RunTargetAttack();
    }
    protected virtual void RunTargetAttack()
    {

        if (MyTargetInRange())
        {
            StandStill();
            LookAtMyTarget();
            SpawnEquippedAttack(myPlayer.transform.position + myPlayer.transform.forward);
            PlayAttackAnimation();
            targetedReceiver = null;
        }
        else
        {
            ChaseMyTarget();
        }
    }

    private bool MyTargetInRange()
    {
        return Vector3.Distance(myPlayer.transform.position, targetedReceiver.transform.position) <= attackRange;
    }

    protected void ChaseMyTarget()
    {
        myPlayer.Movement().MoveToLocation(targetedReceiver.transform.position);
    }

    protected void PlayAttackAnimation()
    {
        myPlayer.GetAnimator().TriggerAttack();
    }

    protected void StandStill()
    {
        myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
    }

    protected void LookAtMyTarget()
    {
        myPlayer.transform.LookAt(
                                                new Vector3(targetedReceiver.transform.position.x,
                                                            myPlayer.transform.position.y,
                                                            targetedReceiver.transform.position.z));
    }

    public virtual void RunAbilityClicked(PlayerController player)
    {
        if (MouseIsOverUI()) return;

        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out hit))
        {
            SuccessfulRaycastFunctionality(ref hit);
        }
    }

    protected virtual void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        myPlayer.Movement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<Clickable>())
        {
            targetedReceiver = hit.collider.GetComponent<CombatReceiver>();
        }
    }

    protected virtual void SpawnEquippedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());

        float critMod = 1;
        int random = Random.Range(0, 100);
        float playerDex = PlayerCharacterSheet.instance.GetDexterity();
        if (random < playerDex) critMod = 2;

        float playerStrength = PlayerCharacterSheet.instance.GetStrength();
        float calculatedDamage = (playerStrength / 3) * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }

    public bool MouseIsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public override void LevelUp()
    {
        if(PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
            skillLevel++;
    }
}

