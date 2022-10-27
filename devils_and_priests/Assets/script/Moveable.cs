using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MyNamespace
{
    public class Moveable : MonoBehaviour
    {
        readonly float speed = 20;

        int flag;
        Vector3 middle;
        Vector3 destination;

        public void SetDestination(Vector3 position)
        {
            destination = position;
            middle = position;
            if (position.y == transform.position.y)
            {          // �ƶ���
                flag = 2;
            }
            else if (position.y < transform.position.y)
            {    // ����ɫ�Ӱ����Ƶ�����
                position.y = transform.position.y;
            }
            else
            {                                      // ����ɫ�Ӵ����Ƶ�����
                position.x = transform.position.x;
            }
            flag = 1;
        }

        void Update()
        {
            if (flag == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                if (transform.position == destination)
                {
                    flag = 0;
                }
            }
            else if (flag == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                if (transform.position == middle)
                {
                    flag = 2;
                }
            }

        }

        public void Reset()
        {
            flag = 0;
        }
    }
}
