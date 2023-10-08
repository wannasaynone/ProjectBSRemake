using System.Collections.Generic;
using KahaGameCore.Combat.Processor.EffectProcessor;

namespace ProjectBS.Combat
{
    public static class CombatUtility 
    {
        public static List<UI.CombatUI.CombatActorUIInfo> GetUIInfo(Data.GameStaticDataManager gameStaticDataManager, List<CombatActor> combatActors, bool isPlayer)
        {
            List<UI.CombatUI.CombatActorUIInfo> info = new List<UI.CombatUI.CombatActorUIInfo>();
            for (int actorIndex = 0; actorIndex < combatActors.Count; actorIndex++)
            {
                info.Add(GetUIInfo(gameStaticDataManager, combatActors[actorIndex], isPlayer));
            }

            return info;
        }

        public static UI.CombatUI.CombatActorUIInfo GetUIInfo(Data.GameStaticDataManager gameStaticDataManager, CombatActor combatActor, bool isPlayer)
        {
            Data.CharacterData characterData = gameStaticDataManager.GetData<Data.CharacterData>(combatActor.sourceID);
            UI.CombatUI.CombatActorUIInfo info = new UI.CombatUI.CombatActorUIInfo
            {
                spriteAddress = characterData.Address,
                isPlayer = isPlayer,
                offset = new UnityEngine.Vector3(characterData.OffsetX, characterData.OffsetY),
                skills = new List<UI.SkillButton.SkillButtonInfo>()
            };

            int skillCount = combatActor.GetSkillCount();
            for (int skillIndex = 0; skillIndex < skillCount; skillIndex++)
            {
                Data.SkillData skillData = combatActor.GetSkillSourceByIndex(skillIndex);
                info.skills.Add(new UI.SkillButton.SkillButtonInfo
                {
                    id = skillData.ID,
                    name = gameStaticDataManager.GetData<Data.ContextData>(skillData.NameID).zh_tw, // TODO: localize
                    isActiveSkill = skillData.Commands.Contains(Const.OnActived)
                });
            }

            return info;
        }

        public static CombatActor ConvertCharacterToCombatActor(EffectCommandDeserializer effectCommandDeserializer, Data.GameStaticDataManager gameStaticDataManager, Data.OwingCharacterData character)
        {
            Data.CharacterData source = gameStaticDataManager.GetData<Data.CharacterData>(character.SourceID);

            CombatActor.InitialInfo initialInfo = new CombatActor.InitialInfo
            {
                Attack = source.Attack,
                Defense = source.Defense,
                Critical = source.Critical,
                CriticalDefense = source.CriticalDefense,
                MaxHealth = source.Health,
                Speed = source.Speed,
                Skills = new List<Data.SkillData>()
            };

            AssignAbilityToActor(initialInfo, character.Weapon);
            AssignAbilityToActor(initialInfo, character.Armor);
            AssignAbilityToActor(initialInfo, character.Helmet);
            AssignAbilityToActor(initialInfo, character.Boots);
            AssignAbilityToActor(initialInfo, character.Accessories);

            initialInfo.Skills.Add(gameStaticDataManager.GetData<Data.SkillData>(source.PassiveID));
            if (character.Skill_1 >= 0) initialInfo.Skills.Add(gameStaticDataManager.GetData<Data.SkillData>(character.Skill_1));
            if (character.Skill_2 >= 0) initialInfo.Skills.Add(gameStaticDataManager.GetData<Data.SkillData>(character.Skill_2));
            if (character.Skill_3 >= 0) initialInfo.Skills.Add(gameStaticDataManager.GetData<Data.SkillData>(character.Skill_3));
            if (character.Skill_4 >= 0) initialInfo.Skills.Add(gameStaticDataManager.GetData<Data.SkillData>(character.Skill_4));

            CombatActor combatActor = new CombatActor(initialInfo, effectCommandDeserializer);

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