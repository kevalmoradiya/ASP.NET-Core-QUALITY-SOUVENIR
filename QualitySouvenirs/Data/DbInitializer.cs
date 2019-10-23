using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QualitySouvenirs.Models;
using QualitySouvenirs;

namespace QualitySouvenirs.Data
{
    public class DbInitializer
    {
       

        
        public static void Initialize(SouvenirContext context)
        {
            context.Database.EnsureCreated();
            
            if(context.Users.Any())
            {
                return;
            }

            

            var users = new User[]
            {
                new User{UseFName="Keval",UseLName="Moradiya",UsePno="123456789",UseEmail="km@gmail.com",UsePassword="123456",UseStatus="Enabled",UseAddress="Auckland",}
            };

            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
           {
                new Supplier{SupName="Keval",SupEmail="keval123@gmail.com",SupPno="123456789",},
                new Supplier{SupName="Keval1",SupEmail="keval1234@gmail.com",SupPno="123456789",},
                new Supplier{SupName="Keval2",SupEmail="keval1235@gmail.com",SupPno="123456789",},
                new Supplier{SupName="Keval3",SupEmail="keval1236@gmail.com",SupPno="123456789",}

           };

            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var categories = new Category[]
           {
               
                new Category{CatName="Maori Gift"},
                 new Category{CatName="Tshirt"},
                new Category{CatName="Mug"}
           };

            foreach (Category s in categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();

           
        

        }
    }
}
