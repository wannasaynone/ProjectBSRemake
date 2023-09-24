using KahaGameCore.Combat;
using System;
using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class TargetSelector : ITargetSelector
    {
        public static TargetSelector Instance 
        {
            get 
            {
                if (instance == null)
                    instance = new TargetSelector();

                return instance;
            }
        }
        private static TargetSelector instance;

        private TargetSelector() { }

        public void StartSelect(string[] vars, Action<List<IActor>> onSelected)
        {
            throw new NotImplementedException();
        }
    }
}