using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : LivingEntity
{
    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target;

    float myCollisionradius;
    float TargetCollisionradius;

    float distanciaSeguridad = 1.5f;
    float NextAttackTime = 0.0f;
    float TimeBetweenAttack = 0.5f;
    bool Atacando = false;
    Material materialInicial;
    Color colorOriginal;

    LivingEntity targetEntity;
    float damage = 1.0f;
    bool bFinalPartida = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Jugador").transform;
        materialInicial = GetComponent<Renderer>().material;
        colorOriginal = materialInicial.color;
        myCollisionradius = GetComponent<CapsuleCollider>().radius;
        TargetCollisionradius = target.GetComponent<CapsuleCollider>().radius;
        targetEntity = target.GetComponent<LivingEntity>();
        t2JugadorController.OnDeathPlayer += FinalPartida;
    }

    void FinalPartida()
    {
        bFinalPartida = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!bFinalPartida)
        {
            if(!Atacando)
            {
                Vector3 dirtoTarget = (target.position - transform.position).normalized;
                Vector3 TargetPosition = target.position - dirtoTarget * (myCollisionradius + TargetCollisionradius + distanciaSeguridad);
                pathFinder.SetDestination(TargetPosition);
                if(Time.time > NextAttackTime)
                {
                    NextAttackTime = Time.time + TimeBetweenAttack;
                    float sqrDsttoTarget = (target.position - transform.position).sqrMagnitude;
                    if(sqrDsttoTarget <= Mathf.Pow(myCollisionradius + TargetCollisionradius + distanciaSeguridad, 2))
                    {
                        Debug.Log("Estoy al lado");
                        StartCoroutine(Attack());
                    }
                }
            }
        }
        
    }

    IEnumerator Attack()
    {
        if(!bFinalPartida)
        {
            pathFinder.enabled = false;
            Atacando = true;
            materialInicial.color = Color.black;
            Vector3 originalPosition = transform.position;
            Vector3 dirtoTarget = (target.position - transform.position).normalized;
            Vector3 AttackPosition = target.position - dirtoTarget * (myCollisionradius +  TargetCollisionradius);
            float percent = 0;
            float attackSpeed = 1;

            bool hasApplieDamage = false;
            while(percent <= 1)
            {
                if(percent >= 0.5f && !hasApplieDamage)
                {
                    targetEntity.TakeDamage(damage);
                    hasApplieDamage = true;
                }
                percent += Time.deltaTime * attackSpeed;
                float interpolacion = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector3.Lerp(originalPosition, AttackPosition, interpolacion);
                yield return null;
            }
            pathFinder.enabled = true;
            Atacando = false;
            materialInicial.color = colorOriginal;
        }
    }
}