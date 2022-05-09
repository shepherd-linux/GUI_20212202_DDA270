using Cirmi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Logics
{
    public class MenuManagerLogic : IMenuManagerLogic
    {
        MainMenuViewModel mainMenuViewModel;
        SettingsViewModel settingsViewModel;
        StoreViewModel storeViewModel;
        SelectLevelViewModel selectLevelViewModel;
        PauseMenuViewModel pauseMenuViewModel;
        GameOverViewModel gameOverViewModel;
        InventoryViewModel inventoryViewModel;
        IMapManagerLogic mapManagerLogic;

        bool isFromMainMenu;
        int balance;

        public void Setup(MainMenuViewModel mainMenuViewModel, SettingsViewModel settingsViewModel, StoreViewModel storeViewModel,
            SelectLevelViewModel selectLevelViewModel, PauseMenuViewModel pauseMenuViewModel, GameOverViewModel gameOverViewModel,
            InventoryViewModel inventoryViewModel, IMapManagerLogic mapManagerLogic)
        {
            isFromMainMenu = true;
            this.mainMenuViewModel = mainMenuViewModel;
            this.settingsViewModel = settingsViewModel;
            this.storeViewModel = storeViewModel;
            this.selectLevelViewModel = selectLevelViewModel;
            this.pauseMenuViewModel = pauseMenuViewModel;
            this.gameOverViewModel = gameOverViewModel;
            this.inventoryViewModel = inventoryViewModel;
            this.mapManagerLogic = mapManagerLogic;
            this.mainMenuViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) {
                if(e.PropertyName == "SelectedMenu")
                {
                    switch (mainMenuViewModel.SelectedMenu)
                    {
                        case 0:
                            LoadMainMenu();
                            break;
                        case 1:
                            LoadSelectLevel();
                            break;
                        case 2:
                            LoadStore();
                            break;
                        case 3:
                            LoadSettings();
                            break;
                        default:
                            break;
                    }
                }
            });

            this.pauseMenuViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "SelectedMenu")
                {
                    switch (pauseMenuViewModel.SelectedMenu)
                    {
                        case 0:
                            LoadPauseMenu();
                            break;
                        case 1:
                            LoadSettings();
                            break;
                        case 2:
                            LoadMainMenu();
                            break;
                        default:
                            break;
                    }
                }
            });

            this.settingsViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Visibility")
                {
                    if(settingsViewModel.Visibility == Visibility.Collapsed)
                    {
                        if (isFromMainMenu)
                            LoadMainMenu();
                        else LoadPauseMenu();
                    }
                }
            });

            this.storeViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Visibility")
                {
                    if (storeViewModel.Visibility == Visibility.Collapsed)
                    {
                        LoadMainMenu();
                        balance = storeViewModel.Balance;
                    }
                }
            });

            this.selectLevelViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Visibility")
                {
                    if (selectLevelViewModel.Visibility == Visibility.Collapsed)
                    {
                        if (!selectLevelViewModel.StartingGame)
                            LoadMainMenu();
                        else
                        {
                            mapManagerLogic.SelectedLevel = selectLevelViewModel.SelectedLevel;
                            if(storeViewModel.SelectedSkin > 2)
                                mapManagerLogic.SelectedSkin = storeViewModel.SelectedSkin;

                            mapManagerLogic.LoadNextLevel();
                        }
                    }
                }
            });

            this.gameOverViewModel.PropertyChanged += new PropertyChangedEventHandler(delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Visibility")
                {
                    switch (mapManagerLogic.SelectedLevel)
                    {
                        case 1:
                            selectLevelViewModel.Level2Locked = false;
                            break;
                        case 2:
                            selectLevelViewModel.Level3Locked = false;
                            break;
                        case 3:
                            selectLevelViewModel.Level4Locked = false;
                            break;
                        case 4:
                            selectLevelViewModel.Level5Locked = false;
                            break;
                        default:
                            break;
                    }

                    if (gameOverViewModel.Visibility == Visibility.Collapsed)
                    {
                        balance += gameOverViewModel.TimeScore;
                        inventoryViewModel.PlayerScore = 0;
                        inventoryViewModel.CollectedItem = "";

                        if (gameOverViewModel.IsBackToMenuClicked)
                        {
                            inventoryViewModel.Visibility = Visibility.Collapsed;
                            LoadMainMenu();
                        }
                        else
                        {
                            mapManagerLogic.SelectedLevel++;

                            if (mapManagerLogic.SelectedLevel > 5)
                                mapManagerLogic.SelectedLevel = 1;

                            mapManagerLogic.LoadNextLevel();
                        }
                    }
                }
            });
        }

        public void LoadInventory()
        {
            inventoryViewModel.Visibility = Visibility.Visible;
        }

        public void LoadGameOver()
        {
            gameOverViewModel.Visibility = Visibility.Visible;
        }

        public void LoadMainMenu()
        {
            mainMenuViewModel.SelectedMenu = 0;
            mainMenuViewModel.Visibility = Visibility.Visible;
            isFromMainMenu = true;
        }

        public void LoadPauseMenu()
        {
            pauseMenuViewModel.SelectedMenu = 0;
            pauseMenuViewModel.Visibility = Visibility.Visible;
            isFromMainMenu = false;
        }

        public void LoadSelectLevel()
        {
            selectLevelViewModel.Visibility = Visibility.Visible;
        }

        public void LoadSettings()
        {
            settingsViewModel.Visibility = Visibility.Visible;
        }

        public void LoadStore()
        {
            storeViewModel.Visibility = Visibility.Visible;
            storeViewModel.Balance = balance;
        }
    }
}
