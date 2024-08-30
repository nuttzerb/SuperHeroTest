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

    [SerializeField] private InputField chatInputField;
    private float maxMessage = 25;
    [SerializeField] PlayerChatTextPopup _playerChatTextPopup;

    [SerializeField] int maxCharacter = 20;

    void Start()
    {
        InitChatUI();

    }

    void Update()
    {
        if (chatInputField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatInputField.text);
                chatInputField.text = "";
                chatInputField.ActivateInputField();
                isChatInputFocused = true;
            }
        }
        else
        {

            if (!chatPanel.activeInHierarchy && !chatInputField.isFocused && (Input.GetKeyDown(KeyCode.Return) || (onMousePointer && Input.GetMouseButtonDown(0))))
            {
                chatPanel.SetActive(true);
                chatInputField.interactable = true;
                chatInputField.ActivateInputField();
                isChatInputFocused = true;


            }
            else if (chatPanel.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || (!onMousePointer && Input.GetMouseButtonDown(0))))
            {
                chatPanel.SetActive(false);
                isChatInputFocused = false;
                chatInputField.DeactivateInputField();
                chatInputField.interactable = false;

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
    private void InitChatUI()
    {
        chatPanel.SetActive(false);
        chatInputField.gameObject.SetActive(true);
        chatInputField.interactable = false;
        chatInputField.characterLimit = maxCharacter;
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