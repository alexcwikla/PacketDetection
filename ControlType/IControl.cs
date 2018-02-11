using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
   
    public interface IControl
    {
        

        List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Functions.FLEXIBLE);
        List<byte> CalculateControlPart(Package nPakiet, int sizeOfControlPart = Functions.FLEXIBLE);
        byte CollisionDetection(Frame nFrame);
        byte CollisionDetection(Package nPackage);
        
        // Funkcja wykrywania bledu zwraca (Collision Detection): 
        // 0 - w przypadku gdy nie ma błędu i dany system sprawdzający też stwierdził ze nie ma błędu
        // 1 - gdy wykryto błąd i ten błąd rzeczywiscie występował
        // 2 - gdy nie wykryto błędu, a dany bład występował
        // 3 - gdy wykryto błąd, a błąd nie występował ( przy ograniczeniach dlugosci części kontrolnej i 
        //                                              do testowania poprawnosci dzialania algorytmu kontroli )
        /*
         * 
         * przydatne funkcje:
         * fra.GetInformationPart() - zwraca listę częsci informacyjnej ( bez części kontroli )
         * fra.GetControlPart() - zwraca obiekt typu ControlElements
         * fra.GetControlPart().GetList() - zwraca listę z elementami częsci kontrolnej
         * fra.GetControlPart().GetCRCDivider() - zwraca dzielnik dla CRC ( DZIALA TYLKO DLA TYPU KONTORLI CRC )
         * mozesz tez odowoływać się za pomoca []. Opis znajduje sie w klasie IFrame i IPackage
         * 
         */
    }
}
