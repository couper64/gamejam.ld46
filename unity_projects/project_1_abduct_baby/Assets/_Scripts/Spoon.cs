namespace LD46
{
	using UnityEngine;

	public class Spoon : MonoBehaviour
	{
		public Rigidbody2D rb;

		private void Start()
		{
			Cursor.visible = false;
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			rb.MovePosition(mouse);
		}

		private void OnDestroy()
		{
			Cursor.visible = true;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			GetComponentInChildren<AudioSource>().Play();
		}
	}
}