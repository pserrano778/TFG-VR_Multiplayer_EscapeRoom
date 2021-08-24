using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VRAnimatorControllerOnline : MonoBehaviour
{
    private Animator animator;
    private Vector3 previousPos;
    private VRRigOnline vrRig;

    private PhotonView photonView;

    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            animator = GetComponent<Animator>();
            vrRig = GetComponent<VRRigOnline>();
            previousPos = vrRig.head.vrTarget.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (!Exit.GetCompleted())
            {
                // Se calcula la velicidad
                Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;

                // Transforma las coordenadas del mundo en coordenadas locales
                Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
                previousPos = vrRig.head.vrTarget.position;

                // Se obtienen las variables de dirección anteriores
                float previousDirectionX = animator.GetFloat("directionX");
                float previousDirectionY = animator.GetFloat("directionY");

                // Se asignan los nuevos valores a las variables de animación
                animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
                animator.SetFloat("directionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
                animator.SetFloat("directionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
            }
        }
    }
}
