using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portion : MonoBehaviour
{
     Rigidbody rb;
    //�����͂̑傫�����`
    float forceMagnitude = 12.0f;
    float rand;

    //���Ƀ|�[�V�������ˏo
    public void SlowLeft()
    {
        rand = Random.Range(0.5f, 2.5f);

        //45�x�̊p�x�Ń|�[�V�������ˏo
        Vector3 forceDirection = new Vector3(-1.0f * rand, 1.0f * rand, 0f);

        // �����Ƒ傫������|�[�V�����ɉ����͂��v�Z����
        Vector3 force = forceMagnitude * forceDirection;

        rb = GetComponent<Rigidbody>();

        rb.AddForce(force, ForceMode.Impulse);          //ForceMode.Impulse�͌���
    }

    //�E�Ƀ|�[�V�������ˏo
    public void SlowRight()
    {
        rand = Random.Range(0.5f, 2.5f);

        //45�x�̊p�x�Ń|�[�V�������ˏo
        Vector3 forceDirection = new Vector3(1.0f * rand, 1.0f * rand, 0f);

        // �����Ƒ傫������|�[�V�����ɉ����͂��v�Z����
        Vector3 force = forceMagnitude * forceDirection;

        rb = GetComponent<Rigidbody>();

        rb.AddForce(force, ForceMode.Impulse);          //ForceMode.Impulse�͌���
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(gameObject);
        }
    }
}
