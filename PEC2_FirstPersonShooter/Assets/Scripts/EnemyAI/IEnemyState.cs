using UnityEngine;

public interface IEnemyState
{
    void EnterState();
    void UpdateState();
    void ExitState();

    void OnTriggerEnter(Collider col);
    void OnTriggerStay(Collider col);
    void OnTriggerExit(Collider col);

    void GetHit();
}
