using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //generic constraint: Generic kısıt
    //Where T : class diyerek T'nin class yani referans tip olabileceğini belirttik. İnt vs vermemek için. 
    //IEntity diyerek herhangi bir class kullanmasın yani sadece IEntity olanlar kullanabilir yada IEntityden implemente olan classlar kullanabilir dedik.
    //Ama ben IEntity'i de kullanmak istemiyorum sadece implemente ettiği şeyleri kullanmak istiyorum.Bu yüzden sonuna new() yazdık yani newlenebilir olsun dedik.
    //Interfaceler newlenemediği için new yazmış olduk.
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter = null); //Bunun sayesinde categorye göre getir idye göre getir diye tek tek metod yazmamızı engelleyecek.
                                                         //ProductManager içinde GetAll kısmında(p=>p.CategoryId==2); gibi filtreler yazabilmemizi sağlıyor.
                                                         //filter=null diyerek filtre vermeyecebileceğimizi de belirtmiş olduk.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
