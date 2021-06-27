using UnityEngine;

public class CameraFree : MonoBehaviour
{
	[SerializeField] private float m_camSpeed;
	[SerializeField] private float m_rotCamSpeed;
	[SerializeField] private float m_scrollSpeed;

	private void Update()
	{
		Vector3 pos = transform.position;
		Vector3 rot = transform.rotation.eulerAngles;

		if(Input.GetKey("z"))
		{
			pos.z += m_camSpeed * Time.deltaTime;
		}
		if(Input.GetKey("q"))
		{
			pos.x -= m_camSpeed * Time.deltaTime;
		}
		if(Input.GetKey("s"))
		{
			pos.z -= m_camSpeed * Time.deltaTime;
		}
		if(Input.GetKey("d"))
		{
			pos.x += m_camSpeed * Time.deltaTime;
		}
		if(Input.GetKey("e"))
		{
			rot.y += m_rotCamSpeed * Time.deltaTime;
		}
		if(Input.GetKey("a"))
		{
			rot.y -= m_rotCamSpeed * Time.deltaTime;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y -= scroll * m_scrollSpeed * Time.deltaTime;

		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
	}
}
