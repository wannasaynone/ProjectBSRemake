namespace ProjectBS.Data
{
    public class OwingCharacterData
    {
        public string Uid;
        public int SourceID;
        public int Rank;
        public int Progress;

        public class EquipmentStats
        {
            public class AbilityData
            {
                public string Tag;
                public int Value;
                public bool Locked;
            }

            public AbilityData AbilityData_1;
            public AbilityData AbilityData_2;
            public AbilityData AbilityData_3;
            public AbilityData AbilityData_4;
        }

        public EquipmentStats Weapon;
        public EquipmentStats Armor;
        public EquipmentStats Boots;
        public EquipmentStats Helmet;
        public EquipmentStats Accessories;

        public int Skill_1 = -1;
        public int Skill_2 = -1;
        public int Skill_3 = -1;
        public int Skill_4 = -1;
    }
}