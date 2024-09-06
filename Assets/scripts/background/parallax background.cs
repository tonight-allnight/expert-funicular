using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ʾ��Ǳ�����xλ�ø��������xλ��һ���ƶ�


public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;//����һ�����Ա���������Ϸ����ı���

    [SerializeField] private float parallaxEffect;//�����Լ����õ��Ӳ�ֵ

    private float xPosition;
    private float length;
    //private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");//ȫ��Ѱ�����ֽ�Main Camara�����
        length =GetComponent<SpriteRenderer>().bounds.size.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;//?
        xPosition = transform.position.x;//�õ�����Ŀǰ��λ�������ģ����ֵ�ǲ����
                                         //��ʼ�ı�����λ��
                                         //�����ó���0����ۿ�
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;//����MianCam���ƶ������Ż��ƶ�
                                                                         //�õ���ʹcam��x�����λ��
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);//�õ����ŵ������λ�������ģ��ĳ��Լ��ƶ���ֵ

        if (distanceMoved > xPosition + length)
        {

            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;

        }
    }
}