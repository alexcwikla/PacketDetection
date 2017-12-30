using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class TransmissionType
    {
        ulong _number_of_transsmision;
        IControl control_type;
        ICollision collision_type;
        int interference_level = 1000;
        int size_of_frame = 10;
        int numbers_of_frame_in_package = 10;
        // 0 - noError
        // 1 - Detected
        // 2 - unDetected
        // 3 - detectedNoError
        // 4 - number_of_transmission

        ulong[] frame_results = new ulong[5] { 0, 0, 0, 0, 0 };
        ulong[] package_results = new ulong[5] { 0, 0, 0, 0, 0 };

        public TransmissionType(ulong _number_of_transsmision, IControl control_type,
            ICollision collision_type, int interference_level = 1000,
            int size_of_frame = 10, int numbers_of_frame_in_package = 10)
        {
            this._number_of_transsmision = _number_of_transsmision;
            this.control_type = control_type;
            this.collision_type = collision_type;
            this.interference_level = interference_level;
            this.size_of_frame = size_of_frame;
            this.numbers_of_frame_in_package = numbers_of_frame_in_package;
        }

        public void Normal()
        {
            for (ulong i = 0; i < _number_of_transsmision; i++)
            {
                Package pak = new Package();
                pak.GenerateFrameList(numbers_of_frame_in_package, size_of_frame, control_type);
                collision_type.DoCollision(pak, interference_level);
                ResultsSelectorPackage(pak.CheckPackage());
                foreach (var item in pak.GetFrames())
                {
                    ResultsSelectorFrame(item.CheckFrame());
                }
                pak.DeleteFrames();
                pak = null;
                //GC.Collect();
            }
            this.package_results[4] = _number_of_transsmision;
            this.frame_results[4] = _number_of_transsmision * (ulong)numbers_of_frame_in_package;
            ShowResults();
            //Console.ReadKey();

        }
        private void ResultsSelectorPackage(byte result)
        {
            ResultsSelector(result, package_results);
        }
        private void ResultsSelectorFrame(byte result)
        {
            ResultsSelector(result, frame_results);
        }
        private void ResultsSelector(byte result, ulong[] results)
        {
            switch (result)
            {
                case 0:
                    results[0]++;
                    break;
                case 1:
                    results[1]++;
                    break;
                case 2:
                    results[2]++;
                    break;
                case 3:
                    results[3]++;
                    break;
                default:
                    Console.WriteLine("PODANO ZLY WYNIK  ");
                    break;
            }
        }
        private void ShowResults()
        {
            Console.WriteLine("------------------------------WYNIKI-------------------------------------");
            Console.WriteLine("------------------------------Pakiet-------------------------------------");
            Console.WriteLine("|Liczba pakietow bez bledu :                        " + package_results[0]);
            Console.WriteLine("|Liczba wykrytych blednych pakietow :               " + package_results[1]);
            Console.WriteLine("|Liczba niewykrytych blednych pakietow :            " + package_results[2]);
            Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + package_results[3]);
            Console.WriteLine("|Liczba transmisji -                                " + package_results[4]);
            Console.WriteLine("------------------------------Ramki--------------------------------------");
            Console.WriteLine("|Liczba ramek bez bledu :                           " + frame_results[0]);
            Console.WriteLine("|Liczba wykrytych blednych ramek :                  " + frame_results[1]);
            Console.WriteLine("|Liczba niewykrytych blednych ramek :               " + frame_results[2]);
            Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + frame_results[3]);
            Console.WriteLine("|Liczba transmisji -                                " + frame_results[4]);
            Console.WriteLine("-------------------------------------------------------------------------");
        }



    }

}
