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
    using System.Data;

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
        public Text userLog;
        public Button buttonMinigameFeeding;
        public Button buttonMinigameBoiling;
        public Canvas canvasMain;
        public AudioSource mouseAudioSource;
        public Baby baby;

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
        }

        public void LoadMinigame(string sceneName) 
        {
            // Load additively, so the gameplay scene stays loaded.
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            // Hide gameplay UI.
            canvasMain.gameObject.SetActive(false);
        }

        public void UnloadMinigame(string sceneName) 
        {
            // Unload it.
            SceneManager.UnloadSceneAsync(sceneName);

            // Load the main gameplay UI.
            canvasMain.gameObject.SetActive(true);
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

            userLog.text = string.Format("It's a happy day {0}!", ++dayCount);
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
        }
    }
}