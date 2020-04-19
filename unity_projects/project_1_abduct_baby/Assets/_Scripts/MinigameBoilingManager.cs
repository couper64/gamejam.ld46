namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Toggle, Slider, Text.
    using UnityEngine.UI;

    public class MinigameBoilingManager : MonoBehaviour
    {
        public float timer;
        public float duration;

        public Toggle toggleStove;
        public Slider sliderFireStrength;
        public Text temperatureLabel;

        public Text timerLabel;

        private void Update()
        {
            // Start the game only when we turn on the stove.
            if (!toggleStove.isOn) 
            {
                return;
            }

            // Ticking.
            timer += Time.deltaTime;

            // Let user know.
            timerLabel.text = (duration - timer).ToString("00");

            if (timer >= duration) 
            {
                // End of the mini game.
                // Go back to main screen.
                FindObjectOfType<GameManager>().UnloadMinigame("MinigameBoiling");
            }

            temperatureLabel.text = sliderFireStrength.value.ToString() + " (c)";
        }
    }
}