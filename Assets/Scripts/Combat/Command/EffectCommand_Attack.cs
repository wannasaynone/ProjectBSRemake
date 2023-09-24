using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat.Command
{
    public class EffectCommand_Attack : EffectCommandBase
    {
        private string[] vars;
        private Action onCompleted;

        private int currentActorIndex = 0;

        private readonly static System.Collections.Generic.Dictionary<KahaGameCore.Combat.IActor, Guid> static_actorToDamageGuid = new System.Collections.Generic.Dictionary<KahaGameCore.Combat.IActor, Guid>();

        public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
        {
            this.vars = vars;
            this.onCompleted = onCompleted;

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
                currentActorIndex = -1;
                CalculateDamage();
                return;
            }

            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.OnAttackStarted, Start_NextTarget_OnAttackStarted);
        }

        private void CalculateDamage()
        {
            for (int i = 0; i < processData.targets.Count; i++)
            {
                float rawDamage = KahaGameCore.Combat.Calculator.Calculate(new KahaGameCore.Combat.Calculator.CalculateData
                {
                    caster = processData.caster,
                    target = processData.targets[i],
                    formula = vars[0],
                    useBaseValue = false
                });
                float defense = processData.targets[i].Stats.GetTotal(Const.Defense, false);

                float finalDamage = (defense / (rawDamage + defense)) * rawDamage;

                if (finalDamage < 1f)
                    finalDamage = 1f;

                if (static_actorToDamageGuid.ContainsKey(processData.targets[i]))
                {
                    Guid guid = static_actorToDamageGuid[processData.targets[i]];
                    processData.targets[i].Stats.SetTemp(guid, Convert.ToInt32(finalDamage));
                }
                else
                {
                    Guid guid = processData.targets[i].Stats.Add(Const.Damage, Convert.ToInt32(finalDamage));
                    static_actorToDamageGuid.Add(processData.targets[i], guid);
                }
            }

            Start_Caster_OnDamageCalculated();
        }

        private void Start_Caster_OnDamageCalculated()
        {
            currentActorIndex = -1;
            processData.caster.SkillTrigger.Trigger(Const.OnDamageCalculated, Start_NextTarget_BeforeDamaged);
        }

        private void Start_NextTarget_BeforeDamaged()
        {
            currentActorIndex++;
            
            if (currentActorIndex >= processData.targets.Count)
            {
                currentActorIndex = -1;

                Start_NextTarget_ApplyDamage();
                return;
            }

            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.BeforeDamaged, Start_NextTarget_BeforeDamaged);
        }

        private void Start_NextTarget_ApplyDamage()
        {
            currentActorIndex++;

            if (currentActorIndex >= processData.targets.Count)
            {
                Start_Caster_OnAttackEnded();
                return;
            }

            int damage = processData.targets[currentActorIndex].Stats.GetTotal(Const.Damage, false);

            if (damage > 0)
            {
                processData.targets[currentActorIndex].Stats.AddBase(Const.HP, -damage);

                // TODO: add animation info

                Start_NextTarget_OnGotDamaged();
            }
            else
            {
                Start_NextTarget_OnGotHit();
            }
        }

        private void Start_NextTarget_OnGotDamaged()
        {
            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.OnGotDamaged, Start_NextTarget_OnGotHit);
        }

        private void Start_NextTarget_OnGotHit()
        {
            processData.targets[currentActorIndex].SkillTrigger.Trigger(Const.OnGotHit, Start_NextTarget_ApplyDamage);
        }

        private void Start_Caster_OnAttackEnded()
        {
            processData.caster.SkillTrigger.Trigger(Const.OnAttackEnded, onCompleted);
        }
    }
}