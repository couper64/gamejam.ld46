namespace LD46
{
    using UnityEngine;

    /// <summary>
    /// One script to rule them all.
    /// </summary>
    public class GameManager : MonoBehaviour
    {


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Days.
        // Iterate of days.
        // End of the game.
    }
}