using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footOffset;
    [Range(0, 1)]
    public float rightFootPosWeight = 1;
    [Range(0, 1)]
    public float rightFootRotWeight = 1;
    [Range(0, 1)]
    public float leftFootPosWeight = 1;
    [Range(0, 1)]
    public float leftFootRotWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la animación
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        // Obtener la posición de la pierna derecha
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;

        // Comprobar si se ha producido colisión del pié derecho
        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);
 
        // Si hay colisión
        if (hasHit && hit.collider.gameObject.CompareTag("Suelo"))
        {

            // Se establece la posición del peso y la posición de la pierna derecha en la animación
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            // Se obtiene la rotación de la pierna derecha
            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);

            // Se establece la rotación del peso y la posición de la pierna derecha en la animación
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else // Si no hay colisión
        {
            // Se establece la posición del peso con valor 0
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        // Se repite el proceso para la pierna izquierda

        // Obtener la posición de la pierna izquierda
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        // Comprobar si se ha producido colisión del pié izquierdo
        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);
        
        // Si hay colisión
        if (hasHit && hit.collider.gameObject.CompareTag("Suelo"))
        {
            // Se establece la posición del peso y la posición de la pierna izquierda en la animación
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            // Se obtiene la rotación de la pierna izquierda
            Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);

            // Se establece la rotación del peso y la posición de la pierna izquierda en la animación
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        }
        else // Si no hay colisión
        {
            // Se establece la posición del peso con valor 0
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
