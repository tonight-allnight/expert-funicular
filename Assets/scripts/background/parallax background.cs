using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//本质就是背景的x位置跟着相机的x位置一起移动


public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;//设置一个可以保存任意游戏组件的变量

    [SerializeField] private float parallaxEffect;//可以自己设置的视差值

    private float xPosition;
    private float length;
    //private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");//全局寻找名字叫Main Camara的组件
        length =GetComponent<SpriteRenderer>().bounds.size.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;//?
        xPosition = transform.position.x;//拿到背景目前的位置用来改，这个值是不变的
                                         //开始的背景的位置
                                         //我设置成了0方便观看
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;//根据MianCam的移动背景才会移动
                                                                         //拿到的使cam的x方向的位置
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);//拿到绑着的组件的位置用来改，改成自己移动的值

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