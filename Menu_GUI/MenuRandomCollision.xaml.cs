using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Menu_GUI
{

    public partial class MenuRandomCollision : Page
    {

        private ResultsWindow Results = new ResultsWindow();
        private TransmissionType newTranssmision;
        public MenuRandomCollision()
        {
            InitializeComponent();
        }

        public void SetResultsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = Results;
        }

        public TransmissionType CreateTransmission(ICollision Collision)
        {


            int toInt(string str) // sprawdza czy kolizja ma bazowac na pakiecie czy ramkach
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

                    RandomCollision RC = new RandomCollision();
                    newTranssmision = CreateTransmission(RC);


                    newTranssmision.Setr(ref Results); //tu moze byc blad
                }
                if (newTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    newTranssmision.Active = true;
                    newTranssmision.UserStop();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadz dane");
            }
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



        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
        }

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


    }
}
