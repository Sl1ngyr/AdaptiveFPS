namespace UI
{
    public class UltimateUI : BaseSliderBar
    {

        public override void SetStats(int count)
        {
            slider.value = count;
            text.text = count + "/100";
        }
    }
}