using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 10.0f;
    public LayerMask capasDestruir;

    public delegate void OnHitEnemy();
    public static event OnHitEnemy onHitEnemy;

    private float damage = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistancia = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * moveDistancia);
        CheckCollision(moveDistancia);
    }
    void CheckCollision(float moveDistancia)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistancia, capasDestruir, QueryTriggerInteraction.Collide))
        {
            Destroy(gameObject);
            
            if(onHitEnemy != null)
            {
                onHitEnemy();
            }
            IDamageble damagebleObject = hit.collider.GetComponent<IDamageble>();
            if(damagebleObject != null)
            {
                damagebleObject.TakeHit(damage, hit);
            }
        }
    }
}
