using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-2.0f, 0.0f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(x, y, 0); //设置向左上方的速度
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);  //删除游戏对象
    }
    // Update is called once per frame
    void Update()
    {
        if((this.transform.localPosition.y <= -20)||(this.transform.localPosition.x <= -40)|| (this.transform.localPosition.x >= 20))
            Destroy(this.gameObject);
    }
}
