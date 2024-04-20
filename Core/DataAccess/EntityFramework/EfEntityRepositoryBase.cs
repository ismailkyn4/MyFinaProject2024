using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext> :IEntityRepository<TEntity>
        where TEntity : class,IEntity,new()
        where TContext: DbContext, new() //Buraya kafamıza göre her classs'ı yazamaıyoruz. Sadece DbContexten inherit edilenleri yaz.
        //Burayı kullanılacak yerde bize birtane TEntity ve TContext ver dedik. İsimlendirme bize ait.
        //Kullanacağımız yerde de Product veya Customer ve Context türünü belirterek kullanabiliriz.
        //TEntity ve TContext için generic kısıtlamalarımızı yazıyoruz.
        //IEntityRepository içine gittiğimizde bizden bir tane T türünde nesne ister.
        //Bizde burda EfEntityRepasitoryBase için TEntity oluşturduğumuz için TEntity türünde ne verirsek onu kullanacak.TEntity bir isimlendirme şekli.
        //Core yapısını bir kere yazarız ve bütün projelerimizde bunu kullanırız.
    {
        public void Add(TEntity entity)
        {
            //using'in içine yazdığımız nesne için NortwinContextin işi bittiğinde çöp toplayıcıyı bekleme bittiği o dispose (bellekten hızlıca atar) et.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //Entry frameworke özel. Git veri kaynağından benim bu gönderdiğim  Product'a bir taneye nesneye eşleştir.
                addedEntity.State = EntityState.Added; //addedEntity'in durumu eklendi olarak belirle
                context.SaveChanges(); //Değişiklikleri kaydet.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);

                //context.Set<Product>() aslında bizim ürünler tablomuz 
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null) //burada filtre yazacağımız yer lambda 
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() 
                    : context.Set<TEntity>().Where(filter).ToList();
                //Eğer filtre null ise Veri tabanındaki Product tablosunu listeye çevir ve onu bana ver
                //Eğer filtre null değilse yukarıda belirttiğimiz gibi ben sana bir filtre vericem 
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
