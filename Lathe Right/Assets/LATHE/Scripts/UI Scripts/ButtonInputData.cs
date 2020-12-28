using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonInputData", menuName = "__UIPanel__/Data/ButtonInputData")]
public class ButtonInputData : ScriptableObject
{
    #region Data
        private bool m_toggleClicked; // True means button is clicked, False means unclicked
    #endregion

    #region Properties
        public bool ToggleClicked
        {
            get => m_toggleClicked;
            set => m_toggleClicked = value;
        }

    #endregion

    #region CustomMethods
        // Reset variables to default state
        // Call this method each time the game is started 
        // This is necessary since a ScriptableObject will remember the state of the variable when a game is finished
        public void ResetInput()
        {
            m_toggleClicked = false;
        }
    #endregion
}
