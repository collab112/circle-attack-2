using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour
{

	public Text cashText;
	public int cash = 70;
	
	void Awake() {
		
		cashText.text = "CASH: " + cash.ToString();
		
	}
	
	public bool updateCash(int cashUpdate) {

		bool affordable;
		
		if ( cash + cashUpdate >= 0) {
		
			cash += cashUpdate;
			cashText.text = "CASH: " + cash.ToString();

			affordable = true;
		
		} else {

			affordable = false;

		}

		return affordable;
		
	}
	
}
