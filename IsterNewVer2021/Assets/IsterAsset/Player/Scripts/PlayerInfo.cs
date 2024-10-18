using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    public float damage { get; set; }
    
    public int skillPoint { get; set; }

    public int attackFigure { get; set; }

    void Start()
    {
        ChangeCharacter(this);

        damage = 10.0f;

        // test
        skillPoint = 100;
    }

    void ChangeComponent()
    {
        System.Type newComponent = null;
        if (_characterName.Equals("son"))
        {
            // 쌍검 조건 추가
            if (false)
                newComponent = typeof(DualSword);
        }
        //else if (_characterName.Equals("smith"))
        //    newComponent = typeof(RageGauge);
        //else if (_characterName.Equals("alchemist"))
        //    newComponent = typeof(UpgradeElement);
        //else if (_characterName.Equals("trader"))
        //    newComponent = typeof(InfiniteInventory);
        //else if (_characterName.Equals("priest"))
        //    newComponent = typeof(ChangeSkillKind);

        PlayerComponent prev = GetComponent<PlayerComponent>();
        if (prev)
            prev.ChangeCharacter(newComponent);
        else
        {
            if (newComponent != null)
                gameObject.AddComponent(newComponent);
        }
    }

    public void ChangeCharacter(CharacterInfo info)
    {
        PlayableCharacterManager pm = FindObjectOfType<PlayableCharacterManager>();
        if (pm)
            pm.CharacterChange(this, info);

        _controller = info.animController;
        ChangeName();
        ChangeComponent();

        PlayerAnimController playerAnim = GetComponent<PlayerAnimController>();
        if (playerAnim)
            playerAnim.ChangeCharacter(_controller);
    }
}
