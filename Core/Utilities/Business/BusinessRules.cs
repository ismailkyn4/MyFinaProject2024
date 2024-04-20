using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //kullanacağımız yerde params ile IResult türünde virgül ile ayırarak istediğimiz kadar parametre ekleyebiliriz. 
        //parametre ile gönderdiğimiz iş kurallarından başarısız olanı businessa gönderiyoruz.
        //Yani şu logic hatalı diye dönüyoruz. Yani kuralın kendisini döndürüyoruz.
        //List<IResult> da dönebiliriz.
        public static IResult Run(params IResult[] logics)  
        {
            // List<IResult> errorResults = new List<IResult>(); List<IResult> dönmek istersek böyle kullanırız.
            //if içinde errorResult.Add(logic) ekleriz ve foreach dışında return errorResult yaparız.Bu şekilde hepsini döndürür.

            foreach (var logic in logics)
            {
                if(!logic.Success)
                {
                    return logic; //kurala uymayanı döndür.
                }
            }
            return null;
        }
    }
}
