using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour
{
    public static bool isChatInputFocused = false;
    public static bool onMousePointer = false;

    [SerializeField] private List<Message> messageList = new List<Message>();

    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject chatContent;
    [SerializeField] private GameObject textObject;

    [SerializeField] private InputField chatInput;
    private float maxMessage = 25;
    [SerializeField] PlayerChatTextPopup _playerChatTextPopup;

    void Start()
    {
        chatPanel.SetActive(false);
        chatInput.gameObject.SetActive(true);
    }
    void Update()
    {
        if (chatInput.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatInput.text);
                chatInput.text = "";
                chatInput.ActivateInputField();
                isChatInputFocused = true;
            }
        }
        else
        {

            if (!chatPanel.activeInHierarchy && !chatInput.isFocused && (Input.GetKeyDown(KeyCode.Return) || (onMousePointer && Input.GetMouseButtonDown(0))))
            {
                chatPanel.SetActive(true);
                chatInput.ActivateInputField();
                isChatInputFocused = true;

            }
            else if (chatPanel.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || (!onMousePointer && Input.GetMouseButtonDown(0))))
            {
                chatPanel.SetActive(false);
                chatInput.DeactivateInputField();
                isChatInputFocused = false;

            }
        }


    }
    public void OnPointerEnter()
    {
        onMousePointer = true;
    }

    public void OnPointerExit()
    {
        onMousePointer = false;


    }
    public void SendMessageToChat(String text)
    {
        if (messageList.Count >= maxMessage)
        {
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatContent.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);

        _playerChatTextPopup.InitTextPopup(text);

    }
}
public class Message
{
    public Text textObject;
    public string text;
}