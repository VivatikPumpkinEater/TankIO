public interface IDamageable
{
    public ActorType ActorType { get; }
    public void TakeDamage(float value);
}