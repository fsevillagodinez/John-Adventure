using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jugador : MonoBehaviour
{

    Rigidbody rigidBody;
    public float speed = 1;
    public float rotationSpeed = 1;
    public float jumpForce = 1;
    public GameObject bolaDeFuego;
    public float gunSpeed = 1;
    public float enemies = 3;
    public AudioClip sonidoGanar;
    public AudioClip sonidoPerder;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Se obtiene el rigidbody para mover el jugador
        rigidBody = GetComponent<Rigidbody>();

        // Se obtiene el rigidbody para mover el jugador
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disparar
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Disparar();
        }

        // Saltar
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Aplicalo la fuerza de salto a la velocidad en y del rigidbody
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + jumpForce, rigidBody.velocity.z);
        }

        // Moverse hacia adelante y atras
        // Obtengo la direccion a la que el jugador quiere moverse. -1 si quiero ir hacia atras o 1 si quiero ir hacia adelante
        float vertical = Input.GetAxis("Vertical");

        // Le doy la velocidad correspondiente al rigidbody para moverse en esta direccion
        rigidBody.velocity = transform.forward * speed * vertical + new Vector3(0, rigidBody.velocity.y, 0);

        // Girar hacia la derecha e izquierda
        // Obtengo la direccion a la que el jugador quiere moverse. -1 si quiero ir hacia la izquierda o 1 si quiero ir hacia derecha
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(new Vector3( 0, rotationSpeed * horizontal * Time.deltaTime, 0));

    }

    // Funcion para disparar bolas de fuego
    void Disparar() {
        // Crear la instancia de la bola de fuego
        GameObject nuevaBolaDeFuego = Instantiate(bolaDeFuego, transform.position + transform.forward, new Quaternion(0,0,0,0));
        // Se guarda el rgid body de objeto nuevo para poder utilizarlo
        Rigidbody bolaDeFuegoRig = nuevaBolaDeFuego.transform.GetComponent<Rigidbody>();
        // Se le asigna una nueva velocidad a la bola de fuego
        bolaDeFuegoRig.velocity = transform.forward * gunSpeed;
        // Destruir despues de 3 segundos
        Destroy(nuevaBolaDeFuego, 3);
    }

    public void EnemigoMuerto() {
        enemies -= 1;

        // Se usa StartCoroutine cuando la funcion es un IEnumerator
        if(enemies <= 0) StartCoroutine(Ganar());
    }

    // IEnumerator me funciona cuando necesito esperar un tiempo para ejecutar alguna instruccion
    IEnumerator Ganar () {
        
        audioSource.clip = sonidoGanar;
        audioSource.Play();

        // Esta instruccion hace que el codigo espere 5 segundos para ejecutar las instrucciones que le siguen
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Ganar");
    }

    // IEnumerator me funciona cuando necesito esperar un tiempo para ejecutar alguna instruccion
    public IEnumerator Perder () {
        
        audioSource.clip = sonidoPerder;
        audioSource.Play();

        // Esta instruccion hace que el codigo espere 5 segundos para ejecutar las instrucciones que le siguen
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Perder");
    }
}
