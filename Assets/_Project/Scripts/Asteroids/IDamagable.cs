using UnityEngine;


public interface IDamageable
{
    // -------------------------------------------------------------------------
    // Public Interfaces:
    // ------------------
    //   TakeDamage
    // -------------------------------------------------------------------------

    #region .  TakeDamage()  .
    // -------------------------------------------------------------------------
    //   Interface....:  TakeDamage()
    //   Description..:  
    //   Parameters...:  None
    // -------------------------------------------------------------------------
    public void TakeDamage(int damage, Vector3 hitPosition);
    #endregion


}   // interface IDamageable
