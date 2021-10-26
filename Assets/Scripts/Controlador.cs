using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    // Variables aqui va todo lo que queremos que el personaje tenga
    Rigidbody rig;
    public float velocidad = 1f;
    public float velocidadDeRotacion = 1f;
    public float fuerzaDeSalto = 1f;
    int experiencia = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y + fuerzaDeSalto, rig.velocity.z);
        }
        transform.Rotate(0, Input.GetAxis("Horizontal") * velocidadDeRotacion, 0);
        rig.velocity = Input.GetAxis("Vertical") * transform.forward * velocidad + new Vector3(0, rig.velocity.y, 0);
    
    }
}
