using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugTab : MonoBehaviour
{
    [SerializeField] private TMP_Text _isAttackingText;
    [SerializeField] private TMP_Text _isInComboText;
    [SerializeField] private TMP_Text _canEnterComboText;
    [SerializeField] private PlayerCombat _playerCombat;

    private void Update()
    {
        _isAttackingText.text = _playerCombat.IsAttacking.ToString();
        _isInComboText.text = _playerCombat.IsInCombo.ToString();
        _canEnterComboText.text = _playerCombat.CanEnterCombo.ToString();
    }
}