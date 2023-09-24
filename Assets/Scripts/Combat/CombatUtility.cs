namespace ProjectBS.Combat
{
    public static class CombatUtility 
    {
        public static CombatActor ConvertCharacterToCombatActor(Data.OwingCharacterData character)
        {
            Data.CharacterData source = Main.GameStaticDataManager.GetGameData<Data.CharacterData>(character.SourceID);

            CombatActor.InitialInfo initialInfo = new CombatActor.InitialInfo
            {
                Attack = source.Attack,
                Defense = source.Defense,
                Critical = source.Critical,
                CriticalDefense = source.CriticalDefense,
                MaxHealth = source.Health,
                Speed = source.Speed,
                Skills = new System.Collections.Generic.List<Data.SkillData>()
            };

            AssignAbilityToActor(initialInfo, character.Weapon);
            AssignAbilityToActor(initialInfo, character.Armor);
            AssignAbilityToActor(initialInfo, character.Helmet);
            AssignAbilityToActor(initialInfo, character.Boots);
            AssignAbilityToActor(initialInfo, character.Accessories);

            initialInfo.Skills.Add(Main.GameStaticDataManager.GetGameData<Data.SkillData>(source.PassiveID));
            if (character.Skill_1 >= 0) initialInfo.Skills.Add(Main.GameStaticDataManager.GetGameData<Data.SkillData>(character.Skill_1));
            if (character.Skill_2 >= 0) initialInfo.Skills.Add(Main.GameStaticDataManager.GetGameData<Data.SkillData>(character.Skill_2));
            if (character.Skill_3 >= 0) initialInfo.Skills.Add(Main.GameStaticDataManager.GetGameData<Data.SkillData>(character.Skill_3));
            if (character.Skill_4 >= 0) initialInfo.Skills.Add(Main.GameStaticDataManager.GetGameData<Data.SkillData>(character.Skill_4));

            CombatActor combatActor = new CombatActor(initialInfo, Main.EffectCommandDeserializer);

            return combatActor;
        }

        private static void AssignAbilityToActor(CombatActor.InitialInfo initialInfo, Data.OwingCharacterData.EquipmentStats equipmentStats)
        {
            if (equipmentStats == null)
                return;

            AssignAbilityToActor(initialInfo, equipmentStats.AbilityData_1);
            AssignAbilityToActor(initialInfo, equipmentStats.AbilityData_2);
            AssignAbilityToActor(initialInfo, equipmentStats.AbilityData_3);
            AssignAbilityToActor(initialInfo, equipmentStats.AbilityData_4);
        }

        private static void AssignAbilityToActor(CombatActor.InitialInfo initialInfo, Data.OwingCharacterData.EquipmentStats.AbilityData abilityData)
        {
            if (abilityData == null)
                return;

            switch(abilityData.Tag)
            {
                case "Attack": initialInfo.Attack += abilityData.Value; break;
                case "Defense": initialInfo.Defense += abilityData.Value; break;
                case "Speed": initialInfo.Speed += abilityData.Value; break;
                case "Critical": initialInfo.Critical += abilityData.Value; break;
                case "CriticalDefense": initialInfo.CriticalDefense += abilityData.Value; break;
                case "Health": initialInfo.MaxHealth += abilityData.Value; break;
            }
        }
    }
}