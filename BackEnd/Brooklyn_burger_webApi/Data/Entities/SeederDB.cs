using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Constants;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Internet_Market_WebApi.Data.Entities
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app) 
        {
            using (var scope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<ISmtpEmailService>();
                context.Database.Migrate();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                if (!context.Roles.Any())
                {
                    RoleEntity admin = new RoleEntity
                    {
                        Name = Roles.Admin,
                    };
                    RoleEntity user = new RoleEntity
                    {
                        Name = Roles.User,
                    };
                    var result = roleManager.CreateAsync(admin).Result;
                    result = roleManager.CreateAsync(user).Result;
                }
                if (!context.Categories.Any())
                {
                    CategoryEntity burgers = new CategoryEntity()
                    {
                        Name = "Burgers",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(burgers);
                    CategoryEntity drinks = new CategoryEntity()
                    {
                        Name = "Drinks",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(drinks);

                    CategoryEntity deserts = new CategoryEntity()
                    {
                        Name = "Deserts",
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                    };
                    context.Categories.Add(deserts);

                    context.SaveChanges();
                }
                if (!context.Subcategories.Any())
                {
                    SubcategoryEntity classic = new SubcategoryEntity()
                    {
                        Name = "Classic Burgers",
                        Category = context.Categories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(classic);
                    SubcategoryEntity black = new SubcategoryEntity()
                    {
                        Name = "Black Burgers",
                        Category = context.Categories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(black);

                    SubcategoryEntity alchohol = new SubcategoryEntity()
                    {
                        Name = "Alchohol",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(alchohol);

                    SubcategoryEntity soda = new SubcategoryEntity()
                    {
                        Name = "Soda",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(soda);

                    SubcategoryEntity water = new SubcategoryEntity()
                    {
                        Name = "Water",
                        Category = context.Categories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(water);

                    SubcategoryEntity cakes = new SubcategoryEntity()
                    {
                        Name = "Cakes",
                        Category = context.Categories.ToList()[2],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(cakes);

                    SubcategoryEntity iceCream = new SubcategoryEntity()
                    {
                        Name = "Ice Cream",
                        Category = context.Categories.ToList()[2],
                        DateCreated = DateTime.Now,
                        IsDelete = false
                    };
                    context.Subcategories.Add(iceCream);
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    //Pizza on red sauce
                    ProductEntity colorado = new ProductEntity()
                    {
                        Name = "Colorado Burger",
                        Description = "Certified Angus Beef, pepper jack cheese, melted cheddar cheese, grilled Anaheim chiles, lettuce, tomatoes, mayo, toasted spicy chipotle bun.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 10.99,
                        SalePrice = 5.99,
                        Image = "https://assets.simpleviewinc.com/simpleview/image/fetch/c_limit,h_1200,q_75,w_1200/https://assets.simpleviewinc.com/simpleview/image/upload/crm/fresnoca/colorado_grill_174867481_448612982893983_6002138391645406038_n_E6C6A50B-5056-A36A-089B622EECA281D0-e6c6a2435056a36_e6c6b063-5056-a36a-08fafa0dbc4f2468.jpg"
                    };
                    context.Products.Add(colorado);
                    ProductEntity smoked = new ProductEntity()
                    {
                        Name = "Smoked Bacon Brisket Burger",
                        Description = "Certified Angus Beef, aged cheddar cheese, brisket, applewood smoked bacon, pickles, bbq sauce, toasted brioche bun.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 14.99,
                        SalePrice = 0,
                        Image = "https://img.cdn4dd.com/p/fit=cover,width=1200,height=1200,format=auto,quality=50/media/photosV2/84646979-b374-451e-b0a3-6453c38905ad-retina-large.jpg"
                    };
                    context.Products.Add(smoked);

                    ProductEntity smash = new ProductEntity()
                    {
                        Name = "Classic Smash® Burger",
                        Description = "Certified Angus Beef, American cheese, lettuce, tomatoes, red onions, pickles, Smash Sauce®, ketchup, toasted bun.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 18.99,
                        SalePrice = 15.99,
                        Image = "https://www.citypng.com/public/uploads/small/11665837933adn5gvyxayk0vigrgoys7phmcq8dqin6uyastrmu8s4lzeyo7ucu3kjpm8bfcax0vuzyupw6utvl4p3appmh3wqjyomaipxei3ds.png"
                    };
                    context.Products.Add(smash);

                    ProductEntity avocado = new ProductEntity()
                    {
                        Name = "Avocado Bacon Club Burger",
                        Description = "Certified Angus Beef, freshly smashed avocado, applewood smoked bacon, lettuce, tomatoes, ranch, mayo, toasted multi-grain bun.",
                        Subcategory = context.Subcategories.ToList()[0],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 11.99,
                        SalePrice = 7.99,
                        Image = "https://assets.epicurious.com/photos/560d7999f9a841923089d998/1:1/w_2595,h_2595,c_limit/350990_hires.jpg"
                    };
                    context.Products.Add(avocado);

                    //Black Burgers
                    ProductEntity kuro = new ProductEntity()
                    {
                        Name = "Kuro Burgers Classic®",
                        Description = "The bread is colored with bamboo charcoal and the ketchup – with onion and garlic in a soy sauce base - has squid ink added to it. Not to be outdone, the cheese slices also have bamboo charcoal and naturally the 115 gram beef patties are made with black pepper.",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 11.99,
                        SalePrice = 6.99,
                        Image = "https://d1ralsognjng37.cloudfront.net/17cc0936-89b4-405c-9a42-983860ba30c5.jpeg"
                    };
                    context.Products.Add(kuro);

                    ProductEntity kuroBacon = new ProductEntity()
                    {
                        Name = "Kuro Burgers Bacon®",
                        Description = "Added solid portion of crispy bacon. The bread is colored with bamboo charcoal and the ketchup – with onion and garlic in a soy sauce base - has squid ink added to it. Not to be outdone, the cheese slices also have bamboo charcoal and naturally the 115 gram beef patties are made with black pepper.",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 16.99,
                        SalePrice = 0,
                        Image = "https://assets.tmecosys.cn/image/upload/t_web767x639/img/recipe/ras/Assets/5aedf3a8bb563a658c05969b3a54c52e/Derivates/696fb022bb1c97ca136ae58066377a4a97c6a893.jpg"
                    };
                    context.Products.Add(kuroBacon);

                    ProductEntity kuroChicken = new ProductEntity()
                    {
                        Name = "Kuro Burgers Chicken®",
                        Description = "With crispy chicken instead of classic beaf. The bread is colored with bamboo charcoal and the ketchup – with onion and garlic in a soy sauce base - has squid ink added to it. Not to be outdone, the cheese slices also have bamboo charcoal and naturally the 115 gram beef patties are made with black pepper.",
                        Subcategory = context.Subcategories.ToList()[1],
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 9.99,
                        SalePrice = 0,
                        Image = "https://burgerlad.com/wp-content/uploads/2016/03/image1-6.jpg"
                    };
                    context.Products.Add(kuroChicken);
                    //Alchohol
                    ProductEntity aperol = new ProductEntity()
                    {
                        Name = "Aperol Spritz",
                        Description = "Aperol, an orange-red liquor invented by the Barbieri brothers in Padova in 1919, is a go-to Spritz option. Low in alcohol, pleasantly citrusy and slightly bitter, it is a light and fresh aperitif that owes its flavors and aromas to sweet and bitter oranges, rhubarb, and gentian root.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 7.99,
                        Image = "https://img.freepik.com/premium-photo/glass-aperol-spritz-cocktail-isolated-white-background_802770-19.jpg?w=2000"
                    };
                    context.Products.Add(aperol);

                    ProductEntity redWine = new ProductEntity()
                    {
                        Name = "Red Wine",
                        Description = "Indulge in the robust flavors of our red wine, with notes of dark fruit and a smooth finish. At 12% alcohol, this wine is the perfect complement to any meal or occasion.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 0,
                        Image = "https://media1.popsugar-assets.com/files/thumbor/XFdgEGjXrPMUmR9nABQUpm0_bvc/fit-in/2048xorig/filters:format_auto-!!-:strip_icc-!!-/2017/11/30/762/n/24155406/tmp_KvXKVY_bf971dc54d346a9c_wine.jpg"
                    };
                    context.Products.Add(redWine);

                    ProductEntity whiteWine = new ProductEntity()
                    {
                        Name = "White Wine",
                        Description = "Savor the crisp and refreshing taste of our white wine, with hints of citrus and a subtle sweetness. At 12% alcohol, this wine pairs perfectly with seafood, light pasta dishes, or as a delightful aperitif.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Alchohol"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 13.99,
                        SalePrice = 0,
                        Image = "https://annapolis-sons-and-daughters-of-italy-in-america-lodge-2225.weebly.com/uploads/1/3/1/5/131550486/s234497489680235550_p188_i1_w615.jpeg"
                    };
                    context.Products.Add(whiteWine);

                    //Soda
                    ProductEntity cola = new ProductEntity()
                    {
                        Name = "Coca Cola",
                        Description = "0.5 of classic Coca Cola soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://target.scene7.com/is/image/Target/GUEST_7819ee30-1f78-46ee-a21c-d2096f99ba42?wid=488&hei=488&fmt=pjpeg"
                    };
                    context.Products.Add(cola);

                    ProductEntity fanta = new ProductEntity()
                    {
                        Name = "Fanta",
                        Description = "0.5 of classic Fanta soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://target.scene7.com/is/image/Target/GUEST_65db2c37-f185-4eb3-9013-c96bd3c96b3b?wid=488&hei=488&fmt=pjpeg"
                    };
                    context.Products.Add(fanta);

                    ProductEntity sprite = new ProductEntity()
                    {
                        Name = "Sprite",
                        Description = "0.5 of classic Sprite soda",
                        Subcategory = context.Subcategories.First(x => x.Name == "Soda"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 3.99,
                        SalePrice = 0,
                        Image = "https://ipcdn.freshop.com/resize?url=https://images.freshop.com/00049000052718/51229873a0c2d7afded6feb20e64fd2c_large.png&width=512&type=webp&quality=90"
                    };
                    context.Products.Add(sprite);

                    //Water
                    ProductEntity stillWater = new ProductEntity()
                    {
                        Name = "Still Water",
                        Description = "0.5 glass of still water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://hips.hearstapps.com/goodhousekeeping-uk/main/embedded/25836/glass_water.jpg"
                    };
                    context.Products.Add(stillWater);

                    ProductEntity sparklingL = new ProductEntity()
                    {
                        Name = "Lightly Sparkling Water",
                        Description = "0.5 glass of lightly sparkling water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://hips.hearstapps.com/goodhousekeeping-uk/main/embedded/25836/glass_water.jpg"
                    };
                    context.Products.Add(sparklingL);

                    ProductEntity sparkling = new ProductEntity()
                    {
                        Name = "Sparkling Water",
                        Description = "0.5 glass of sparkling water",
                        Subcategory = context.Subcategories.First(x => x.Name == "Water"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 1.00,
                        SalePrice = 0,
                        Image = "https://www.freepnglogos.com/uploads/water-glass-png/water-glass-biolution-2.png"
                    };
                    context.Products.Add(sparkling);

                    //Cakes
                    ProductEntity chocolateCake = new ProductEntity()
                    {
                        Name = "Chocolate Cake",
                        Description = "Peace of delcious soft cake flavored with melted chocolate, cocoa powder, or both.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 10.99,
                        SalePrice = 4.99,
                        Image = "https://www.hersheyland.com/content/dam/hersheyland/en-us/recipes/recipe-images/2_Hersheys_Perfectly_Chocolate_Cake_11-18.jpeg"
                    };
                    context.Products.Add(chocolateCake);

                    ProductEntity napoleon = new ProductEntity()
                    {
                        Name = "Napoleon",
                        Description = "Napoleon is composed of many layers of puff pastry with a whipped pastry cream filling and encrusted with more pastry crumbs",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 8.99,
                        SalePrice = 0,
                        Image = "https://letthebakingbegin.com/wp-content/uploads/2013/07/The-Best-Napoleon-Cake-is-made-with-thin-puff-pastry-layers-then-sandwiched-with-rich-and-buttery-custard.-This-Napoleon-dessert-is-one-of-my-familys-favorite-2.jpg"
                    };
                    context.Products.Add(napoleon);

                    ProductEntity strudel = new ProductEntity()
                    {
                        Name = "Apple Strudel",
                        Description = "Perfect classic Austrian strudel, made with fresh apples, baked with love.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Cakes"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://houseofnasheats.com/wp-content/uploads/2021/10/Apple-Strudel-Apfelstrudel-Square-1.jpg"
                    };
                    context.Products.Add(strudel);

                    //Ice Cream
                    ProductEntity chocolateIceCream = new ProductEntity()
                    {
                        Name = "Chocolate Ice Cream",
                        Description = "The Best Chocolate Ice Cream is a bold name, but it is equally matched by the bold chocolate flavor of this custard-based ice cream.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://joyfoodsunshine.com/wp-content/uploads/2020/06/homemade-chocolate-ice-cream-recipe-7.jpg"
                    };
                    context.Products.Add(chocolateIceCream);

                    ProductEntity vanilla = new ProductEntity()
                    {
                        Name = "Vanilla Ice Cream",
                        Description = "Super-smooth and creamy. Spot-on vanilla flavor. Oh-so cold and refreshing. Basically EVERYTHING a classic vanilla ice cream should be.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://joyfoodsunshine.com/wp-content/uploads/2020/07/homemade-vanilla-ice-cream-recipe-6.jpg"
                    };
                    context.Products.Add(vanilla);

                    ProductEntity strawberry = new ProductEntity()
                    {
                        Name = "Strawberry Ice Cream",
                        Description = "Our strawberry ice cream is so rich and creamy, with an amazing fresh strawberry taste.",
                        Subcategory = context.Subcategories.First(x => x.Name == "Ice Cream"),
                        DateCreated = DateTime.Now,
                        IsDelete = false,
                        Price = 7.99,
                        SalePrice = 0,
                        Image = "https://ohsweetbasil.com/wp-content/uploads/creamy-homemade-strawberry-ice-cream-recipe-6-scaled.jpg"
                    };
                    context.Products.Add(strawberry);

                    context.SaveChanges();

                }
            }
        }
    }
}
