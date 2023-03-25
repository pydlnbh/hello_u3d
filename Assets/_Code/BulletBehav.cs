using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehav : MonoBehaviour
{
    private const float MOVE_SPEED = 64f;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * MOVE_SPEED, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.up * MOVE_SPEED * Time.deltaTime);

        if (gameObject.transform.position.y > 20f)
        {
            Destroy(gameObject);
        }
    }
}
