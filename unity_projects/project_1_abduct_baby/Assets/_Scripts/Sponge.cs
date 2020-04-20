namespace LD46
{
	using UnityEngine;

	public class Sponge : MonoBehaviour
	{
		public Rigidbody2D rb;

		public Vector2 lastMousePosition;

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

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (Input.GetMouseButton(0))
			{
				// Keep cleaning until none.
				DirtySpot spot = collision.gameObject.GetComponent<DirtySpot>();

				// Local variables.
				float distance;

				// Calculate.
				distance = Vector2.Distance(lastMousePosition, Input.mousePosition);

				// Checking.
				if (spot && (distance != 0.00f))
				{
					// Apply washing strength.
					spot.dirtiness -= Time.deltaTime * 100.00f;
				}
			}
		}
	}
}