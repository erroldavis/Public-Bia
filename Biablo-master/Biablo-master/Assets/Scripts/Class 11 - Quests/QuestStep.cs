using UnityEngine;
using System;

[Serializable]
public class QuestStep : MonoBehaviour
{
    [SerializeField] protected string questName = "";
    [SerializeField] protected int questStep = -1;
    [SerializeField] bool initialStep = false;
    [SerializeField] bool lastStep = false;

    [SerializeField] float experienceValue = 0;
    #region Quest Step Auditing and Preparation
    protected bool QuestExistsInQuestManager()
    {
        return (QuestManager.instance.GetQuest(questName) != null);
    }
    public bool QuestIsOnThisStep()
    {
        bool questOnStep = (QuestManager.instance.GetQuest(questName)?.GetCurrentStep() == questStep);
        bool questStartable = (QuestManager.instance.GetQuest(questName) == null && initialStep);
        return (questOnStep || questStartable);
    }
    #endregion

    protected virtual void InitializeQuest()
    {
        if(!QuestExistsInQuestManager())
        {
            QuestManager.instance.AddQuest(questName, questStep + 1);
        }
    }

    public virtual void ProgressQuest()
    {
        if (initialStep) InitializeQuest();
        else if (lastStep) QuestManager.instance.CompleteQuest(questName);
        else
        {
            QuestManager.instance.ProgressQuestToNextStep(questName);
        }
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
    }
}
