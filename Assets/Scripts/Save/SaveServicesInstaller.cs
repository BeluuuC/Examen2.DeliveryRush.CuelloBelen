using UnityEngine;

namespace DeliveryRushExam.Save
{
    public class SaveServicesInstaller : MonoBehaviour
    {
        public enum SaveMode
        {
            Local,
            Cloud
        }

        [SerializeField] private SaveMode saveMode = SaveMode.Local;

        private void Awake()
        {
            ISaveService saveService;

            if (saveMode == SaveMode.Local)
            {
                saveService = new LocalSaveService();
            }
            else
            {
                saveService = new UgsCloudSaveService();
            }

            ServiceLocator.Register<ISaveService>(saveService);

            Debug.Log($"SaveServicesInstaller: Registrado {saveService.GetType().Name}");
        }
    }
}
