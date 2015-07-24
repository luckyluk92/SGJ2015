using UnityEngine;
using System.Collections;

public class employee : MonoBehaviour {

	public float productivityDecreaseTime = 1f;
    public float productivityLoss = 0.1f;
    public float flatMoneyGain = 20f;

	private float _timeBetweenGain;

    public float initialProductivity = 1f;
    private float _productivity;

    public float initialStress = 0f;
    private float _stress;

    public Color _productiveColor = new Color(0f, 1f, 0f);
	
    private SpriteRenderer _spriteRenderer;

    public bool IsProductive
    {
        get
        {
            return _productivity > 0;
        }
    }

	// Use this for initialization
	void Start () 
	{
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _productiveColor;
        _productivity = initialProductivity;
        _stress = initialStress;
	}
	
	// Update is called once per frame
	void Update () 
	{
        GainMoney();
        DecreaseProductivity();

        if(_stress == 1f)
        {
            //TODO
        }
	}

	void DecreaseProductivity()
	{
        var newColor = Color.white;
        newColor.g = _productiveColor.g * (1 - _productivity/initialProductivity);
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, newColor, productivityLoss*Time.deltaTime);
		
		if(_productivity > 0)
		{
            _productivity -= productivityLoss*Time.deltaTime;

		}
	}

	void GainMoney()
	{
        _timeBetweenGain += Time.deltaTime;

        if(_timeBetweenGain > productivityDecreaseTime)
        {
            _timeBetweenGain = 0;

            //flatMoneyGain * _productivity/initialProductivity <- formula for money gain
        }
	}
}

