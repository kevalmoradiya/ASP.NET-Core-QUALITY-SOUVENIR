# ASP.NET-Core-QUALITY-SOUVENIR
Shopping website developed in ASP.NET Core


# Introduction:
Quality Souvenir is a website for people to shop online T-shirts, Mugs,MaoriGifts. This website can be browsed by both anonymous visitors and registered members, but only registered members can place an order. This application was developed using Visual Studio 2017 with programming languages; ASP.NET Core 2.1.4, C# and Database which created by different classes then automatically add in MySQLServer.
Website URL: 	localhost
Admin User Name / Password: 	admin123@gmail.com/ admin123
Customer User Name / Password:	
Website Folder:	


# BUSINESS SPECIFICATION
1.1	Customer
The website allows both registered and anonymous users to browse the products displayed in the website in below manners. 

1.1.1	View Home page, Product page,and Contact page
Both logged in customers and guests can view these pages. They can navigate to the corresponding category by clicking on the category on home page.
They can view all the Products on product page. Additionally, these Quality Souvenir can be viewed by one category at a time by choosing on of category buttons in the same page. Each page will display up to 9products, the rest will be viewed in “Next” page. 
They can search one souvenir on product page. After entering keywords, they can view the result souvenirs.

1.1.2	Add Item to Shopping Cart
Both guests and logged in customers are able to add any available souvenir to their shopping cart. Adding any product will increase the total products quantity displayed beside the shopping cart icon in the upper right navigation bar. Adding the same souvenir will increase the quantity and total cost of that souvenir in shopping cart.

1.1.3	View Shopping Cart
Both guests and logged in customers can view their shopping cart by clicking the shopping cart icon. Shopping cart view will display the Souvenir ID, Souvenir name, Category, Quantity, price and the total cost (including GST) should be displayed.

1.1.4	Remove Souvenir items from Shopping Cart
Both guests and logged in customers are able to remove souvenir items from their shopping cart by clicking the “-” sign in shopping cart view. After clicking, the quantity will reduce by 1 (original quantity > 1) or the item will get removed from the shopping cart (original quantity = 1).

1.1.5	Clear Shopping Cart
Both guests and logged in customers are able to clear their shopping cart by clicking the Clear cart button in shopping cart view. After clicking, all items in the shopping cart are disappeared.

1.1.6	Check out

When Proceed to Checkout button is clicked, if user is not logged in, the login page will be displayed. If user is not registered yet, they can find register page hyperlink in login page. 

1.1.7	Place an order
Only registered customers can place an order by clicking the “checkout” button on the shopping cart view. After that they will be asked to confirm their order information and then complete the order process. They can view their order details in the end.

1.1.8	Registration
A guest can register to be a member.
A guest must have   a valid first name, a valid last name, a password, a valid email address, a valid mobile number for registration.

1.1.9	Login
To be able to log in, a customer must complete registration first, including email confirmation.
Customer must provide correct email and password to log in.

1.1	Administrator
The administrator has permission to access all the anonymous visitors and registered customers functions. The administrator can do any shopping activities, which can facilitate the testing process.

1.1.1	Log in
With embedded username and password, administrator can log in.

1.1.2	View Information
After logging in, administrator will see all the information for all customers, existing orders, souvenirs, categories and suppliers.

1.1.3	Add Information
After logging in, administrator will be able to add a souvenir item,  category and supplier by providing required and eligible input.

1.1.4	Modify Information
After logging in, administrator will be able to edit all bag information including souvenir  image, profile details of customers and suppliers, and category.

1.1.5	Delete Information
After logging in, administrator will be able to delete a customer, souvenir item, souvenir category, supplier and order with a user interface for each.

1.1.6	Disable Account
After logging in, administrator will be able to disable any customer by clicking Disable button in index page of customer.




