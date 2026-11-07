using TMPro;
using UnityEngine;

public class DamageReadout : MonoBehaviour
{
    private float TimeToTween = 2.5f;
    private Vector3 TargetPoint = Vector3.up;
    private Vector3 _pointToTweenTo;
    private Vector3 _originalPos;
    private float _timePassedSoFar = 0.0f;
    
    private bool _deleteMe = false;

    private TextMeshProUGUI _number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _originalPos = transform.position;
        _pointToTweenTo = _originalPos + TargetPoint;
        _timePassedSoFar = 0f;
        _number = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate elapsed time and whether or not this item should be deleted.
        _timePassedSoFar += Time.deltaTime;
        if (_timePassedSoFar > TimeToTween) { _deleteMe = true; _timePassedSoFar = 1.0f; }
        float _ratio = _timePassedSoFar / TimeToTween;

        //change position using smoothstep. Consider Lerping as well. 
        Vector3 _newPos = new Vector3(Mathf.SmoothStep(_originalPos.x, _pointToTweenTo.x, _ratio), Mathf.SmoothStep(_originalPos.y, _pointToTweenTo.y, _ratio), Mathf.SmoothStep(_originalPos.z, _pointToTweenTo.z, _ratio));
        this.gameObject.transform.position = _newPos;

        //change text alpha using smoothstep. Consider Lerping as well. You could also use the canvas' canvasgroup component to alter alpha. If alpha fade gets debuggy consider that.
        _number.alpha = 1.0f-_ratio;

        //deletion logic
        if (_deleteMe)
        {
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// Sets the initial parameters for this particular readout. Use to set number and customize how long something is shown and where it goes.
    /// </summary>
    /// <param name="_readout">The string that the readout will display. Usually a number of damage dealt/healed.</param>
    /// <param name="_targetPointFromHere">Where the readout will tween to from where it currently is. Default Vector3.up</param>
    /// <param name="_timeToTween">How long it takes for readout to tween to its position and to an alpha of 0. typically 2.5f.</param>
    public void Init(string _readout, Vector3 _targetPointFromHere , float _timeToTween= 2.5f)
    {
        _timePassedSoFar = 0f;
        _originalPos = this.gameObject.transform.position;
        _pointToTweenTo = _originalPos + _targetPointFromHere;
        TimeToTween = _timeToTween;
        if (_number == null) { _number = this.gameObject.GetComponentInChildren<TextMeshProUGUI>(); }
        _number.text = _readout;
    }

    //Makes the text green. Useful for conveying healing. 
    public void Greenify() {
        _number.faceColor = new Color32(0, 255, 0, 255);
    }
}
