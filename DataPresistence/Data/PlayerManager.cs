using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // Dictionary �红����� PlayerStatus �ͧ���� player �� key �� playerId
    private Dictionary<int, PlayerStatus> players = new Dictionary<int, PlayerStatus>();

    private void Awake()
    {
        instance = this;
    }

    // ������� player ������������ �����¡�����ʹ����������� PlayerStatus �ͧ player ����� Dictionary
    public void AddPlayer(PlayerStatus player)
    {
        if (!players.ContainsKey(player.playerId))
        {
            players.Add(player.playerId, player);
        }
    }

    // ����� player ˹��� �͡�ҡ�� �����¡�����ʹ�������ź PlayerStatus �ͧ player �͡�ҡ Dictionary
    public void RemovePlayer(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            players.Remove(playerId);
        }
    }

    // ����͵�ͧ�����Ҷ֧ PlayerStatus �ͧ player �� ��� �����¡�����ʹ���
    public PlayerStatus GetPlayer(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            return players[playerId];
        }
        return null;
    }
}
