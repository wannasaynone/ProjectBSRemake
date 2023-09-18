using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_Attack : EffectCommandBase
    {
        private string[] vars;
        private Action onCompleted;
        private Action onForceQuit;

        private int currentActorIndex = 0;

        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            this.vars = vars;
            this.onCompleted = onCompleted;
            this.onForceQuit = onForceQuit;

            currentActorIndex = 0;
            StartNextBeforeAttackStart();
        }

        private void StartNextBeforeAttackStart()
        {
            processData.caster.SkillTrigger.Trigger("", null);
        }
    }
}