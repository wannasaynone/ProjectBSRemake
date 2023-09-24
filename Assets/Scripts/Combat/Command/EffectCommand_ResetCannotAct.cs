using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_ResetCannotAct : EffectCommandBase
    {
        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            for (int i = 0; i < processData.targets.Count; i++)
            {
                processData.targets[i].Stats.AddBase(Const.CannotAct, -1);
                if (processData.targets[i].Stats.GetTotal(Const.CannotAct, false) < 0)
                {
                    processData.targets[i].Stats.SetBase(Const.CannotAct, 0);
                }
            }

            onCompleted?.Invoke();
        }
    }
}