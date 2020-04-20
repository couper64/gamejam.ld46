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

		private void OnTriggerStay(Collider other)
		{
			if (Input.GetMouseButton(0)) 
			{
				// Keep cleaning until none.
				DirtySpot spot = other.GetComponent<DirtySpot>();

				// Null checking.
				if (!spot) 
				{
					// Local variables.
					float delta;

					// Distance.
					delta = Vector2.Distance
					(
						lastMousePosition, 
						Input.mousePosition
					);

					// Apply washing strength.
					spot.dirtiness -= Time.deltaTime * delta;
				}
			}
		}
	}
}