using System.Net.Mime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour, Iinteract
{
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private Dialog dialog;
    [SerializeField] private bool doAction;
    [SerializeField] private ActionEvent _actionEvent;
    [SerializeField] private GameObject _actionObject;
    private DialogController _dialog;
    private Text _dialogText;
    private FixedJoint2D _fixedJoint;
    private int _dialogIndex;

    private void Start() 
    {   
        _dialog = GetComponent<DialogController>();
        _dialogText = _dialogWindow.transform.Find("Text").GetComponent<Text>();
        _fixedJoint = this.GetComponent<FixedJoint2D>();
        _dialogIndex = 0;
    }

    public void Desactive(Transform player)
    {
        
    }

    public void Interact(Transform player)
    {
        if(dialog.dialog.Length > _dialogIndex)
        {
            _dialogWindow.SetActive(true);
            _fixedJoint.enabled = true;
            _fixedJoint.connectedBody = player.GetComponent<Rigidbody2D>();
            StartCoroutine(_dialog.WriteText(dialog.dialog[_dialogIndex++].text ,_dialogText));
        }
        else
        {
            _dialogWindow.SetActive(false);
            _fixedJoint.enabled = false;
            _dialogIndex = 0;
            if(doAction) _actionEvent.DoAction(_actionObject);
        }
    }
}
