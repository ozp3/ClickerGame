using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Genel Değişkenler
    public double xp = 0;                  // xp artık double
    public double availableMoney = 0;      // double kullandık

    // Click (aktif) gelir için:
    public int normalHitUpgradeLevel = 0;  // Click upgrade seviyesi
    // (Başlangıç değeri artık 1 olarak kabul ediliyor.)

    // Crit upgrade için:
    public float critUpgradeCost = 1f;     // İlk crit upgrade maliyeti
    public int criticalUpgradeLevel = 0;   // Crit upgrade seviyesi
    public float criticalBase = 1f;        // Başlangıç krit oranı (%1)
    public float criticalChance = 1f;      // Güncel krit oranı (UI'da hesaplanacak)

    // Pasif Gelir – Tierler (diğer bölümler aynı)
    public int passiveIncomeLevel1 = 0;
    public float passiveIncomeUpgradeCost1 = 5f;
    public int passiveIncomeLevel2 = 0;
    public float passiveIncomeUpgradeCost2 = 100f;
    public int passiveIncomeLevel3 = 0;
    public float passiveIncomeUpgradeCost3 = 2000f;
    public int passiveIncomeLevel4 = 0;
    public float passiveIncomeUpgradeCost4 = 40000f;
    public int passiveIncomeLevel5 = 0;
    public float passiveIncomeUpgradeCost5 = 800000f;
    public int passiveIncomeLevel6 = 0;
    public float passiveIncomeUpgradeCost6 = 16000000f;
    public int passiveIncomeLevel7 = 0;
    public float passiveIncomeUpgradeCost7 = 320000000f;
    public int passiveIncomeLevel8 = 0;
    public float passiveIncomeUpgradeCost8 = 6400000000f;
    public int passiveIncomeLevel9 = 0;
    public float passiveIncomeUpgradeCost9 = 128000000000f;
    public int passiveIncomeLevel10 = 0;
    public float passiveIncomeUpgradeCost10 = 2560000000000f;

    // UI Referansları
    public TMP_Text xpText;
    public TMP_Text moneyText;
    public TMP_Text critUpgradeButtonText;
    public TMP_Text clickUpgradeButtonText;
    public TMP_Text statsText;  // İstatistikler için

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

    // İstatistikler
    public float totalTimePlayed = 0f;
    public int totalClicks = 0;
    public double totalClickIncome = 0;
    public double totalPassiveIncome = 0;

    private float lastClickTime = 0f;
    private float clickCooldown = 0.1f;
    private float lastPassiveIncomeTime = 0f;

    // ---------------------------
    // Yardımcı Fonksiyonlar:
    string FormatNumber(double num)
    {
        // 1e3'e kadar
        if (num < 1e3)
        {
            if (num % 1 == 0)
                return num.ToString("0");
            else
                return num.ToString("0.00");
        }
        // 1e3 - 1e6 arası: K
        else if (num < 1e6)
        {
            double result = num / 1e3;
            if (result % 1 == 0)
                return result.ToString("0") + "K";
            else
                return result.ToString("0.00") + "K";
        }
        // 1e6 - 1e9 arası: M
        else if (num < 1e9)
        {
            double result = num / 1e6;
            if (result % 1 == 0)
                return result.ToString("0") + "M";
            else
                return result.ToString("0.00") + "M";
        }
        // 1e9 - 1e12 arası: B
        else if (num < 1e12)
        {
            double result = num / 1e9;
            if (result % 1 == 0)
                return result.ToString("0") + "B";
            else
                return result.ToString("0.00") + "B";
        }
        // 1e12 - 1e15 arası: T
        else if (num < 1e15)
        {
            double result = num / 1e12;
            if (result % 1 == 0)
                return result.ToString("0") + "T";
            else
                return result.ToString("0.00") + "T";
        }
        // 1e15 - 1e18 arası: Q
        else if (num < 1e18)
        {
            double result = num / 1e15;
            if (result % 1 == 0)
                return result.ToString("0") + "Q";
            else
                return result.ToString("0.00") + "Q";
        }
        // 1e18 - 1e21: Qi
        else if (num < 1e21)
        {
            double result = num / 1e18;
            if (result % 1 == 0)
                return result.ToString("0") + "Qi";
            else
                return result.ToString("0.00") + "Qi";
        }
        // 1e21 - 1e24: Sx
        else if (num < 1e24)
        {
            double result = num / 1e21;
            if (result % 1 == 0)
                return result.ToString("0") + "Sx";
            else
                return result.ToString("0.00") + "Sx";
        }
        // 1e24 - 1e27: Sp
        else if (num < 1e27)
        {
            double result = num / 1e24;
            if (result % 1 == 0)
                return result.ToString("0") + "Sp";
            else
                return result.ToString("0.00") + "Sp";
        }
        // 1e27 - 1e30: Oc
        else if (num < 1e30)
        {
            double result = num / 1e27;
            if (result % 1 == 0)
                return result.ToString("0") + "Oc";
            else
                return result.ToString("0.00") + "Oc";
        }
        // 1e30 - 1e33: Nn
        else if (num < 1e33)
        {
            double result = num / 1e30;
            if (result % 1 == 0)
                return result.ToString("0") + "Nn";
            else
                return result.ToString("0.00") + "Nn";
        }
        // 1e33 - 1e36: X1
        else if (num < 1e36)
        {
            double result = num / 1e33;
            if (result % 1 == 0)
                return result.ToString("0") + "X1";
            else
                return result.ToString("0.00") + "X1";
        }
        // 1e36 - 1e39: X2
        else if (num < 1e39)
        {
            double result = num / 1e36;
            if (result % 1 == 0)
                return result.ToString("0") + "X2";
            else
                return result.ToString("0.00") + "X2";
        }
        // 1e39 - 1e42: X3
        else if (num < 1e42)
        {
            double result = num / 1e39;
            if (result % 1 == 0)
                return result.ToString("0") + "X3";
            else
                return result.ToString("0.00") + "X3";
        }
        // 1e42 - 1e45: X4
        else if (num < 1e45)
        {
            double result = num / 1e42;
            if (result % 1 == 0)
                return result.ToString("0") + "X4";
            else
                return result.ToString("0.00") + "X4";
        }
        else
        {
            double result = num / 1e45;
            if (result % 1 == 0)
                return result.ToString("0") + "X5";
            else
                return result.ToString("0.00") + "X5";
        }
    }


    // 1000 üssü hesaplama (tam sayı)
    long Pow1000(int exponent)
    {
        long result = 1;
        for (int i = 0; i < exponent; i++)
            result *= 1000L;
        return result;
    }

    // Pasif gelir yield hesaplaması (önceki hali)
    long CalculateXPYield(int level, long baseXP)
    {
        if (level <= 0) return 0;
        int segments = (level - 1) / 100;
        int remainder = level - segments * 100;
        return baseXP * Pow1000(segments) * remainder;
    }
    double CalculateMoneyYield(int level, double baseMoney)
    {
        if (level <= 0) return 0;
        int segments = (level - 1) / 100;
        int remainder = level - segments * 100;
        return baseMoney * Pow1000(segments) * remainder;
    }

    // Click yield hesaplaması (yeni ölçeklendirme):
    // Her 25 seviyede yield 5 katına çıkacak.
    // seg = floor(level/25), rem = level mod 25
    // yield = baseClick * [1 + (rem/25)*4] * (5^(seg))
    double CalculateClickYieldSimple(int level)
    {
        return System.Math.Pow(1.11, level);
    }


    // Crit yield hesaplaması: lineer artış, maksimum %20.
    float CalculateCritYieldSimple(int level, float baseCrit, float increment)
    {
        float value = baseCrit + increment * level;
        return value > 20f ? 20f : value;
    }

    // ---------------------------
    void Start()
    {
        // Başlangıç seviyelerini sıfırlayın
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

        // Buton tıklama event'leri
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
        // Oyun süresi (timer) güncelleniyor
        totalTimePlayed += Time.deltaTime;

        if (Time.time - lastPassiveIncomeTime >= 1f)
        {
            lastPassiveIncomeTime = Time.time;
            long totalXPGain = 0;

            // Her tier için pasif gelir yield hesaplamaları (önceki hali)
            if (passiveIncomeLevel1 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel1, 100L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel1, 1.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel2 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel2, 1000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel2, 10.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel3 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel3, 10000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel3, 100.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel4 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel4, 100000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel4, 1000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel5 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel5, 1000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel5, 10000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel6 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel6, 10000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel6, 100000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel7 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel7, 100000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel7, 1000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel8 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel8, 1000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel8, 10000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel9 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel9, 10000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel9, 100000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }
            if (passiveIncomeLevel10 > 0)
            {
                long xpGain = CalculateXPYield(passiveIncomeLevel10, 100000000000L);
                double moneyGain = CalculateMoneyYield(passiveIncomeLevel10, 1000000000.0);
                xp += xpGain;
                availableMoney += moneyGain;
                totalXPGain += xpGain;
                totalPassiveIncome += xpGain;
            }

            if (totalXPGain > 0)
                ShowPassiveIncomeEffect(totalXPGain, Color.green);

            UpdateUI();
            UpdateStatsUI();
        }
    }

    // İstatistikleri UI'da gösteren metod
    void UpdateStatsUI()
    {
        string stats = "";
        stats += "Time Played: " + FormatTime(totalTimePlayed) + "\n";
        stats += "Total Clicks: " + totalClicks + "\n";
        stats += "Click Income: " + FormatNumber(totalClickIncome / 100.0) + "\n";
        stats += "Passive Income: " + FormatNumber(totalPassiveIncome / 100.0);
        statsText.text = stats;
    }

    // Süreyi HH:MM:SS formatında gösteren yardımcı fonksiyon
    string FormatTime(float timeInSeconds)
    {
        int hours = (int)(timeInSeconds / 3600);
        int minutes = (int)((timeInSeconds % 3600) / 60);
        int seconds = (int)(timeInSeconds % 60);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }

    void UpdateUI()
    {
        xpText.text = "XP: " + FormatNumber(xp);
        moneyText.text = "Money: " + FormatNumber(availableMoney);

        // Crit upgrade UI: mevcut --> sonraki yield; eğer %20'ye ulaştıysa "MAX"
        float currentCrit = CalculateCritYieldSimple(criticalUpgradeLevel, criticalBase, 0.5f);
        string critText;
        if (currentCrit >= 20f)
        {
            critText = $"Crit Rate Upgrade (Lvl {criticalUpgradeLevel})\nYield: {FormatNumber(20f)}% (MAX)";
            critUpgradeButtonText.text = critText + $"\nCost: {FormatNumber(critUpgradeCost)}";
            critUpgradeButton.interactable = false;
        }
        else
        {
            float nextCrit = CalculateCritYieldSimple(criticalUpgradeLevel + 1, criticalBase, 0.5f);
            critText = $"Crit Rate Upgrade (Lvl {criticalUpgradeLevel})\nYield: {FormatNumber(currentCrit)}% --> {FormatNumber(nextCrit)}%";
            critUpgradeButtonText.text = critText + $"\nCost: {FormatNumber(critUpgradeCost)}";
            critUpgradeButton.interactable = true;
        }

        // Click upgrade UI: mevcut --> sonraki yield ve maliyet %11.68 artışla.
        double currentClickYield = CalculateClickYieldSimple(normalHitUpgradeLevel);
        double nextClickYield = CalculateClickYieldSimple(normalHitUpgradeLevel + 1);
        clickUpgradeButtonText.text = $"Click Upgrade (Lvl {normalHitUpgradeLevel})\nYield: {FormatNumber(currentClickYield)} --> {FormatNumber(nextClickYield)}\nCost: {FormatNumber(Mathf.Pow(1.138f, normalHitUpgradeLevel))}";

        // Passive income butonları
        long currentYield1 = passiveIncomeLevel1 > 0 ? CalculateXPYield(passiveIncomeLevel1, 100L) : 0;
        long nextYield1 = CalculateXPYield(passiveIncomeLevel1 + 1, 100L);
        passiveIncomeUpgradeButtonText1.text = $"Income (T1) (Lvl {passiveIncomeLevel1})\nYield: {FormatNumber(currentYield1)} --> {FormatNumber(nextYield1)}\nCost: {FormatNumber(passiveIncomeUpgradeCost1)}";

        long currentYield2 = passiveIncomeLevel2 > 0 ? CalculateXPYield(passiveIncomeLevel2, 1000L) : 0;
        long nextYield2 = CalculateXPYield(passiveIncomeLevel2 + 1, 1000L);
        passiveIncomeUpgradeButtonText2.text = $"Income (T2) (Lvl {passiveIncomeLevel2})\nYield: {FormatNumber(currentYield2)} --> {FormatNumber(nextYield2)}\nCost: {FormatNumber(passiveIncomeUpgradeCost2)}";

        long currentYield3 = passiveIncomeLevel3 > 0 ? CalculateXPYield(passiveIncomeLevel3, 10000L) : 0;
        long nextYield3 = CalculateXPYield(passiveIncomeLevel3 + 1, 10000L);
        passiveIncomeUpgradeButtonText3.text = $"Income (T3) (Lvl {passiveIncomeLevel3})\nYield: {FormatNumber(currentYield3)} --> {FormatNumber(nextYield3)}\nCost: {FormatNumber(passiveIncomeUpgradeCost3)}";

        long currentYield4 = passiveIncomeLevel4 > 0 ? CalculateXPYield(passiveIncomeLevel4, 100000L) : 0;
        long nextYield4 = CalculateXPYield(passiveIncomeLevel4 + 1, 100000L);
        passiveIncomeUpgradeButtonText4.text = $"Income (T4) (Lvl {passiveIncomeLevel4})\nYield: {FormatNumber(currentYield4)} --> {FormatNumber(nextYield4)}\nCost: {FormatNumber(passiveIncomeUpgradeCost4)}";

        long currentYield5 = passiveIncomeLevel5 > 0 ? CalculateXPYield(passiveIncomeLevel5, 1000000L) : 0;
        long nextYield5 = CalculateXPYield(passiveIncomeLevel5 + 1, 1000000L);
        passiveIncomeUpgradeButtonText5.text = $"Income (T5) (Lvl {passiveIncomeLevel5})\nYield: {FormatNumber(currentYield5)} --> {FormatNumber(nextYield5)}\nCost: {FormatNumber(passiveIncomeUpgradeCost5)}";

        long currentYield6 = passiveIncomeLevel6 > 0 ? CalculateXPYield(passiveIncomeLevel6, 10000000L) : 0;
        long nextYield6 = CalculateXPYield(passiveIncomeLevel6 + 1, 10000000L);
        passiveIncomeUpgradeButtonText6.text = $"Income (T6) (Lvl {passiveIncomeLevel6})\nYield: {FormatNumber(currentYield6)} --> {FormatNumber(nextYield6)}\nCost: {FormatNumber(passiveIncomeUpgradeCost6)}";

        long currentYield7 = passiveIncomeLevel7 > 0 ? CalculateXPYield(passiveIncomeLevel7, 100000000L) : 0;
        long nextYield7 = CalculateXPYield(passiveIncomeLevel7 + 1, 100000000L);
        passiveIncomeUpgradeButtonText7.text = $"Income (T7) (Lvl {passiveIncomeLevel7})\nYield: {FormatNumber(currentYield7)} --> {FormatNumber(nextYield7)}\nCost: {FormatNumber(passiveIncomeUpgradeCost7)}";

        long currentYield8 = passiveIncomeLevel8 > 0 ? CalculateXPYield(passiveIncomeLevel8, 1000000000L) : 0;
        long nextYield8 = CalculateXPYield(passiveIncomeLevel8 + 1, 1000000000L);
        passiveIncomeUpgradeButtonText8.text = $"Income (T8) (Lvl {passiveIncomeLevel8})\nYield: {FormatNumber(currentYield8)} --> {FormatNumber(nextYield8)}\nCost: {FormatNumber(passiveIncomeUpgradeCost8)}";

        long currentYield9 = passiveIncomeLevel9 > 0 ? CalculateXPYield(passiveIncomeLevel9, 10000000000L) : 0;
        long nextYield9 = CalculateXPYield(passiveIncomeLevel9 + 1, 10000000000L);
        passiveIncomeUpgradeButtonText9.text = $"Income (T9) (Lvl {passiveIncomeLevel9})\nYield: {FormatNumber(currentYield9)} --> {FormatNumber(nextYield9)}\nCost: {FormatNumber(passiveIncomeUpgradeCost9)}";

        long currentYield10 = passiveIncomeLevel10 > 0 ? CalculateXPYield(passiveIncomeLevel10, 100000000000L) : 0;
        long nextYield10 = CalculateXPYield(passiveIncomeLevel10 + 1, 100000000000L);
        passiveIncomeUpgradeButtonText10.text = $"Income (T10) (Lvl {passiveIncomeLevel10})\nYield: {FormatNumber(currentYield10)} --> {FormatNumber(nextYield10)}\nCost: {FormatNumber(passiveIncomeUpgradeCost10)}";
    }

    void TryClick(Vector3 clickPosition)
    {
        if (Time.time - lastClickTime < clickCooldown) return;
        lastClickTime = Time.time;
        ShowClickEffect(clickPosition);
    }

    // Click gelirini hesaplayan metod (yeni formül)
    void ShowClickEffect(Vector3 clickPosition)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, clickPosition, null, out anchoredPos);
        float randomOffsetX = Random.Range(-50f, 50f);
        float randomOffsetY = Random.Range(-50f, 50f);
        anchoredPos += new Vector2(randomOffsetX, randomOffsetY);

        double gainedXP = CalculateClickYieldSimple(normalHitUpgradeLevel);

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

        totalClicks++;
        totalClickIncome += gainedXP;

        UpdateUI();
        UpdateStatsUI();
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
        // Eğer krit oranı zaten %20 veya üzerindeyse upgrade yapılamaz.
        if (criticalChance >= 20f)
            return;

        if (availableMoney >= critUpgradeCost)
        {
            availableMoney -= critUpgradeCost;
            critUpgradeCost *= 5f;
            criticalUpgradeLevel++;
            criticalChance = CalculateCritYieldSimple(criticalUpgradeLevel, criticalBase, 0.5f);
            if (criticalChance > 20f)
                criticalChance = 20f;
            UpdateUI();
        }
    }

    void BuyClickUpgrade()
    {
        float cost = Mathf.Pow(1.138f, normalHitUpgradeLevel);
        if (availableMoney >= cost)
        {
            availableMoney -= cost;
            normalHitUpgradeLevel++;
            UpdateUI();
        }
    }

    // Passive Income Satın Alma Metotları:
    void BuyPassiveIncomeUpgrade1()
    {
        if (availableMoney >= passiveIncomeUpgradeCost1)
        {
            availableMoney -= passiveIncomeUpgradeCost1;
            passiveIncomeUpgradeCost1 *= 1.1f;
            passiveIncomeLevel1++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade2()
    {
        if (availableMoney >= passiveIncomeUpgradeCost2)
        {
            availableMoney -= passiveIncomeUpgradeCost2;
            passiveIncomeUpgradeCost2 *= 1.1f;
            passiveIncomeLevel2++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade3()
    {
        if (availableMoney >= passiveIncomeUpgradeCost3)
        {
            availableMoney -= passiveIncomeUpgradeCost3;
            passiveIncomeUpgradeCost3 *= 1.1f;
            passiveIncomeLevel3++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade4()
    {
        if (availableMoney >= passiveIncomeUpgradeCost4)
        {
            availableMoney -= passiveIncomeUpgradeCost4;
            passiveIncomeUpgradeCost4 *= 1.1f;
            passiveIncomeLevel4++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade5()
    {
        if (availableMoney >= passiveIncomeUpgradeCost5)
        {
            availableMoney -= passiveIncomeUpgradeCost5;
            passiveIncomeUpgradeCost5 *= 1.1f;
            passiveIncomeLevel5++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade6()
    {
        if (availableMoney >= passiveIncomeUpgradeCost6)
        {
            availableMoney -= passiveIncomeUpgradeCost6;
            passiveIncomeUpgradeCost6 *= 1.1f;
            passiveIncomeLevel6++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade7()
    {
        if (availableMoney >= passiveIncomeUpgradeCost7)
        {
            availableMoney -= passiveIncomeUpgradeCost7;
            passiveIncomeUpgradeCost7 *= 1.1f;
            passiveIncomeLevel7++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade8()
    {
        if (availableMoney >= passiveIncomeUpgradeCost8)
        {
            availableMoney -= passiveIncomeUpgradeCost8;
            passiveIncomeUpgradeCost8 *= 1.1f;
            passiveIncomeLevel8++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade9()
    {
        if (availableMoney >= passiveIncomeUpgradeCost9)
        {
            availableMoney -= passiveIncomeUpgradeCost9;
            passiveIncomeUpgradeCost9 *= 1.1f;
            passiveIncomeLevel9++;
            UpdateUI();
        }
    }
    void BuyPassiveIncomeUpgrade10()
    {
        if (availableMoney >= passiveIncomeUpgradeCost10)
        {
            availableMoney -= passiveIncomeUpgradeCost10;
            passiveIncomeUpgradeCost10 *= 1.1f;
            passiveIncomeLevel10++;
            UpdateUI();
        }
    }
}
