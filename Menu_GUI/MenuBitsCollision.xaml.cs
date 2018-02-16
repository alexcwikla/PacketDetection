using System;
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
        private TransmissionType newTranssmision;
        public MenuBitsCollision()
        {
            InitializeComponent();
        }

        public void SetResultsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = Results;
        }

        void Stop()
        {
            if (newTranssmision != null)
                newTranssmision.Active = false;
        }
        public void SClose()
        {
            Stop();
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            Stop();
        }

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

        public TransmissionType CreateTransmission(ICollision Collision)
        {
            

            int toInt(string str) // zamiana na Int
            { return Convert.ToInt32(str); }

            ulong numOfT = Convert.ToUInt64(_NumberOfTransmission.Text);
            IControl contType = new ParityBitControl(); // zabezpiecznie przed niezaznaczniem zadnego checkboxu
            if (_CRC.IsChecked == true)
                contType = new CRCControl();
            else if (_CheckSum.IsChecked == true)
                contType = new CheckSumControl();
            else if (_ParityBit.IsChecked == true)
                contType = new ParityBitControl();

            int intLvl = toInt(_InterferenceLVL.Text);
            int sizeOfFra = toInt(_BitsInFrame.Text);
            int numFraInPac = toInt(_FramesInPackage.Text);
            int sizeOfControl = toInt(_BitsControlPart.Text);

            return new TransmissionType(numOfT, contType, Collision, intLvl, sizeOfFra, numFraInPac, sizeOfControl);
        }
            
    

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (newTranssmision == null)
                {
                    BitsCollision BC = CreateBitsCollision(_IsRandom.IsChecked == true, _FirstIndex.Text, _FirstFrame.Text);
                    newTranssmision = CreateTransmission(BC);
                    newTranssmision.Setr(ref Results); //tu moze byc blad
                }
                if (newTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    newTranssmision.Active = true;
                    newTranssmision.UserStop();
                }

            }
            catch (FormatException )
            {
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

        // Prosty sposob na uniemozliwienie zaznaczenia wiecej niz 1 checkbox 
        private void ClickCheckCRC_(object sender, RoutedEventArgs e)
        {
            _CheckSum.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _ParityBit.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }
        private void ClickCheckCheckSum_(object sender, RoutedEventArgs e)
        {
            _ParityBit.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _CRC.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }
        private void ClickCheckParityBit_(object sender, RoutedEventArgs e)
        {
            _CheckSum.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _CRC.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }

        private void IsRandomChecked_(object sender, RoutedEventArgs e)
        {
            _FirstIndex.Text = "";
            _IsRandom.SetCurrentValue(CheckBox.IsCheckedProperty, true);
        }
        #endregion
    }
}
