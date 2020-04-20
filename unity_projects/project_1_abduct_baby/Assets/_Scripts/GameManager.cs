namespace LD46
{
    // IEnumerator.
    using System.Collections;

    // Monobehaviour.
    using UnityEngine;

    // Text.
    using UnityEngine.UI;

    // SceneManager.
    using UnityEngine.SceneManagement;

    /// <summary>
    /// One script to rule them all.
    /// Days.
    /// Iterate over days.
    /// End of the game.
    /// Each day consists out of events, which lead to a minigame.
    /// Multiple type of events depend on baby's state.
    /// Mutliple type of minigames depend on the type of the event.
    /// Every minigames is time limited.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public int dayCount;
        public int dayFinal;
        public int eventsCount;
        public int eventsMax;
        public Text userLog;
        public Button buttonMinigameFeeding;
        public Button buttonMinigameBoiling;
        public Button buttonMinigameCleaning;
        public Button buttonMinigamePatting;
        public Canvas canvasMain;
        public AudioSource mouseAudioSource;
        public AudioSource scoringUpAudioSource;
        public Baby baby;

        private IEnumerator ShowScore
        (
            int hungerScore, 
            int cleanlinessScore, 
            int thurstScore, 
            int burpScore, 
            float duration
        ) 
        {
            // Find children and fade out others.
            for (int i = 0; i < canvasMain.transform.childCount; i++)
            {
                Transform childTransform = canvasMain.transform.GetChild(i);

                if (childTransform.name.StartsWith("PanelActions"))
                {
                    // Local variables;
                    Button[] buttons;

                    // Get button components.
                    buttons = childTransform.GetComponentsInChildren<Button>();

                    // Make them all uninteractive.
                    foreach (var button in buttons)
                    {
                        button.interactable = false;
                    }
                }
            }

            // Do increment animation for hunger.
            for (int i = 0; i < Mathf.Abs(hungerScore); i++)
            {
                // Make a decision to subtract or add.
                float direction = hungerScore > 0 ? 1 : -1;

                // Store the score.
                baby.hunger += direction;

                // Setup sounds.
                scoringUpAudioSource.pitch = 1.00f + (float)i / hungerScore;

                // Play sounds.
                scoringUpAudioSource.Play();

                yield return new WaitForSeconds(duration / Mathf.Abs(hungerScore));
            }

            // Reset.
            scoringUpAudioSource.pitch = 1.00f;

            // Do increment animation for hunger.
            for (int i = 0; i < Mathf.Abs(thurstScore); i++)
            {
                // Make a decision to subtract or add.
                float direction = thurstScore > 0 ? 1 : -1;

                // Store the score.
                baby.thurst += direction;

                // Setup sounds.
                scoringUpAudioSource.pitch = 1.00f + (float)i / thurstScore;

                // Play sounds.
                scoringUpAudioSource.Play();

                yield return new WaitForSeconds(duration / Mathf.Abs(thurstScore));
            }

            // Reset.
            scoringUpAudioSource.pitch = 1.00f;

            // Do increment animation for hunger.
            for (int i = 0; i < Mathf.Abs(cleanlinessScore); i++)
            {
                // Make a decision to subtract or add.
                float direction = cleanlinessScore > 0 ? 1 : -1;

                // Store the score.
                baby.cleanliness += direction;

                // Setup sounds.
                scoringUpAudioSource.pitch = 1.00f + (float)i / cleanlinessScore;

                // Play sounds.
                scoringUpAudioSource.Play();

                yield return new WaitForSeconds
                (
                    duration / Mathf.Abs(cleanlinessScore)
                );
            }

            // Reset.
            scoringUpAudioSource.pitch = 1.00f;

            // Do increment animation for hunger.
            for (int i = 0; i < Mathf.Abs(burpScore); i++)
            {
                // Make a decision to subtract or add.
                float direction = burpScore > 0 ? 1 : -1;

                // Store the score.
                baby.burp += direction;

                // Setup sounds.
                scoringUpAudioSource.pitch = 1.00f + (float)i / thurstScore;

                // Play sounds.
                scoringUpAudioSource.Play();

                yield return new WaitForSeconds(duration / Mathf.Abs(burpScore));
            }

            // Reset.
            scoringUpAudioSource.pitch = 1.00f;

            // Fade in others.
            for (int i = 0; i < canvasMain.transform.childCount; i++)
            {
                Transform childTransform = canvasMain.transform.GetChild(i);

                if (childTransform.name.StartsWith("PanelActions"))
                {
                    // Local variables;
                    Button[] buttons;

                    // Get button components.
                    buttons = childTransform.GetComponentsInChildren<Button>();

                    // Make them all uninteractive.
                    foreach (var button in buttons)
                    {
                        button.interactable = true;
                    }
                }
            }
        }

        public void GameOver() 
        {
            // Local variables.
            string message;

            // Prepare.
            message = "Game Over.";

            // Output.
            userLog.text = message;

            // Stop updating.
            enabled = false;

            // Load start screen.
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }

        public void LoadMinigame(string sceneName) 
        {
            // Load additively, so the gameplay scene stays loaded.
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            // Hide gameplay UI.
            canvasMain.gameObject.SetActive(false);
        }

        public void UnloadMinigame
        (
            string sceneName, 
            int hungerScore, 
            int cleanlinessScore, 
            int thurstScore,
            int burpScore
        ) 
        {
            // Unload it.
            SceneManager.UnloadSceneAsync(sceneName);

            // Load the main gameplay UI.
            canvasMain.gameObject.SetActive(true);

            // Increment events counter.
            eventsCount++;

            // Do scoring and show results.
            StartCoroutine
            (
                ShowScore
                (
                    hungerScore, 
                    cleanlinessScore, 
                    thurstScore, 
                    burpScore, 
                    5.00f
                )
            );
        }

        public void NextDay() 
        {
            userLog.text = string.Format("It's a happy day {0}!", ++dayCount);
        }

        private void Start()
        {
            buttonMinigameFeeding.onClick.AddListener
            (
                delegate { LoadMinigame("MinigameFeeding"); }
            );
            buttonMinigameFeeding.GetComponentInChildren<Text>().text = "Feed Baby";

            buttonMinigameBoiling.onClick.AddListener
            (
                delegate { LoadMinigame("MinigameBoiling"); }
            );
            buttonMinigameBoiling.GetComponentInChildren<Text>().text = "Boil Milk";

            buttonMinigameCleaning.onClick.AddListener
            (
                delegate { LoadMinigame("MinigameCleaning"); }
            );
            buttonMinigameCleaning.GetComponentInChildren<Text>().text = "Clean";

            // Player starts from day 1!
            NextDay();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                mouseAudioSource.Play();
            }

            if (dayCount >= dayFinal) 
            {
                GameOver();
            }

            if (eventsCount >= eventsMax) 
            {
                // Reset counter.
                eventsCount = 0;

                // New day comes.
                NextDay();
            }
        }
    }
}