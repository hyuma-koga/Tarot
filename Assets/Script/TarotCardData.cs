using UnityEngine;

[CreateAssetMenu(fileName = "NewTarotCard", menuName = "Tarot/Create New Tarot Card")]
public class TarotCardData : ScriptableObject
{
    public int number;        //0�`21�i�^���b�g�ԍ��j
    public string cardName;   //���ҁA���p�t�Ȃ�
    public string imageName;  //TheFool�Ȃ�
    public Sprite illustration;  //�J�[�h�摜
    public string meaningUpright;  //���ʒu�̈Ӗ�
    public string meaningReversed; //�t�ʒu�̈Ӗ�
    public string discriptionUpright;  //���ʒu�̎��̐���
    public string discriptionReversed; //�t�ʒu�̎��̐���
    public bool isReversed;        //����͋t�ʒu���H

    public string effectDescription;  //�v���C���[�����e�L�X�g�i���ʐ����j

    public AudioClip cardSound;       //�J�[�h���o��
    public GameObject visualEffectPrefab;  //�J�[�h���o�p�G�t�F�N�g
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
