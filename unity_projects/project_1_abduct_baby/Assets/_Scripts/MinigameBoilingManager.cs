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

        public int score;

        // Whether user wants to go back to home screne,
        // after finishing the game.
        public bool isFinished;
        public GameObject panelGameOver;
        public Text gameOverLabel;

        public Toggle toggleStove;
        public Slider sliderFireStrength;
        public Text temperatureLabel;

        public Text timerLabel;

        public void SetFinished(bool finished)
        {
            isFinished = finished;
        }

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
                // Freeze timer.
                timer = duration;

                // Prevent mood changes and scene unloading.
                if (!isFinished)
                {
                    // Show GameOver panel.
                    panelGameOver.SetActive(true);

                    // Prepare the message.
                    gameOverLabel.text = string.Format
                    (
                        "Congratulations! You gain " +
                        "<color=green>+{0}</color> points of thurst.",
                        score
                    );

                    return;
                }

                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // Tell manager to set default mood for baby.
                gameManager.baby.SetDefaultMood();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame("MinigameBoiling", 0, 0, score);
            }

            temperatureLabel.text = sliderFireStrength.value.ToString() + " (c)";
        }
    }
}