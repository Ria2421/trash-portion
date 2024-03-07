//==============================================
//Autor:�O����l
//Day:3/5
//BGM����
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g���擾
        bgm = GetComponent<AudioSource>();
        Invoke("PlaySound", 4.0f);
    }

    public void PlaySound()
    {
        //�Đ�
        bgm.Play();
    }
}
