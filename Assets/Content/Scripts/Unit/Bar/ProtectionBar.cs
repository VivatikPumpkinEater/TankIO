public class ProtectionBar : BaseBar
{
    protected override string Prefix { get; }
    protected override string Postfix => "%";

    protected override void UpdateUI(float value)
    {
        var adaptive = value * 100;
        base.UpdateUI(adaptive);
    }
}