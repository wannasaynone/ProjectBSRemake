using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class WaitingActionRateState : CombatStateBase, ITickable
    {
        public static event System.Action<CombatActor> OnActionRateFull;

        private readonly Dictionary<CombatActor, float> actorToActionDelta;

        public WaitingActionRateState(List<CombatActor> actors)
        {
            actors.Sort((x, y) => y.GetTotal("Speed", false).CompareTo(x.GetTotal("Speed", false)));
            actorToActionDelta = new Dictionary<CombatActor, float>();
            for (int i = 0; i < actors.Count; i++)
            {
                actorToActionDelta.Add(actors[i], (float)actors[i].GetTotal("Speed", false) / (float)actors[0].GetTotal("Speed", false));
            }
        }

        public override void Enter()
        {
            UnityTicker.Add(this);
        }

        public void Tick(float delta)
        {
            foreach(KeyValuePair<CombatActor, float> kvp in actorToActionDelta)
            {
                kvp.Key.actionRate += kvp.Value * delta * 2f; // 2 for increase add speed
                UnityEngine.Debug.Log(kvp.Key.name + " delta=" + kvp.Value + ", actionRate=" + kvp.Key.actionRate);
            }

            foreach (KeyValuePair<CombatActor, float> kvp in actorToActionDelta)
            {
                if (kvp.Key.actionRate >= 1f)
                {
                    UnityTicker.Remove(this);
                    OnActionRateFull?.Invoke(kvp.Key);
                    break;
                }
            }
        }
    }
}