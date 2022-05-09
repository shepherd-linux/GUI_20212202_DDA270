using Cirmi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cirmi.Logics
{
    public interface IMenuManagerLogic
    {
        public void Setup(MainMenuViewModel mainMenuViewModel, SettingsViewModel settingsViewModel, StoreViewModel storeViewModel,
            SelectLevelViewModel selectLevelViewModel, PauseMenuViewModel pauseMenuViewModel, GameOverViewModel gameOverViewModel,
            InventoryViewModel inventoryViewModel, IMapManagerLogic mapManagerLogic, IPlayerLogic playerLogic);
        public void LoadMainMenu();
        public void LoadSettings();
        public void LoadStore();
        public void LoadSelectLevel();
        public void LoadPauseMenu();
        public void LoadGameOver();
        public void LoadInventory();
    }
}
