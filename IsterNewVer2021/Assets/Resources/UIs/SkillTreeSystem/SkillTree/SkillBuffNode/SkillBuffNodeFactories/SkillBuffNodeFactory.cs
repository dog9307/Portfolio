using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBuffNodeFactory : MonoBehaviour
{
    #region SINGLETON
    static private SkillBuffNodeFactory _instance;
    static public SkillBuffNodeFactory instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<SkillBuffNodeFactory>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "CameraShakeController";
                _instance = container.AddComponent<SkillBuffNodeFactory>();
            }
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(SkillBuffNodeFactory.instance);
    }
    #endregion

    private Dictionary<string, SkillBuffNodeFactoryHelperBase> _helpers = new Dictionary<string, SkillBuffNodeFactoryHelperBase>();

    void Start()
    {
        // active
        _helpers.Add("ActiveRangeAttack", new ActiveRangeAttackHelper());
        _helpers.Add("MagicArrow", new MagicArrowHelper());
        _helpers.Add("ActiveMirage", new ActiveMirageHelper());
        _helpers.Add("ActiveDash", new ActiveDashHelper());
        _helpers.Add("ActiveTimeSlow", new ActiveTimeSlowHelper());
        _helpers.Add("ActiveMarker", new ActiveMarkerHelper());
        _helpers.Add("ActiveCoolTimeDown", new ActiveCoolTimeDownHelper());
        _helpers.Add("ActiveShield", new ActiveShieldHelper());
        _helpers.Add("ActiveGravity", new ActiveGravityHelper());


        // passive
        _helpers.Add("PassiveOtherAttack", new PassiveOtherAttackHelper());
        _helpers.Add("TracingArrow", new TracingArrowHelper());
        _helpers.Add("PassiveMirage", new PassiveMirageHelper());
        _helpers.Add("AdditionalAttack", new AdditionalAttackHelper());
        _helpers.Add("PassiveCounterAttack", new CounterAttackHelper());
        _helpers.Add("PassiveMarker", new PassiveMarkerHelper());
        _helpers.Add("PassiveCoolTimeDown", new PassiveCoolTimeDownHelper());
        _helpers.Add("PassiveRemover", new PassiveRemoverHelper());
        _helpers.Add("PassiveKnockbackIncrease", new KnockbackIncreaseHelper());
        
    }

    public SkillBuffBase CreateSkillBuff(string skillName, int buffId)
    {
        SkillBuffBase temp = null;
        if (_helpers.ContainsKey(skillName))
            temp = _helpers[skillName].CreateBuff(buffId);

        return temp;
    }
}
