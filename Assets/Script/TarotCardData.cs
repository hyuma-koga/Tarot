using UnityEngine;

[CreateAssetMenu(fileName = "NewTarotCard", menuName = "Tarot/Create New Tarot Card")]
public class TarotCardData : ScriptableObject
{
    public int number;        //0～21（タロット番号）
    public string cardName;   //愚者、魔術師など
    public string imageName;  //TheFoolなど
    public Sprite illustration;  //カード画像
    public string meaningUpright;  //正位置の意味
    public string meaningReversed; //逆位置の意味
    public string discriptionUpright;  //正位置の時の説明
    public string discriptionReversed; //逆位置の時の説明
    public bool isReversed;        //今回は逆位置か？

    public string effectDescription;  //プレイヤー向けテキスト（効果説明）

    public AudioClip cardSound;       //カード演出音
    public GameObject visualEffectPrefab;  //カード演出用エフェクト
}

public enum EffectType
{
    MoveForward,
    MoveBackward,
    GainStat,
    LoseStat,
    GlobalEvent,
    PlayerSwap,
    SkipTurn,
    NullEffect
}
