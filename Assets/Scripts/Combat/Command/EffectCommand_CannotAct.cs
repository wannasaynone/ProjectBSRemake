using System;
using KahaGameCore.Combat.Processor.EffectProcessor;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_CannotAct : EffectCommandBase
    {
        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            for (int i = 0; i < processData.targets.Count; i++)
            {
                processData.targets[i].Stats.Add(Const.CannotAct, 1);
            }

            onCompleted?.Invoke();
        }
    }
}