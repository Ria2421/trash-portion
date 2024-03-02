//
//�{�^���X�N���v�g
//Author�F���{�S��
//Date:2/21
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; //DOTween�g�p�ɕK�v
using System.Text;
using System.Linq;
using System.Net.Sockets;

public class ButtonPushScript :
MonoBehaviour,
IPointerEnterHandler,
    IPointerExitHandler
{
    CanvasGroup canvasGroup;
    Image image;
    Text text;
    Button button;

    [SerializeField] AudioClip ButtonSE;

    AudioSource buttonSE;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        text = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        button = GetComponent<Button>();
        buttonSE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonPushSE();
    }

    /// <summary>
    /// Button�Ƀ}�E�X�J�[�\���ɒu���ꂽ��
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1.2f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
    }
    /// <summary>
    /// Button����}�E�X�J�[�\�������ꂽ��
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
    }

    //�{�^������������
    async void ButtonPushSE()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // ���M����
            string json = "1";
            byte[] buffer = Encoding.UTF8.GetBytes(json);                      // JSON��byte�ɕϊ�
            buffer = buffer.Prepend((byte)EventID.InGameFlag).ToArray();       // ���M�f�[�^�̐擪�ɃC�x���gID��t�^
            NetworkStream stream = NetworkManager.MyTcpClient.GetStream();
            await stream.WriteAsync(buffer, 0, buffer.Length);                 // JSON���M����
#if DEBUG
            Debug.Log("�C���Q�[�����M");
#endif
        }
    }
        
}
