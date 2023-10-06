using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_Select : EffectCommandBase
    {
        private readonly ITargetSelector targetSelector;

        private Action onCompleted;

        public EffectCommand_Select(ITargetSelector targetSelector)
        {
            this.targetSelector = targetSelector;
        }

        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            this.onCompleted = onCompleted;
            targetSelector.StartSimpleSelect(processData.caster, vars, OnSelected);
        }

        private void OnSelected(System.Collections.Generic.List<CombatActor> targets)
        {
            processData.targets.AddRange(targets);
            onCompleted?.Invoke();
        }
    }
}