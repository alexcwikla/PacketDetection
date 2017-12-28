using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    /* Realizacja Klasy Frame
     * 
     * 
     * Przykład:
     * newFrame1
     * Część Informacyjna        : Kontrolna
     * [0|1|1|1|0|1] : [1|0|1|1]
     * 
     * Część kontrolna jest jako osobny objekt i można się do niej odwołać za pomocą funkcji List<byte> GetControlPart()
     * 
     * Do konkretnych elementów ramki można się dostać w następujący sposób:
     * 
     * newFrame1[0] - zwróci pierwszy element listy częsci informacyjnej ( w tym przypadku 0 )
     * newFrame1[5] - zwróci szósty element listy części informacyjnej ( czyli 0 )
     * newFrame1[7] - zwróci błąd, bo część infomacyjna nie jest połączona z częścią kontrolną!!!!!  <------------
     * newFrame1.getControlPart()[0] - zwróci pierwszy element części kontrolnej ramki ( czyli 1 )
     * newFrame1.getControlPart()[1] - zwróci drugi element części kontrolnej ramki ( czyli 0 )
     * 
     * Przeciążony jest operator [] !!
     * 
     * ==============================================================================
     * Przykładowy sposob tworzenia ramki:
     * Frame fra = new Frame.Builder().RandomFrame(30).SetControl(7).Create()
     * 
     * (Opis poszczegolnych metod przy deklaracji)
     * ==============================================================================
     */
    interface IFrame
    {
        /* Zwraca Listę ramki bez części kontroli ( część informacyjna )
         */
        List<byte> GetInformationPart();
        /* Zwraca obiekt częsci kontrolnej
         */
        ControlElements GetControlPart();

        /* Zwraca część Informacyjna w postaci dziesiętnej - dla bardzo duzych ramek moze nie zadziałać!! 64bity
         */
        ulong GetInformationPartInDec();

        /* Wyświetla Część kontrolną
         * 
         */
        void ShowControlPart();
        /* Wyświetla część informacyjna
         */
        void ShowInformationPart();

        /* Ustawia część informacyjna zamieniając podany argument(liczbę) funkcji
         * z systemu dziesiętnego na binarny
         */
        void SetInformationPart(int number);

        /* Ustawia część kontrolną zamieniając podany argument(liczbę) funkcji
         * z systemu dziesiętnego na binarny
         */
        //void SetControlPart(int number);
        void SetControlPartByType();
        //void SetControlPart(List<byte> nList);

        /* Generuje losowe bity dla części informacyjnej
         */
        void CreateRandomInformationPart(int size); 

    }
}
