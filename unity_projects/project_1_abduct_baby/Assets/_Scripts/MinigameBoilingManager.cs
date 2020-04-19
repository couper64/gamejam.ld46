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

            // Is it the time to stop the game?
            if (timer >= duration)
            {
                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // Tell manager to set default mood for baby.
                gameManager.baby.SetDefaultMood();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame("MinigameBoiling", 10);
            }

            temperatureLabel.text = sliderFireStrength.value.ToString() + " (c)";
        }
    }
}