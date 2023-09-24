using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_RecoverHealth : EffectCommandBase
    {
        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            for (int i = 0; i < processData.targets.Count; i++)
            {
                float rawHealth = KahaGameCore.Combat.Calculator.Calculate(new KahaGameCore.Combat.Calculator.CalculateData
                {
                    caster = processData.caster,
                    target = processData.targets[i],
                    formula = vars[0],
                    useBaseValue = false
                });

                if (rawHealth < 0f)
                    rawHealth = 0f;

                processData.targets[i].Stats.AddBase(Const.HP, Convert.ToInt32(rawHealth));

                // TODO: add animtion info

                onCompleted?.Invoke();
            }
        }
    }
}