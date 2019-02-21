using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed = 10f;
    public float hoverForce = 20f;
    public float hoverHeight = 3.5f;
    private Rigidbody soldierRigidbody;
    public Vector3 locToGo;
    void Awake()
    {
        soldierRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (transform.position.x == locToGo.x && transform.position.z == locToGo.z)
            setLocToGo(new Vector3(-8000, -8000, -8000));
        else if (locToGo.x != -8000) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, locToGo, step);
        }
    }
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            soldierRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }

    public void setLocToGo(Vector3 new_loc)
    {
        locToGo = new_loc;
    }
}