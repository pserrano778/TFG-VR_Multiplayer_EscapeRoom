using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class Linterna : MonoBehaviour
{
    public Material material;
    public GameObject luz;
    public GameObject luzHabitacion;
    public GameObject objetoARevelar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!luzHabitacion.active && EstoyEnRango())
        {
            material.SetVector("_LightPosition", luz.GetComponent<Light>().transform.position);
            material.SetVector("_LightDirection", -luz.GetComponent<Light>().transform.forward);
            material.SetFloat("_LightAngle", luz.GetComponent<Light>().spotAngle);
        }
        else
        {
            material.SetVector("_LightPosition", Vector3.zero);
            material.SetVector("_LightDirection", Vector3.zero);
            material.SetFloat("_LightAngle", 0);
        }
    }

    private bool EstoyEnRango()
    {
        return luz.GetComponent<Light>().range >= Vector3.Distance(luz.transform.position, objetoARevelar.transform.position);
    }
}
