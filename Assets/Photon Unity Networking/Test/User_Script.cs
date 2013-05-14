/*using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	public GameObject beam;
	private float fire_last = 0f;
	private float fire_term = 0.3f;
	private float Speed = 10f;
	
	public bool isControl = false;
	
	void Update ()
	{
		Fire ();
		if (isControl != true) return;
		if(Input.GetKey(KeyCode.RightArrow)) Move_Right();
		if(Input.GetKey(KeyCode.LeftArrow)) Move_Left();
	}
	void Move_Right()
	{
		transform.Translate(Vector3.left * Speed * Time.deltaTime);
		if(transform.position.x > 5f) transform.position = new Vector3(5f,0f,0f);
	}
	void Move_Left()
	{
		transform.Translate(Vector3.right * Speed * Time.deltaTime);
		if(transform.position.x < -5f) transform.position = new Vector3(-5f,0f,0f);
	}
	void Fire()
	{
		if(fire_last + fire_term > Time.time) return;
		StartCoroutine("Fire_Beam");
		fire_last = Time.time;
	}
	IEnumerator Fire_Beam()
	{
		Instantiate(beam, transform.position, Quaternion.identity);
		return null;
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag != "Enemy") return;
		PhotonNetwork.Destroy(gameObject);
		//StartCoroutine("Die");
	}
	public void Set_isControl(bool x)
	{
		isControl = x;
	}
	/*IEnumerator Die()
	{
		Destroy(gameObject);
		return null;
	}
}
*/

using UnityEngine;
using System.Collections;
 
public class User_Script: MonoBehaviour
{
    private float Speed = 10f;
     
    public bool isControl = false;
 
    void Start () {
     
    }
     
    // Update is called once per frame
    void FixedUpdate ()
	{
		Debug.Log ("player Update");
        if (isControl != true) return;
        if (Input.GetKey(KeyCode.RightArrow)) Move_Right();
        if (Input.GetKey(KeyCode.LeftArrow)) Move_Left();
    }
 
    void Move_Right()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
 
    void Move_Left()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
 
    void OnTriggerEnter(Collider col)
    {
    }
 
    public void Set_isControl(bool x)
    {
        isControl = x;
    }
}