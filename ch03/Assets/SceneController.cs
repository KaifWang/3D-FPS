using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
    private int _nums;
    private GameObject[] _enemy;

    void Start()
    {
        _nums = 1;
        _enemy = new GameObject[1];
    }

	void Update() {
        bool isempty = true;
        for(int i = 0; i < _enemy.Length; i++)
        {
            if(_enemy[i] != null)
            {
                isempty = false;
            }
        }
		if (isempty) {
            _enemy = new GameObject[_nums++];
            for (int i = 0; i < _enemy.Length; i++)
            {
                _enemy[i] = Instantiate(enemyPrefab) as GameObject;
                _enemy[i].transform.position = new Vector3(0, 1, 0);
                float angle = Random.Range(0, 360);
                _enemy[i].transform.Rotate(0, angle, 0);
            }
        }
	}
}
