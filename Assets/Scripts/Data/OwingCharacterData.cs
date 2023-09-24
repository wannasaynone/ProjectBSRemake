namespace ProjectBS.Data
{
    public class OwingCharacterData
    {
        public string Uid { get; private set; }
        public int SourceID { get; private set; }
        public int Rank { get; private set; }
        public int Progress { get; private set; }

        public class EquipmentStats
        {
            public class AbilityData
            {
                public string Type;
                public int Value;
                public bool Locked;
            }

            public AbilityData AbilityData_1;
            public AbilityData AbilityData_2;
            public AbilityData AbilityData_3;
            public AbilityData AbilityData_4;
        }

        public EquipmentStats Weapon { get; private set; }
        public EquipmentStats Armor { get; private set; }
        public EquipmentStats Boots { get; private set; }
        public EquipmentStats Helmet { get; private set; }
        public EquipmentStats Accessories { get; private set; }
    }
}