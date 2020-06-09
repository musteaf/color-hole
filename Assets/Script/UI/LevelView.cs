using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Text currentLevelText;
        [SerializeField] private Text nextLevelText;
        [SerializeField] private Slider currentSlider;
        [SerializeField] private Slider nextSlider;

        public void SetLevel(int level)
        {
            currentLevelText.text = level.ToString();
            nextLevelText.text = (level + 1).ToString();
        }
        
        public void SetPercentage(float percentage)
        {
            currentSlider.value = percentage;
            if (percentage > 1)
                nextSlider.value = percentage - 1;
        }

        public void ResetPercentage()
        {
            currentSlider.value = 0;
            nextSlider.value = 0;
        }
    }
}