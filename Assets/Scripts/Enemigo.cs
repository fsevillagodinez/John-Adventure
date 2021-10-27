using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
    {
    //variables para gestionar el radio de vision de los enemigos 
    public float visionRadius;
    public float speed;

    //variable para guardar eljugador
    GameObject player;

    //variable para guardar la posicion inicial
    Vector3 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
       //recuperamos al jugador gracias al tag
       player = GameObject.FindGameObjectWithTag("Player");

       //Guardamos nuestra posicion inicial
       initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       // por defecto nuestro objectivo siempre sera nuestra posicion inicial
       Vector3 target = initialPosition;

       // pero si la distacia hasta el jugador es menorque es radio de vision el objectivo sera el
       float dist = Vector3.Distance(player.transform.position, transform.position);
       if (dist < visionRadius) target = player.transform.position;
       
       // finalmente movemos al enemigo en direccion a su target
       float fixedSpeed = speed*Time.deltaTime;
       transform.position =Vector3.MoveTowards(transform.position, target, fixedSpeed);

       //y podemos debuguearlo con una linea
       Debug.DrawLine(transform.position, target, Color.green);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
    }