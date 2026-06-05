using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryRushExam.Data;
using Unity.Services.CloudSave;
using UnityEngine;

namespace DeliveryRushExam.Save
{
    public class UgsCloudSaveService : ISaveService
    {
        private const string KEY_BEST_SCORE = "bestScore";
        private const string KEY_TOTAL_COINS = "totalCoins";
        private const string KEY_COMPLETED_ORDERS = "completedOrders";
        private const string KEY_UNLOCKED_LEVEL = "unlockedLevel";
        private const string KEY_LAST_SAVE_DATE = "lastSaveDate";

        public async Task SaveAsync(PlayerProgressData progressData)
        {
            progressData.TouchSaveDate();
            
            var dataToSave = new Dictionary<string, object>
            {
                { KEY_BEST_SCORE, progressData.bestScore },
                { KEY_TOTAL_COINS, progressData.totalCoins },
                { KEY_COMPLETED_ORDERS, progressData.completedOrders },
                { KEY_UNLOCKED_LEVEL, progressData.unlockedLevel },
                { KEY_LAST_SAVE_DATE, progressData.lastSaveDate }
            };
            
            await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);
            Debug.Log("[CloudSave] Datos guardados");
        }

        public async Task<PlayerProgressData> LoadAsync()
        {
            var keysToLoad = new HashSet<string>
            {
                KEY_BEST_SCORE,
                KEY_TOTAL_COINS,
                KEY_COMPLETED_ORDERS,
                KEY_UNLOCKED_LEVEL,
                KEY_LAST_SAVE_DATE
            };
            
            var loadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(keysToLoad);
            
            var progress = new PlayerProgressData();
            
            if (loadedData.TryGetValue(KEY_BEST_SCORE, out var bestScore))
                progress.bestScore = bestScore.Value.GetAs<int>();
                
            if (loadedData.TryGetValue(KEY_TOTAL_COINS, out var totalCoins))
                progress.totalCoins = totalCoins.Value.GetAs<int>();
                
            if (loadedData.TryGetValue(KEY_COMPLETED_ORDERS, out var completedOrders))
                progress.completedOrders = completedOrders.Value.GetAs<int>();
                
            if (loadedData.TryGetValue(KEY_UNLOCKED_LEVEL, out var unlockedLevel))
                progress.unlockedLevel = unlockedLevel.Value.GetAs<int>();
                
            if (loadedData.TryGetValue(KEY_LAST_SAVE_DATE, out var lastSaveDate))
                progress.lastSaveDate = lastSaveDate.Value.GetAs<string>();
            
            Debug.Log("[CloudSave] Datos cargados");
            return progress;
        }
    }
}