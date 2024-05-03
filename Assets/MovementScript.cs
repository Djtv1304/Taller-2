using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del cubo (ajusta seg�n sea necesario)
    public float doubleJumpForce = 65f; // Fuerza de doble salto
    public Transform sueloCheck; // Punto de comprobaci�n para detectar el suelo
    public LayerMask capaSuelo; // Capa que representa el suelo

    private Rigidbody rb;
    private bool enSuelo = false;
    private bool saltoPendiente = false;
    private bool dobleSaltoDisponible = false;
    private Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /* C�digo que no s� por qu� no reproduc�a una sola vez:c */
        //animator = GetComponent<Animator>(); // Obtener la referencia al componente Animator
        //animator.Play("Salto", -1, 0); // Reproducir la animaci�n al inicio del juego
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Comprobar si el cubo est� en el suelo
        enSuelo = Physics.CheckSphere(sueloCheck.position, 100f, capaSuelo);

        // Movimiento en las cuatro direcciones
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.velocity = movimiento * moveSpeed;

        // Salto simple
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dobleSaltoDisponible)
            {
                // Si no est� en el suelo pero el doble salto est� disponible, realizar un doble salto
                rb.velocity = new Vector3(rb.velocity.x, doubleJumpForce, rb.velocity.z);
                dobleSaltoDisponible = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Permitir el doble salto cuando se toca el suelo
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            dobleSaltoDisponible = true;
            saltoPendiente = false;
        }
    }
}
