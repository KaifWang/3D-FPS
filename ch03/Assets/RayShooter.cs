using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {

    public GameObject player;
    public float floatingLabelHorSpeed = 30.0f;
    public float floatingLabelVertSpeed = 30.0f;
    private float floatingLabelPosX;
    public float floatingLabelPosY;
    private Camera _camera;

	void Start() {
		_camera = GetComponent<Camera>();
        floatingLabelPosX = _camera.pixelWidth / 2;
        floatingLabelPosY = _camera.pixelHeight / 2;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");

        string _health = "0";
        PlayerCharacter playerChar = player.GetComponent<PlayerCharacter>();
        if (playerChar.getHealth() == 1)
        {
            _health = "1: *";
        }
        else if (playerChar.getHealth() == 2)
        {
            _health = "2: * *";
        }
        else if (playerChar.getHealth() == 3)
        {
            _health = "3: * * *"; 
        }
        else if (playerChar.getHealth() == 4)
        {
            _health = "4: * * * *";
        }
        else if (playerChar.getHealth() == 5)
        {
            _health = "5: * * * * *";
        }
        if (playerChar.getHealth() == 0)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(floatingLabelPosX, floatingLabelPosY, 200, 50), "You have died", style);
        }
        else
        {
            GUI.Label(new Rect(_camera.pixelWidth / 1.20f, _camera.pixelHeight / 10, size * 5, size * 5), _health);
        }

	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if (target != null) {
					target.ReactToHit();
				} else {
					StartCoroutine(SphereIndicator(hit.point));
				}
			}
		}

        PlayerCharacter playerChar = player.GetComponent<PlayerCharacter>();
        if(playerChar.getHealth() == 0)
        {
            floatingLabelPosX += floatingLabelHorSpeed * Time.deltaTime;
            floatingLabelPosY += floatingLabelVertSpeed * Time.deltaTime;
            if (floatingLabelPosX >= _camera.pixelWidth - 200)
            {
                floatingLabelHorSpeed =  (-1) * floatingLabelHorSpeed;
            }
            else if (floatingLabelPosX <= 0)
            {
                floatingLabelHorSpeed = (-1) * floatingLabelHorSpeed;
            }
            if (floatingLabelPosY >= _camera.pixelHeight - 50)
            {
                floatingLabelVertSpeed = (-1) * floatingLabelVertSpeed;
            }
            else if (floatingLabelPosY <= 0)
            {
                floatingLabelVertSpeed = (-1) * floatingLabelVertSpeed;
            }

        }

	}

	private IEnumerator SphereIndicator(Vector3 pos) {
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = pos;

		yield return new WaitForSeconds(1);

		Destroy(sphere);

	}
}