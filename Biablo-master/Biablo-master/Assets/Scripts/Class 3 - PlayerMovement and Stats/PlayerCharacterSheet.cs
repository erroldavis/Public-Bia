using UnityEngine;

public class PlayerCharacterSheet : MonoBehaviour
{
    int level = 1;
    float experience = 0;
    float strength = 15;
    float dexterity = 15;
    float vitality = 15;
    float energy = 15;

    float currentHitpoints = 35;
    float maxHitpoints = 35;
    float currentMana = 35;
    float maxMana = 35;

    int statPointsToSpend = 0;
    int skillPointsToSpend = 0;

    public static PlayerCharacterSheet instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        EventsManager.instance.onExperienceGranted.AddListener(AddExperience);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onExperienceGranted.RemoveListener(AddExperience);
    }
    public int GetLevel()
    {
        return level;
    }
    public float GetExperience()
    {
        return experience;
    }
    public void AddExperience(float amount)
    {
        experience += amount;
        if (experience >= GetExperienceToNextLevel())
            LevelUp();

        EventsManager.instance.onExperienceUpdated.Invoke(experience / GetExperienceToNextLevel());
    }
    float GetExperienceToNextLevel()
    {
        return (float)(100 * level);
    }
    void LevelUp()
    {
        experience -= GetExperienceToNextLevel();
        level++;
        statPointsToSpend += 5;
        skillPointsToSpend++;
        EventsManager.instance.onPlayerLeveledUp.Invoke();
    }

    #region StatGetters
    public float GetStrength()
    {
        return strength;
    }
    public float GetDexterity()
    {
        return dexterity;
    }
    public float GetVitality()
    {
        return vitality;
    }
    public float GetEnergy()
    {
        return energy;
    }
    #endregion

    #region HP/Mana Getters
    public float GetMaxHP()
    {
        return (5 + (2 * vitality));
    }
    public float GetMaxMana()
    {
        return (5 + (2 * energy));
    }
    #endregion

    #region Stat and Skill Getters
    public int GetStatPointsToSpend()
    {
        return statPointsToSpend;
    }
    public int GetSkillPointsToSpend()
    {
        return skillPointsToSpend;
    }
    #endregion

    #region Stat Point Spend
    bool PointSpendSuccessful()
    {
        if (statPointsToSpend <= 0) return false;
        else
        {
            statPointsToSpend--;
            return true;
        }
    }
    public void BuyStrengthPoint()
    {
        if (PointSpendSuccessful()) strength++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyDexterityPoint()
    {
        if (PointSpendSuccessful()) dexterity++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyVitalityPoint()
    {
        if (PointSpendSuccessful()) vitality++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyEnergyPoint()
    {
        if (PointSpendSuccessful()) energy++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    #endregion

    #region Skill Point Spend
    public bool SkillPointSpendSuccessful()
    {
        if (skillPointsToSpend <= 0) return false;
        else
        {
            skillPointsToSpend--;
            return true;
        }
    }
    #endregion
}
