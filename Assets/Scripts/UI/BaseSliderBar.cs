using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public abstract class BaseSliderBar : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] protected Slider slider;

        public abstract void SetStats(int count);
    }
}