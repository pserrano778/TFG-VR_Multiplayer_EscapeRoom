﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable] //Para ver las variables de la clase desde el editor de Unity
public class VRMapOnline
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        // Se obtiene la posición, teniendo en cuenta el offset de posición
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);

        // Se obtiene la rotación, teninendo en cuenta el offset de rotación
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRigOnline : MonoBehaviour
{
    public VRMapOnline head;
    public VRMapOnline rightArm;
    public VRMapOnline leftArm;

    private PhotonView photonView;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            // Se inicializa el valor del offset
            headBodyOffset = transform.position - headConstraint.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Se obtiene la posición, añadiéndole el offset
            transform.position = headConstraint.position + headBodyOffset;

            // Se obtiene la rotación, en el vector up
            transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized; // =headConstraint.up para rotarlo en todos los ejes

            // Se realiza el mapeo de las distintas partes
            head.Map();
            rightArm.Map();
            leftArm.Map();
        }
    }
}
