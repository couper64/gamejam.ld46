namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    public class DirtySpot : MonoBehaviour
    {
        public float dirtiness;

        private void Start()
        {
            dirtiness = 100.00f;
        }

        private void Update()
        {
            if (dirtiness <= 0) 
            {
                FindObjectOfType<MinigameCleaningManager>().score += 5;

                Destroy(gameObject);
            }
        }
    }
}