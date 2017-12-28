using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public interface IControl
    {
        List<byte> CalculateControlPart(Frame nFrame);
        List<byte> CalculateControlPart(Package nPakiet);
        //byte CollisionDetection(Frame nFrame); 
        // TODO: zdefiniowac tą funkcje w kazdym typie kontroli
        // Przydałoby się zrobić tak żeby:
        // zwracała ta funkcja 
        // 0 - w przypadku gdy nie ma błędu i dany system sprawdzający też stwierdził ze nie ma błędu
        // 1 - gdy wykryto błąd i ten błąd rzeczywiscie występował
        // 2 - gdy nie wykryto błędu, a dany bład występował
        // 3 - gdy wykryto błąd, a błąd nie występował ( chyba może być taka sytuacja, ale bardziej chodzi o sprawdzenie
        //                                              działania funkcji generujacych kontrole )
        /*
         * Mogą Ci się przydać takie funkcje jak:
         * np dla ramki o nazwie fra;
         * fra.GetInformationPart() - zwraca listę częsci informacyjnej ( bez części kontroli )
         * fra.GetControlPart() - zwraca obiekt typu ControlElements
         * fra.GetControlPart().GetList() - zwraca listę z elementami częsci kontrolnej
         * fra.GetControlPart().GetCRCDivider() - zwraca dzielnik dla CRC ( DZIALA TYLKO DLA TYPU KONTORLI CRC )
         * mozesz tez odowoływać się za pomoca []. Opis znajduje sie w klasie IFrame i IPackage
         * 
         */
    }
}
