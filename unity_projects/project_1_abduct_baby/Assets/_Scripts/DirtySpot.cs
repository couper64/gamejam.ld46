namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    public class DirtySpot : MonoBehaviour
    {
        public float dirtiness;

        public AudioSource audioSourceCleanUp;

        public SpriteRenderer spriteRenderer;

        public Color colorSprite;

        private void Start()
        {
            dirtiness = 100.00f;

            colorSprite = spriteRenderer.color;
        }

        private void Update()
        {
            if (dirtiness <= 0) 
            {
                // Local.
                MinigameCleaningManager manager;

                // Find.
                manager = FindObjectOfType<MinigameCleaningManager>();

                // Add score.
                manager.score += Random.Range(5, 15);

                audioSourceCleanUp.Play();

                Destroy(gameObject);
            }

            colorSprite.a = dirtiness / 100.00f;

            spriteRenderer.color = colorSprite;
        }
    }
}