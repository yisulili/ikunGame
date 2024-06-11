using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform taget;
    Vector3 velocity = Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime;
    //�����Χ �±�0Ϊ��Сֵ��1Ϊ���ֵ
    public float[] xLimit = new float[2]; //x�߽��޶�
    public float[] yLimit = new float[2]; //y�߽��޶� 

    private void Awake()
    {
        taget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 tagetPosition = taget.position;
        tagetPosition = new Vector3(Mathf.Clamp(tagetPosition.x, xLimit[0], xLimit[1]), Mathf.Clamp(tagetPosition.y, yLimit[0], yLimit[1]), -10);
        transform.position = Vector3.SmoothDamp(transform.position,tagetPosition,ref velocity,smoothTime);
    }

}
