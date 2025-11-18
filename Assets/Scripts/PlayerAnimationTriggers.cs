using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{

  private Player m_Player;

  void Awake()
  {
    m_Player = GetComponentInParent<Player>();
  }

  public void CurrentAnimationTrigger()
  {
    m_Player.CallAnimationTrigger();
  }
}