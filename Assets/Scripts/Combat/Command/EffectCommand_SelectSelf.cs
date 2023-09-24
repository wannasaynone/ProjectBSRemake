using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_SelectSelf : EffectCommandBase
    {
        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            processData.targets.Add(processData.caster);
            onCompleted?.Invoke();
        }
    }
}