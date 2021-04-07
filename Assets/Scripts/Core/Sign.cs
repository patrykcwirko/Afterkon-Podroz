using System.Net.Mime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour, Iinteract
{

    [SerializeField] private bool doAction;
    [SerializeField] private ActionEvent _actionEvent;
    [SerializeField] private GameObject _actionObject;
    [SerializeField] private Activator activator;
    private DialogController _dialog;
    private Text _dialogText;
    private FixedJoint2D _fixedJoint;
    private int _dialogIndex;

    private void Start() 
    {   
        _dialog = GetComponent<DialogController>();
        _dialogText = _dialog.dialogWindow.transform.Find("Text").GetComponent<Text>();
        _fixedJoint = this.GetComponent<FixedJoint2D>();
        _dialogIndex = 0;
        if(activator) activator.onTriger += Activator_onTriger;
    }

    private void Activator_onTriger(object sender, Activator.onActiveEventArgs e)
    {
        if(e.collision.tag == "Player")
        {
            Interact(e.collision.transform);
        }
    }

    public void Desactive(Transform player)
    {
        
    }

    public void Interact(Transform player)
    {
        if(_dialog.dialog.dialog.Count > _dialogIndex)
        {
            _dialog.dialogWindow.SetActive(true);
            _fixedJoint.enabled = true;
            _fixedJoint.connectedBody = player.GetComponent<Rigidbody2D>();
            StartCoroutine(_dialog.WriteText(_dialogIndex++ ,_dialogText));
        }
        else
        {
            _dialog.dialogWindow.SetActive(false);
            _fixedJoint.enabled = false;
            _dialogIndex = 0;
            if(doAction) _actionEvent.DoAction(_actionObject);
        }
        player.GetComponent<Player.PlayerInput>().states.interactable = false;
    }
}
