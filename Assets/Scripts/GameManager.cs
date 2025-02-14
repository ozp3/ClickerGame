using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Genel Değişkenler
    public long xp = 0;  // Artık long
    public double availableMoney = 0; // double kullandık
    public int normalHitValue = 1;
    public float criticalChance = 1f;
    public int criticalUpgradeLevel = 0;
    public int normalHitUpgradeLevel = 0;

    // Pasif Gelir – Tier 1
    public int passiveIncomeLevel1 = 0;
    public float passiveIncomeUpgradeCost1 = 5f;

    // Tier 2
    public int passiveIncomeLevel2 = 0;
    public float passiveIncomeUpgradeCost2 = 100f;

    // Tier 3
    public int passiveIncomeLevel3 = 0;
    public float passiveIncomeUpgradeCost3 = 2000f;

    // Tier 4
    public int passiveIncomeLevel4 = 0;
    public float passiveIncomeUpgradeCost4 = 40000f;

    // Tier 5
    public int passiveIncomeLevel5 = 0;
    public float passiveIncomeUpgradeCost5 = 800000f;

    // Tier 6
    public int passiveIncomeLevel6 = 0;
    public float passiveIncomeUpgradeCost6 = 16000000f;

    // Tier 7
    public int passiveIncomeLevel7 = 0;
    public float passiveIncomeUpgradeCost7 = 320000000f;

    // Tier 8
    public int passiveIncomeLevel8 = 0;
    public float passiveIncomeUpgradeCost8 = 6400000000f;

    // Tier 9
    public int passiveIncomeLevel9 = 0;
    public float passiveIncomeUpgradeCost9 = 128000000000f;

    // Tier 10
    public int passiveIncomeLevel10 = 0;
    public float passiveIncomeUpgradeCost10 = 2560000000000f;

    // UI Referansları
    public TMP_Text xpText;
    public TMP_Text moneyText;
    public TMP_Text critUpgradeButtonText;
    public TMP_Text clickUpgradeButtonText;

    // Her Tier için buton metinleri ve butonlar:
    public TMP_Text passiveIncomeUpgradeButtonText1;
    public Button passiveIncomeUpgradeButton1;

    public TMP_Text passiveIncomeUpgradeButtonText2;
    public Button passiveIncomeUpgradeButton2;

    public TMP_Text passiveIncomeUpgradeButtonText3;
    public Button passiveIncomeUpgradeButton3;

    public TMP_Text passiveIncomeUpgradeButtonText4;
    public Button passiveIncomeUpgradeButton4;

    public TMP_Text passiveIncomeUpgradeButtonText5;
    public Button passiveIncomeUpgradeButton5;

    public TMP_Text passiveIncomeUpgradeButtonText6;
    public Button passiveIncomeUpgradeButton6;

    public TMP_Text passiveIncomeUpgradeButtonText7;
    public Button passiveIncomeUpgradeButton7;

    public TMP_Text passiveIncomeUpgradeButtonText8;
    public Button passiveIncomeUpgradeButton8;

    public TMP_Text passiveIncomeUpgradeButtonText9;
    public Button passiveIncomeUpgradeButton9;

    public TMP_Text passiveIncomeUpgradeButtonText10;
    public Button passiveIncomeUpgradeButton10;

    public GameObject plusOnePrefab;
    public RectTransform canvasRect;
    public Button clickButton;
    public Button critUpgradeButton;
    public Button clickUpgradeButton;

    private float lastClickTime = 0f;
    private float clickCooldown = 0.1f;
    private float lastPassiveIncomeTime = 0f;

    private float critUpgradeCost = 1f;

    // Sayı biçimlendirme fonksiyonu (double kullanıyoruz)
    string FormatNumber(double num)
    {
        if (num < 1000)
        {
            if (num % 1 == 0)
                return num.ToString("0");
            else
                return num.ToString("0.00");
        }
        if (num >= 1e15)
            return (num / 1e15).ToString("0.##") + "Q";
        if (num >= 1e12)
            return (num / 1e12).ToString("0.##") + "T";
        if (num >= 1e9)
            return (num / 1e9).ToString("0.##") + "B";
        if (num >= 1e6)
            return (num / 1e6).ToString("0.##") + "M";
        if (num >= 1e3)
            return (num / 1e3).ToString("0.##") + "K";

        return num.ToString("0.00");
    }

    // Yardımcı: 1000 üzeri üstel hesaplama
    long Pow1000(int exponent)
    {
        long result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= 1000L;
        }
        return result;
    }

    // Genel yield hesaplaması: base değeri, passive level
    // yield = base * (1000^segments) * remainder, 
    // burada segments = (level - 1) / 100, remainder = level - segments * 100
    long CalculateXPYield(int level, long baseXP)
    {
        int segments = (level - 1) / 100;
        int remainder = level - segments * 100;
        return baseXP * Pow1000(segments) * remainder;
    }

    double CalculateMoneyYield(int level, double baseMoney)
    {
        int segments = (level - 1) / 100;
        int remainder = level - segments * 100;
        return baseMoney * Pow1000(segments) * remainder;
    }

    void Start()
    {
        // Başlangıç seviyelerini sıfırlıyoruz
        passiveIncomeLevel1 = 0;
        passiveIncomeLevel2 = 0;
        passiveIncomeLevel3 = 0;
        passiveIncomeLevel4 = 0;
        passiveIncomeLevel5 = 0;
        passiveIncomeLevel6 = 0;
        passiveIncomeLevel7 = 0;
        passiveIncomeLevel8 = 0;
        passiveIncomeLevel9 = 0;
        passiveIncomeLevel10 = 0;

        UpdateUI();

        // Buton tıklama event’leri
        clickButton.onClick.AddListener(() => TryClick(Input.mousePosition));
        critUpgradeButton.onClick.AddListener(BuyCritUpgrade);
        clickUpgradeButton.onClick.AddListener(BuyClickUpgrade);

        passiveIncomeUpgradeButton1.onClick.AddListener(BuyPassiveIncomeUpgrade1);
        passiveIncomeUpgradeButton2.onClick.AddListener(BuyPassiveIncomeUpgrade2);
        passiveIncomeUpgradeButton3.onClick.AddListener(BuyPassiveIncomeUpgrade3);
        passiveIncomeUpgradeButton4.onClick.AddListener(BuyPassiveIncomeUpgrade4);
        passiveIncomeUpgradeButton5.onClick.AddListener(BuyPassiveIncomeUpgrade5);
        passiveIncomeUpgradeButton6.onClick.AddListener(BuyPassiveIncomeUpgrade6);
        passiveIncomeUpgradeButton7.onClick.AddListener(BuyPassiveIncomeUpgrade7);
        passiveIncomeUpgradeButton8.onClick.AddListener(BuyPassiveIncomeUpgrade8);
        passiveIncomeUpgradeButton9.onClick.AddListener(BuyPassiveIncomeUpgrade9);
        passiveIncomeUpgradeButton10.onClick.AddListener(BuyPassiveIncomeUpgrade10);
    }

    void Update()
    {
        if (Time.time - lastPassiveIncomeTime >= 1f)
        {
            lastPassiveIncomeTime = Time.time;
            long totalXPGain = 0;

            // Tier 1 (base: 100 xp, 1 money)
            if (passiveIncomeLevel1 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel1, 100L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel1, 1.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 2 (base: 1000 xp, 10 money)
            if (passiveIncomeLevel2 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel2, 1000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel2, 10.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 3 (base: 10000 xp, 100 money)
            if (passiveIncomeLevel3 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel3, 10000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel3, 100.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 4 (base: 100000 xp, 1000 money)
            if (passiveIncomeLevel4 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel4, 100000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel4, 1000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 5 (base: 1000000 xp, 10000 money)
            if (passiveIncomeLevel5 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel5, 1000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel5, 10000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 6 (base: 10000000 xp, 100000 money)
            if (passiveIncomeLevel6 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel6, 10000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel6, 100000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 7 (base: 100000000 xp, 1000000 money)
            if (passiveIncomeLevel7 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel7, 100000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel7, 1000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 8 (base: 1000000000 xp, 10000000 money)
            if (passiveIncomeLevel8 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel8, 1000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel8, 10000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 9 (base: 10000000000 xp, 100000000 money)
            if (passiveIncomeLevel9 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel9, 10000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel9, 100000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            // Tier 10 (base: 100000000000 xp, 1000000000 money)
            if (passiveIncomeLevel10 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel10, 100000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel10, 1000000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
            }

            if (totalXPGain > 0)
            {
                // Tüm passive income'ların toplam XP kazanımını yeşil renkte tek bir efekt olarak göster.
                ShowPassiveIncomeEffect(totalXPGain, Color.green);
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        xpText.text = "XP: " + FormatNumber(xp);
        moneyText.text = "Money: " + FormatNumber(availableMoney);

        critUpgradeButtonText.text = $"Crit Rate Upgrade (Lvl {criticalUpgradeLevel})\nCost: {FormatNumber(critUpgradeCost)}";
        float clickUpgradeCost = Mathf.Pow(1.1f, normalHitUpgradeLevel);
        clickUpgradeButtonText.text = $"Click Upgrade (Lvl {normalHitUpgradeLevel})\nCost: {FormatNumber(clickUpgradeCost)}";

        // Buton metinlerinde "Money" kelimesi kaldırıldı
        passiveIncomeUpgradeButtonText1.text = $"Income (T1) (Lvl {passiveIncomeLevel1})\nCost: {FormatNumber(passiveIncomeUpgradeCost1)}";
        passiveIncomeUpgradeButtonText2.text = $"Income (T2) (Lvl {passiveIncomeLevel2})\nCost: {FormatNumber(passiveIncomeUpgradeCost2)}";
        passiveIncomeUpgradeButtonText3.text = $"Income (T3) (Lvl {passiveIncomeLevel3})\nCost: {FormatNumber(passiveIncomeUpgradeCost3)}";
        passiveIncomeUpgradeButtonText4.text = $"Income (T4) (Lvl {passiveIncomeLevel4})\nCost: {FormatNumber(passiveIncomeUpgradeCost4)}";
        passiveIncomeUpgradeButtonText5.text = $"Income (T5) (Lvl {passiveIncomeLevel5})\nCost: {FormatNumber(passiveIncomeUpgradeCost5)}";
        passiveIncomeUpgradeButtonText6.text = $"Income (T6) (Lvl {passiveIncomeLevel6})\nCost: {FormatNumber(passiveIncomeUpgradeCost6)}";
        passiveIncomeUpgradeButtonText7.text = $"Income (T7) (Lvl {passiveIncomeLevel7})\nCost: {FormatNumber(passiveIncomeUpgradeCost7)}";
        passiveIncomeUpgradeButtonText8.text = $"Income (T8) (Lvl {passiveIncomeLevel8})\nCost: {FormatNumber(passiveIncomeUpgradeCost8)}";
        passiveIncomeUpgradeButtonText9.text = $"Income (T9) (Lvl {passiveIncomeLevel9})\nCost: {FormatNumber(passiveIncomeUpgradeCost9)}";
        passiveIncomeUpgradeButtonText10.text = $"Income (T10) (Lvl {passiveIncomeLevel10})\nCost: {FormatNumber(passiveIncomeUpgradeCost10)}";
    }

    void TryClick(Vector3 clickPosition)
    {
        if (Time.time - lastClickTime < clickCooldown) return;
        lastClickTime = Time.time;
        ShowClickEffect(clickPosition);
    }

    void ShowClickEffect(Vector3 clickPosition)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, clickPosition, null, out anchoredPos);
        float randomOffsetX = Random.Range(-50f, 50f);
        float randomOffsetY = Random.Range(-50f, 50f);
        anchoredPos += new Vector2(randomOffsetX, randomOffsetY);

        int gainedXP = normalHitValue;
        int randomChance = Random.Range(0, 100);
        if (randomChance < criticalChance)
        {
            gainedXP *= 2;
            CreateTextEffect(plusOnePrefab, "+" + FormatNumber(gainedXP), anchoredPos, Color.red);
        }
        else
        {
            CreateTextEffect(plusOnePrefab, "+" + FormatNumber(gainedXP), anchoredPos, Color.white);
        }
        xp += gainedXP;
        availableMoney += gainedXP * 0.01;
        UpdateUI();
    }

    void ShowPassiveIncomeEffect(long passiveXP, Color effectColor)
    {
        Vector2 randomPos = new Vector2(Random.Range(-200f, 200f), Random.Range(-100f, 100f));
        CreateTextEffect(plusOnePrefab, "+" + FormatNumber(passiveXP), randomPos, effectColor);
    }

    void CreateTextEffect(GameObject prefab, string message, Vector2 position, Color color)
    {
        GameObject textEffect = Instantiate(prefab, canvasRect);
        RectTransform rect = textEffect.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        TMP_Text text = textEffect.GetComponent<TMP_Text>();
        text.text = message;
        text.color = color;
        text.raycastTarget = false;
        rect.SetAsFirstSibling();
        StartCoroutine(FadeAndMoveUp(textEffect));
    }

    IEnumerator FadeAndMoveUp(GameObject textEffect)
    {
        TMP_Text text = textEffect.GetComponent<TMP_Text>();
        RectTransform rect = textEffect.GetComponent<RectTransform>();
        float duration = 1f;
        float elapsedTime = 0f;
        Vector2 startPos = rect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, 50);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            Color color = text.color;
            color.a = Mathf.Lerp(1, 0, t);
            text.color = color;
            yield return null;
        }
        Destroy(textEffect);
    }

    void BuyCritUpgrade()
    {
        if (availableMoney >= critUpgradeCost && criticalChance < 20f)
        {
            availableMoney -= critUpgradeCost;
            critUpgradeCost *= 5f;
            criticalChance += 0.5f;
            criticalUpgradeLevel++;
            UpdateUI();
        }
    }

    void BuyClickUpgrade()
    {
        float cost = Mathf.Pow(1.1f, normalHitUpgradeLevel);
        if (availableMoney >= cost)
        {
            availableMoney -= cost;
            normalHitValue++;
            normalHitUpgradeLevel++;
            UpdateUI();
        }
    }

    // Tier 1 Satın Alma
    void BuyPassiveIncomeUpgrade1()
    {
        if (availableMoney >= passiveIncomeUpgradeCost1)
        {
            availableMoney -= passiveIncomeUpgradeCost1;
            passiveIncomeUpgradeCost1 *= 1.1f;  // %10 artış
            passiveIncomeLevel1++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade2()
    {
        if (availableMoney >= passiveIncomeUpgradeCost2)
        {
            availableMoney -= passiveIncomeUpgradeCost2;
            passiveIncomeUpgradeCost2 *= 1.1f;  // %10 artış
            passiveIncomeLevel2++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade3()
    {
        if (availableMoney >= passiveIncomeUpgradeCost3)
        {
            availableMoney -= passiveIncomeUpgradeCost3;
            passiveIncomeUpgradeCost3 *= 1.1f;  // %10 artış
            passiveIncomeLevel3++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade4()
    {
        if (availableMoney >= passiveIncomeUpgradeCost4)
        {
            availableMoney -= passiveIncomeUpgradeCost4;
            passiveIncomeUpgradeCost4 *= 1.1f;  // %10 artış
            passiveIncomeLevel4++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade5()
    {
        if (availableMoney >= passiveIncomeUpgradeCost5)
        {
            availableMoney -= passiveIncomeUpgradeCost5;
            passiveIncomeUpgradeCost5 *= 1.1f;  // %10 artış
            passiveIncomeLevel5++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade6()
    {
        if (availableMoney >= passiveIncomeUpgradeCost6)
        {
            availableMoney -= passiveIncomeUpgradeCost6;
            passiveIncomeUpgradeCost6 *= 1.1f;  // %10 artış
            passiveIncomeLevel6++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade7()
    {
        if (availableMoney >= passiveIncomeUpgradeCost7)
        {
            availableMoney -= passiveIncomeUpgradeCost7;
            passiveIncomeUpgradeCost7 *= 1.1f;  // %10 artış
            passiveIncomeLevel7++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade8()
    {
        if (availableMoney >= passiveIncomeUpgradeCost8)
        {
            availableMoney -= passiveIncomeUpgradeCost8;
            passiveIncomeUpgradeCost8 *= 1.1f;  // %10 artış
            passiveIncomeLevel8++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade9()
    {
        if (availableMoney >= passiveIncomeUpgradeCost9)
        {
            availableMoney -= passiveIncomeUpgradeCost9;
            passiveIncomeUpgradeCost9 *= 1.1f;  // %10 artış
            passiveIncomeLevel9++;
            UpdateUI();
        }
    }

    void BuyPassiveIncomeUpgrade10()
    {
        if (availableMoney >= passiveIncomeUpgradeCost10)
        {
            availableMoney -= passiveIncomeUpgradeCost10;
            passiveIncomeUpgradeCost10 *= 1.1f;  // %10 artış
            passiveIncomeLevel10++;
            UpdateUI();
        }
    }
}
