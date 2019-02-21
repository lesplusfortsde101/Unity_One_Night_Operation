using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public GameObject soldier;
    public GameObject floor;
    public Material soldierMaterial;
    private GameObject selected;
    private Camera cam;
    void Start()
    {
        selected = null;
        cam = Camera.main;
        Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity); 
        Instantiate(soldier, new Vector3(0, 5, 0), Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Soldier")) {
                if (selected != null)
                    selected.GetComponent<Renderer>().material = soldierMaterial;
                selected = hit.collider.gameObject;
                selected.GetComponent<Renderer>().material.color = Color.green;
            } else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Floor") && selected != null) {
                selected.GetComponent<Soldier>().setLocToGo(hit.point);
                selected.GetComponent<Renderer>().material = soldierMaterial;
                selected = null;
            } else if (Physics.Raycast(ray, out hit) && hit.collider == null && selected != null) {
                selected.GetComponent<Renderer>().material = soldierMaterial;
                selected = null;
            }
        }
    }
}
