using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

public class MirageSetter : StateMachineBehaviour
{
    [SerializeField] GameObject _effect;

    void StartEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(_effect) as GameObject;
        effect.transform.position = pos;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimationClip[] clips = FindObjectOfType<PlayerAnimController>().anim.runtimeAnimatorController.animationClips;
        PlayerAttacker attack = FindObjectOfType<PlayerAttacker>();
        string findKey = "";
        // test
        //if (clips[0].name.Contains("son"))
        //{
        //    if (attack.currentHand == HAND.LEFT)
        //        findKey = "attack";
        //    else
        //        findKey = "attackr";
        //}
        //else
            findKey = "attack";

        MirageController controller = animator.GetComponent<MirageController>();
        Vector2 dir = controller.dir;
        if (dir.magnitude == 0.0f)
            dir = FindObjectOfType<LookAtMouse>().dir;
        animator.SetFloat("dirX", dir.x);
        animator.SetFloat("dirY", dir.y);
        controller.dir = dir;
        controller.currentHand = attack.currentHand;

        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        AnimationClipOverrides clipOverrides = new AnimationClipOverrides(overrideController.overridesCount);
        overrideController.GetOverrides(clipOverrides);
        
        int count = 0;
        foreach (var clip in clips)
        {
            if (clip.name.Contains(findKey))
            {
                if (clip.name.Contains("charging")) continue;

                string name = overrideController.runtimeAnimatorController.animationClips[count].name;
                clipOverrides[name] = clip;
                count++;
                if (count >= clipOverrides.Count) break;
            }
        }
        overrideController.ApplyOverrides(clipOverrides);

        Vector3 pos = animator.transform.position;
        pos.y -= 0.5f;
        //StartEffect(pos);

        animator.SetFloat("attackMultiplier", attack.GetDashMultiplier() * attack.attackSpeedMultiplier);

        //animator.Update(Time.deltaTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 pos = animator.transform.position;
        pos.y -= 0.5f;
        StartEffect(pos);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
