using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Collider Bounds obtiene los valores del tamaño de collider BOUNDS == LIMITE 
        Bounds collisionbounds = collision.collider.bounds;
        Bounds playerBounds = gameObject.GetComponent<Collider>().bounds;

        // Saber el valor de XYZ de un collider
        Debug.Log("Collider bounds X: " + collisionbounds.size.x);
        Debug.Log("Player bounds X: " + playerBounds.size.x);

        // SABER EL TAMAÑO DE LA COLISION

        // Obtener el punto máximo de los mínimos de X
        float minX = Mathf.Max(collisionbounds.min.x, playerBounds.min.x);
        Debug.Log("Punto minimo del colider del colision: " + collisionbounds.min.x);
        Debug.Log("Punto minimo del colider del player: " + playerBounds.min.x);
        Debug.Log("Máximo mínimo de collision: " + minX);

        // Obtener el punto mínimo de los máximos
        float maxX = Mathf.Min(collisionbounds.max.x, playerBounds.max.x);
        Debug.Log("Punto Máximo del colider del colision: " + collisionbounds.max.x);
        Debug.Log("Punto Máximo del colider del player: " + playerBounds.max.x);
        Debug.Log("Mínimo de los máximos: " + maxX);

        //SABER EL PUNTO EXACTO DONDE COLISIONA UN OBJETO

        // Al mínimo sumarle el máximo y obtener el total
        // Lo divido por 2 así estaré en la mitad de la colisión o pivote
        // Se lo resto al total de colider del que necesite obtener la posición exacta.

        float average = (minX + maxX) / 2 - collisionbounds.min.x;
        Debug.Log("Average: " + average);
    }
}
