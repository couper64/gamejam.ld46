namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Toggle, Slider, Text.
    using UnityEngine.UI;

    public class MinigameCleaningManager : MonoBehaviour
    {
        public float timer;
        public float duration;

        public int score;

        // Whether user wants to go back to home screne,
        // after finishing the game.
        public bool isFinished;
        public GameObject panelGameOver;
        public Text gameOverLabel;

        public Text timerLabel;

        public void SetFinished(bool finished) 
        {
            isFinished = finished;
        }

        private void Update()
        {
            // Minigame ticking.
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
                    // Find and destroy spoon.
                    Sponge sponge = FindObjectOfType<Sponge>();

                    // Destroy only if exists.
                    if (sponge != null) 
                    {
                        Destroy(sponge.gameObject);
                    }

                    // Show cursor.
                    Cursor.visible = true;

                    // Show GameOver panel.
                    panelGameOver.SetActive(true);

                    // Prepare the message.
                    gameOverLabel.text = string.Format
                    (
                        "Congratulations! You gain " + 
                        "<color=green>+{0}</color> points of hunger.",
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
                gameManager.UnloadMinigame("MinigameCleaning", 0, score, 0, 0);
            }
        }
    }
}
