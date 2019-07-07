namespace Eccentric.Attack {
    public interface IAttack {
        bool IsCanAttack { get; }
        float CDRemain { get; }
    }
}
