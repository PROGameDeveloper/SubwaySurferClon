using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Collider Bounds obtiene los valores del tama�o de collider BOUNDS == LIMITE 
        Bounds collisionbounds = collision.collider.bounds;
        Bounds playerBounds = gameObject.GetComponent<Collider>().bounds;

        // Saber el valor de XYZ de un collider
        Debug.Log("Collider bounds X: " + collisionbounds.size.x);
        Debug.Log("Player bounds X: " + playerBounds.size.x);

        // SABER EL TAMA�O DE LA COLISION

        // Obtener el punto m�ximo de los m�nimos de X
        float minX = Mathf.Max(collisionbounds.min.x, playerBounds.min.x);
        Debug.Log("Punto minimo del colider del colision: " + collisionbounds.min.x);
        Debug.Log("Punto minimo del colider del player: " + playerBounds.min.x);
        Debug.Log("M�ximo m�nimo de collision: " + minX);

        // Obtener el punto m�nimo de los m�ximos
        float maxX = Mathf.Min(collisionbounds.max.x, playerBounds.max.x);
        Debug.Log("Punto M�ximo del colider del colision: " + collisionbounds.max.x);
        Debug.Log("Punto M�ximo del colider del player: " + playerBounds.max.x);
        Debug.Log("M�nimo de los m�ximos: " + maxX);

        //SABER EL PUNTO EXACTO DONDE COLISIONA UN OBJETO

        // Al m�nimo sumarle el m�ximo y obtener el total
        // Lo divido por 2 as� estar� en la mitad de la colisi�n o pivote
        // Se lo resto al total de colider del que necesite obtener la posici�n exacta.

        float average = (minX + maxX) / 2 - collisionbounds.min.x;
        Debug.Log("Average: " + average);
    }
}
