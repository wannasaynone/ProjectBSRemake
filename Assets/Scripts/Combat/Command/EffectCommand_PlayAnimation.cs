using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_PlayAnimation : EffectCommandBase
    {
        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            onCompleted?.Invoke();
        }
    }
}