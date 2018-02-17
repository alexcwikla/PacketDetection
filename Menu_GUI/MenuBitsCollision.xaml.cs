﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PackageDetection.Results;
using Projekt_Kolko;
//using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuBitsCollision : Page
    {

        private ResultsWindow Results = new ResultsWindow();
        private MenuPackageSettings PSettings = new MenuPackageSettings();
        private BitsCollision BC;


        public MenuBitsCollision()
        {
            InitializeComponent();
        }

        #region SetPages
        public void SetResultsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = Results;
        }

        public void SetPackageSettingsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = PSettings;
        }
        #endregion


        #region Stop/exit
        public void SClose()
        {
            PSettings.Stop();
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            PSettings.Stop();
        }
        #endregion

        protected BitsCollision CreateBitsCollision(bool isRandom, string firstIndex, string firstFrame)
        {
            int firstIndexInt;
            int firstFrameInt;
            bool isBasedPackage;
            if (firstFrame == "") { firstFrameInt = 0; isBasedPackage = false; MessageBox.Show(firstFrame); } // dla pustej
            else { firstFrameInt = Convert.ToInt32(firstFrame); isBasedPackage = true; } // dla zapisanej czesci

            if (firstIndex == "") { firstIndexInt = 0; isRandom = true; }
            else { firstIndexInt = Convert.ToInt32(firstIndex); isRandom = false; }

            if (isRandom == true)
                return new BitsCollision.Builder().SetBasedOnPackage(isBasedPackage, firstFrameInt).SetRandomCollision().Create();
            else
                return new BitsCollision.Builder().SetBasedOnPackage(isBasedPackage, firstFrameInt).ChangeGroupOfBits(firstIndexInt).Create();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BC == null)
                {
                    BC = CreateBitsCollision(_IsRandom.IsChecked == true, _FirstIndex.Text, _FirstFrame.Text);
                }
                PSettings.Start_transsmision(BC, Results);
            }
            catch (FormatException )
            {
                BC = null;
                MessageBox.Show("Wprowadz dane");
            }
        }

        #region DataInBox And Checkbox
        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
        }

        // wybor konkretnej grupy bitow do zamiany | wylaczenie losowego wyboru bitow
        private void DataInBoxGroup_(object sender, TextChangedEventArgs e)
        {
            _FirstIndex.Text = Data_verification.Check(_FirstIndex.Text, 10000, 0, 5);
            _IsRandom.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }


        private void IsRandomChecked_(object sender, RoutedEventArgs e)
        {
            _FirstIndex.Text = "";
            _IsRandom.SetCurrentValue(CheckBox.IsCheckedProperty, true);
        }
        #endregion
    }
}
