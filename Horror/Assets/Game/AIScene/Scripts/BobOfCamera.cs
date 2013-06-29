using UnityEngine;
using System.Collections;

public class BobOfCamera : MonoBehaviour
{
    private GameObject camera;

    public double Seperation = 1.0f;
    public double Period = 1.0f;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("/Player/Sub Camera");

        StartCoroutine("isMoving");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator isMoving()
    {
        float deltaTime = 0.0f;
        Vector3 originPos = this.transform.localPosition;

        while (true)
        {
            if ((Input.GetAxis("Forward") != 0) ||
                (Input.GetAxis("Left") != 0) ||
                (Input.GetAxis("Back") != 0) ||
                (Input.GetAxis("Right") != 0))
            {
                this.transform.localPosition = originPos;

                float y = Mathf.Sin((float)(deltaTime * Period));
                //y = Mathf.Abs(y);

                Vector3 newPos = originPos;

                newPos.y += (float)(y / Seperation);

                this.transform.localPosition = newPos;

                deltaTime += Time.deltaTime;
            }
            yield return null;
        }
    }
}
