using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    // TODO
    // 스테이지 레벨에 맞춰 몬스터 hp,돈 늘리기
    public static int StageLevel { get; set; } = 1;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;


        Managers.Map.LoadMap(1);

        player = Managers.Resource.Instantiate("Creature/Player");
        player.name = "Player";

        /*Managers.Defender.MakeDefender(Define.DefenderType.Archer, player.transform.position);
        Managers.Defender.MakeDefender(Define.DefenderType.Knight, player.transform.position);
        Managers.Defender.MakeDefender(Define.DefenderType.Mage, player.transform.position);*/

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //gameObject.GetOrAddComponent<CursorController>();

        //GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        //GameObject go = new GameObject { name = "SpawningPool" };
        //SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        //pool.SetKeepMonsterCount(2);
    }

    public void NextStage()
    {
        ++StageLevel;
    }
    public override void Clear()
    {
        
    }
}
