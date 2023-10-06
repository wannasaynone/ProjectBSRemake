using KahaGameCore.Combat.Processor.EffectProcessor;

namespace ProjectBS.Combat.Command
{
    public class EffectCommandFactory_CannotAct : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_CannotAct();
        }
    }

    public class EffectCommandFactory_ResetCannotAct : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_ResetCannotAct();
        }
    }

    public class EffectCommandFactory_SelectSelf : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_SelectSelf();
        }
    }

    public class EffectCommandFactory_RecoverHealth : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_RecoverHealth();
        }
    }

    public class EffectCommandFactory_Select : EffectCommandFactoryBase
    {
        private readonly ITargetSelector targetSelector;

        public EffectCommandFactory_Select(ITargetSelector targetSelector)
        {
            this.targetSelector = targetSelector;
        }

        public override EffectCommandBase Create()
        {
            return new EffectCommand_Select(targetSelector);
        }
    }

    public class EffectCommandFactory_Attack : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_Attack();
        }
    }

    public class EffectCommandFactory_PlayAnimation : EffectCommandFactoryBase
    {
        public override EffectCommandBase Create()
        {
            return new EffectCommand_PlayAnimation();
        }
    }
}