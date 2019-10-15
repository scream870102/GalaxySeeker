using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.Jellyfish {
    public class Jellyfish : AEnemy {

        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am jellyfish I come from hell");
        }
        public void ChooseNextAction ( ) {
            List<Action> ableActions = new List<Action> ( );
            float distance = Vector2.Distance (this.tf.position, this.Player.tf.position);
            Action actResult;
            //先將可以使用的行動收集起來
            foreach (Action act in this.Actions) {
                if (act.IsCanUse && act.Distance >= distance)
                    ableActions.Add (act);
            }
            //從中選出行動
            if (ableActions.Count == 0) return;
            actResult = ableActions [ActionSelector.SelectWithProbability (ableActions)];
            actResult.ResetCD ( );
            //根據行動切換對應的動畫
            if (actResult.Trigger != "")
                this.Animator.SetTrigger (actResult.Trigger);
        }
    }
    public class AJellyFishComponent:AEnemyComponent{
        public Jellyfish Parent { get { return this.parent as Jellyfish; } protected set { this.parent = value; } }
    }
}
