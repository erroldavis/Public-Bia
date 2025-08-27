using UnityEngine;

public class EnemyAnimator : BasicAnimator
{
    void Update()
    {
        deltaPos = (transform.position - oldPos);

        if (deltaPos.sqrMagnitude > .001f * Time.deltaTime)
            SetWalking(true);
        else
            SetWalking(false);

        oldPos = transform.position;
    }

    public override void TriggerAttack()
    {
        int random = Random.Range(0, 2);

        switch (random) {

            case 0:
                thisAnimator.SetTrigger("Attack");
                break;
            case 1:
                thisAnimator.SetTrigger("AltAttack");
                break;


        }
    }
}
