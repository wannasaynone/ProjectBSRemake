using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class WaitingActionRateState : CombatStateBase, ITickable
    {
        private readonly Dictionary<CombatActor, float> actorToActionDelta;

        private System.Action onEnded;

        public WaitingActionRateState(List<CombatActor> actors)
        {
            actors.Sort((x, y) => y.Stats.GetTotal("Speed", false).CompareTo(x.Stats.GetTotal("Speed", false)));
            actorToActionDelta = new Dictionary<CombatActor, float>();
            float baseSpeed = (float)actors[0].Stats.GetTotal("Speed", false);
            for (int i = 0; i < actors.Count; i++)
            {
                if (i == 0)
                {
                    actorToActionDelta.Add(actors[i], 1f);
                }
                else
                {
                    actorToActionDelta.Add(actors[i], (float)actors[i].Stats.GetTotal("Speed", false) / baseSpeed);
                }
            }
        }

        public override void Enter(System.Action onEnded)
        {
            this.onEnded = onEnded;
            UnityTicker.Add(this);
        }

        public void Tick(float delta)
        {
            foreach(KeyValuePair<CombatActor, float> kvp in actorToActionDelta)
            {
                kvp.Key.actionRate += kvp.Value * delta * 4f; // 4 for 0.25f
                UnityEngine.Debug.Log(kvp.Key.name + " delta=" + kvp.Value + ", actionRate=" + kvp.Key.actionRate);
            }

            foreach (KeyValuePair<CombatActor, float> kvp in actorToActionDelta)
            {
                if (kvp.Key.actionRate >= 1f)
                {
                    UnityTicker.Remove(this);
                    onEnded?.Invoke();
                    break;
                }
            }
        }
    }
}