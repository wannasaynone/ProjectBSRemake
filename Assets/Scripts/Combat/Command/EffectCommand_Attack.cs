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

            Start_Caster_BeforeAttackStart();
        }

        private void Start_Caster_BeforeAttackStart()
        {
            currentActorIndex = -1;
            processData.caster.SkillTrigger.Trigger(Const.BeforeAttackStart, Start_NextTarget_BeforeAttackStart);
        }

        private void Start_NextTarget_BeforeAttackStart()
        {
            currentActorIndex++;

            if (currentActorIndex >= processData.targets.Count)
            {
                Start_Caster_OnAttackStarted();
                return;
            }

            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.BeforeAttackStart, Start_NextTarget_BeforeAttackStart);
        }

        private void Start_Caster_OnAttackStarted()
        {
            currentActorIndex = -1;
            processData.caster.SkillTrigger.Trigger(Const.OnAttackStarted, Start_NextTarget_OnAttackStarted);
        }

        private void Start_NextTarget_OnAttackStarted()
        {
            currentActorIndex++;

            if (currentActorIndex >= processData.targets.Count)
            {
                CalculateDamage();
                return;
            }

            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.OnAttackStarted, Start_NextTarget_OnAttackStarted);
        }

        private void CalculateDamage()
        {

        }
    }
}