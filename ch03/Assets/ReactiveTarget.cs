using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {
    [SerializeField] private GameObject tombstonePrefab;
    private GameObject _tombstone;

    private bool _isdying = false;
    private float dyingangle = 90;
    public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI>();
		if (behavior != null) {
			behavior.SetAlive(false);
		}
		StartCoroutine(Die());
	}

    void Update()
    {
        if (_isdying)
        {
            this.transform.Rotate(dyingangle * Time.deltaTime, 0, 0);
        }
    }

	private IEnumerator Die() {
        _isdying = true;
		
		yield return new WaitForSeconds(1);

        Destroy(this.gameObject);
        _tombstone = Instantiate(tombstonePrefab) as GameObject;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        _tombstone.transform.position = pos;


    }
}
