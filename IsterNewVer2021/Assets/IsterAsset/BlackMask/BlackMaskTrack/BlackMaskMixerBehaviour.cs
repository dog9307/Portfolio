using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

public class BlackMaskMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount();
        float currentAlpha = 0.0f;
        for (int i = 0; i < inputCount; i++)
        {
            ScriptPlayable<BlackMaskBehaviour> playableInput = (ScriptPlayable<BlackMaskBehaviour>)playable.GetInput(i);
            BlackMaskBehaviour input = playableInput.GetBehaviour();

            float inputWeight = playable.GetInputWeight(i);
            currentAlpha += inputWeight * input.targetAlpha;
        }

        BlackMaskBehaviour.currentAlpha = currentAlpha;
    }
}
