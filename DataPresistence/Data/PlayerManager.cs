using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // Dictionary เก็บข้อมูล PlayerStatus ของแต่ละ player โดย key เป็น playerId
    private Dictionary<int, PlayerStatus> players = new Dictionary<int, PlayerStatus>();

    private void Awake()
    {
        instance = this;
    }

    // เมื่อมี player เข้ามาในเกมใหม่ จะเรียกใช้เมธอดนี้เพื่อเพิ่ม PlayerStatus ของ player เข้าไปใน Dictionary
    public void AddPlayer(PlayerStatus player)
    {
        if (!players.ContainsKey(player.playerId))
        {
            players.Add(player.playerId, player);
        }
    }

    // เมื่อ player หนึ่งๆ ออกจากเกม จะเรียกใช้เมธอดนี้เพื่อลบ PlayerStatus ของ player ออกจาก Dictionary
    public void RemovePlayer(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            players.Remove(playerId);
        }
    }

    // เมื่อต้องการเข้าถึง PlayerStatus ของ player ใดๆ ในเกม จะเรียกใช้เมธอดนี้
    public PlayerStatus GetPlayer(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            return players[playerId];
        }
        return null;
    }
}
