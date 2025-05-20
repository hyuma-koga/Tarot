#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class TarotCardImporter
{
    [MenuItem("Tools/Import Tarot Cards From JSON")]
    public static void ImportTarotCards()
    {
        string jsonPath = Application.dataPath + "/Resources/TarotCards.json";
        string savePath = "Assets/Resources/TarotCards";

        // データ保存用フォルダを作成
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        // JSON読み込み & 構造確認
        string rawJson = File.ReadAllText(jsonPath).Trim();
        Debug.Log($"📥 JSON読み込み開始: {rawJson}");

        //BOM削除
        if (!string.IsNullOrEmpty(rawJson) && rawJson[0] == '\uFEFF')
            rawJson = rawJson.Substring(1);

        //JsonUtility用に整形(cards:[...]形式でラップ)
        string wrappedJson = "{\"cards\":" + rawJson + "}";

        TarotCardWrapper wrapper = JsonUtility.FromJson<TarotCardWrapper>(wrappedJson);

        if (wrapper == null || wrapper.cards == null || wrapper.cards.Length == 0)
        {
            Debug.LogError("❌ JSONデータの読み込みに失敗しています！");
            return;
        }

        // ScriptableObjectの生成
        foreach (var jsonCard in wrapper.cards)  // ← ここ修正
        {

            Debug.Log($"✅ 読み込み成功: {jsonCard.cardName}, {jsonCard.meaningUpright}, {jsonCard.effectDescription}");


            TarotCardData card = ScriptableObject.CreateInstance<TarotCardData>();

            card.number = jsonCard.number;
            card.cardName = jsonCard.cardName;
            card.imageName = jsonCard.imageName;
            card.meaningUpright = jsonCard.meaningUpright;
            card.meaningReversed = jsonCard.meaningReversed;
            card.effectDescription = jsonCard.effectDescription;
            card.effectValue = jsonCard.effectValue;

            if (System.Enum.TryParse(jsonCard.effectType, out EffectType result))
            {
                card.effectType = result;
            }

            // Resourcesから画像読み込み
            card.illustration = Resources.Load<Sprite>("TarotImages/" + jsonCard.imageName);

            string assetPath = Path.Combine(savePath, $"Tarot_{card.number}_{card.cardName}.asset");

            // 既存アセットがあれば削除
            if (File.Exists(assetPath))
            {
                AssetDatabase.DeleteAsset(assetPath);
            }

            AssetDatabase.CreateAsset(card, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("タロットカードScriptableObject生成完了！");
    }
}

[System.Serializable]
public class TarotCardJsonData
{
    public int number;
    public string cardName;
    public string imageName;
    public string meaningUpright;
    public string meaningReversed;
    public string effectDescription;
    public string effectType;
    public int effectValue;
}

[System.Serializable]
public class TarotCardWrapper
{
    public TarotCardJsonData[] cards;
}
#endif
