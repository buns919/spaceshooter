using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = .03f;

    Material myMaterial;
    Vector2 offset;

	// Use this for initialization
	void Start () {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
	}
}
