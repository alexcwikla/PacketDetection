using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Pakiet jako przechowywacz listy Ramek
 * 
 * Przyklad ( losowe generowanie Ramek za pomoca GenerateFrameList() )
 * R - ramka
 * 
 * [ Pakiet1 ]
 * Lista ramek             : czesc kontrolna
 * [R0][R1][R2][R3][R4]... : [1][0][1]
 * 
 * Pakiet1[0] - pierwsza ramka 
 * Pakiet1[2] - trzecia ramka
 * 
 * Czesc kontrolna nie jest polaczona z Lista ramek.
 * Do czesci kontroli mozna sie dostac za pomoca :
 * pak.GetControlPart()[1] - zwroci 0 w tym przypadku
 * pak.GetControlPart()[0] - zwroci 1 w tym przypadku
 */

namespace Projekt_Kolko
{
    interface IPackage
    {
        // Opis poszczegolnych metod przy wywolywaniu!!
        void ShowControlPart();
        void ShowFrames();
        List<Frame> GetFrames();
        ControlElements GetControlPart();
        //void SetControlPart(int number);
        void AddFrame(Frame item);
        void GenerateFrameList(int numberOfFrames, int size, IControl controlType = null, bool random = true);
        void DeleteFrames();






    }
}
