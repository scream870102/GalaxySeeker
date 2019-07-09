namespace Eccentric.UnityUtils.Attack {
    /// <summary>interface for attack action</summary>
    public interface IAttackAction {
        /// <summary>define if character can use this action to attack right now</summary>
        bool IsCanAttack { get; }
        /// <summary>define remain time of cooldown</summary>
        float CDRemain { get; }
    }
}
