namespace UI
{
    public class HealthBarUI : BaseSliderBar
    {
        public override void SetStats(int count)
        {
            slider.value = count;
            text.text = count + "/100";
        }
    }
}