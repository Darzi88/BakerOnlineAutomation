# BakerOnlineAutomation
Contains automated tests for https://bakeronline.be/be-en/demo-shop

Description
Using Selenium, Nlog and ExtentReports I was able to make automated tests for for https://bakeronline.be/be-en/demo-shop, and after every run logs and a .html report will be created with details how the tests performed. 

Functionalities that were automated are:
1. Registering a user.
2. Logging in and loging out of the application.
3. Reseting the password and then logging in using the new password.
4. Browsing products as a visitor.
5. Browsing products as a user.
6. Ordering a product as a user.
7. Ordering two products as a user.

Tests are writen in C#

Technologies used:
Selenium v.4.3.0
Selenium ChromeDriver v.104.0.5112.7900
NLog v.5.0.1
ExtentReports v.4.1.0

Take note that Selenium WebDriver needs to be updated to match version of your Chrome, which should be updated to the latest version.

Using Selenium ChromeDriver we are able to Chrome and interact with it to successfuly run our automated tests.
Using Selenium we are able to manipulate page elements to search for errors and bugs at the application.
Using Nlog we are able to log our steps and write logs to check of what happened after we ran our tests.
Using ExtentReports we are able to generate .html reports for a review of all the tests we ran, whether they passed or failed, and take screenshots.

Instructions on how to use:
1. Download Visual Studio from https://visualstudio.microsoft.com/vs/
2. On the web site hover over Download Visual Studio dropdown. Choose option: Community 2022
3. Run VisualStudioSetup.exe. Visual Studio Installer will open.
4. On the first step click Continue. This will start a short download and installation proccess.
5. On the next step the Installer will offer for you to choose workloads to install in addtition to Visual Studio Core.
6. In Desktop & Mobile section choose .NET desktop development workload.
7. On the right side a copule of optional features will pop up. No need to touch those.
8. Under the workloads you can choose Location where you want install Visual Studio. Then in the bottom right corner click Install button.
9. After the instalaltion open Visual Studio.
10. You can sign in with your account or choose Skip this for now. Then click on Start Visual Studio button.
11. In then next window under Get started choose option to clone a repository.
12. In the Repository location copy the URL: https://github.com/Darzi88/BakerOnlineAutomation
13. You can change the Path where the repository will be copied. Then click Clone.
14. Inside the Build tab click Build Solution (shortcut Ctrl+Shift+B)
15. On the right side of the screen right click on the Solution and click Manage NuGet Packages for Solution...
16. In the search field type chrome
17. Click on Selenium.WebDriver.ChromeDriver and install latest stable version.
18. Also make sure you have the latest version of Google Chrome on your PC.
19. To update Google Chrome open it and then in the top right corner of the screen click on three dots -> Help -> About Google Chrome. This will update Google Chrome to the latest version automatically.
20. In the Visual Studion inside the View tab click Test Explorer (shortcut Ctrl+E, T). You can drag and drop it to the left middle of the screen so you can view it side by side with the Solution.
21. In the Test Explorer you can start running tests by right clicking on categories or single tests and clicking Run.
22. After the test or tests have ran, logs and a report will be generated in the C:\temp\BakerOnlineAutomation. There you can see outcome of the tests ran.

Pages description
BasePage -> A base page from which other pages inherit common properties and methods.
RegistrationPage -> A page representing Registration page in the application. Contains method for filling out and submiting registation form.
ExternalEmailPage -> An external page not connected to the application that visits the https://10minutemail.net/ website. Used for generating new valid emails and reseting the password.
HomePage -> A page representing Home page in the application.
UserMenu -> A page representing User Menu in the application. Contains method for logging out the user.
LoginPage -> A page representing Login page in the application. Contains method for inputing login information.
ForgotPasswordPage -> A page representing Forgot Password page in the application. Contains method for submiting the email page which reset password link will be sent to.
ResetPasswordPage -> A page representing Reset Password page in the application. Contains method to submit a new password.
SearchPage -> A page representing Search page in the application. Used for checking if products are visible on the page and adding them to the Cart.
ProductPage -> A page representing Product page in the application. Used for checking if product details are visible.
CheckoutPage -> A page representing Checkout in the application to which the user is redirected after placing the order. Contains methods for completing the order.
Header -> A page representing Header in the application. Contains methods for searching products and using the Cart.
Cart -> A page representing Cart in the application.

Test Pages description
BaseTest -> A base test page from which other test pages inherit common properties and methods.
BakerOnlineUserTests -> A test page taht contains tests concerning users.
BakerOnlineProductsTests -> A test page taht contains tests concerning products.

Other classes description
TestUser -> A user class for passing first name, last name, password, email address and phone number properties.
Product -> A product class containing product enums.
Reporter -> A class that takes care of generating reports and logs.
NamespaceSetup -> A class that makes sure only one report will be genereted per test run. Runing multiple tests will put them in the same report.
ScreenshoTaker -> A class used for taking screenshots. Will automaticaly take a screenshot after a test has failed.

Tests description
Registering a user
Test is located in BakerOnlineUserTests TestClass PBI1, under the TestMethod TCID1.
Steps:
1. Opening the Registration page.
2. Opening the External Email page in a new tab.
3. Copying generateg email address and storing it.
4. Switching back to the Registration page tab.
5. Filling out and submiting the registration form.
6. Asserting that the page was redirected to the Home page and that the user is logged in.

Logging in and loging out of the application
Test is located in BakerOnlineUserTests TestClass PBI1, under the TestMethod TCID2.
Steps:
1. Opening the Login page.
2. Inputing the login info and attempting to log in.
3. Asserting that the page was redirected to the Home page and that the user is logged in.
4. Opening te User Menu and attempting to log out.
5. Asserting that the user is logged out.

Reseting the password and then logging in using the new password
Test is located in BakerOnlineUserTests TestClass PBI1, under the TestMethod TCID3.
Steps:
1. Opening the Registration page.
2. Opening the External Email page in a new tab.
3. Copying generateg email address and storing it.
4. Switching back to the Registration page tab.
5. Filling out and submiting the registration form.
6. Asserting that the page was redirected to the Home page and that the user is logged in.
7. Opening te User Menu and attempting to log out.
8. Asserting that the user is logged out.
9. Opening the Forgot Password page.
10. Submiting the email address.
11. Switching to the External Email tab.
12. Waiting for a reset password link (can take up to 2 minutes).
13. Opening the link in a new tab.
14. Asserting that the Reset Password page is visible.
15. Submiting a new password address.
16. Opening te User Menu and attempting to log out.
17. Opening the Login page.
18. Inputing the login info with the new password and attempting to log in.
19. Asserting that the page was redirected to the Home page and that the user is logged in.

Browsing products as a visitor
Test is located in BakerOnlineProductsTests TestClass PBI2, under the TestMethod TCID1.
Steps:
1. Opening the Home page.
2. Searching for a product in the search field.
3. Asserting that the product appears on the Search page.
4. Clicking on the product.
5. Asserting that the product details are visible.

Browsing products as a user
Test is located in BakerOnlineProductsTests TestClass PBI2, under the TestMethod TCID2.
Steps:
1. Opening the Login page.
2. Inputing the login info and attempting to log in.
3. Asserting that the page was redirected to the Home page and that the user is logged in.
4. Searching for a product in the search field.
5. Asserting that the product appears on the Search page.
6. Clicking on the product.
7. Asserting that the product details are visible.

Ordering a product as a user
Test is located in BakerOnlineProductsTests TestClass PBI2, under the TestMethod TCID3.
Steps:
1. Opening the Login page.
2. Inputing the login info and attempting to log in.
3. Asserting that the page was redirected to the Home page and that the user is logged in.
4. Searching for a product in the search field.
5. Asserting that the product appears on the Search page.
6. Adding the product to the Cart.
7. Asserting that the Cart contains 1 product.
8. Clicking on the Cart.
7. Asserting that the Cart contains chosen product.
8. Ordering the product.
9. Assert Checkout page is visible.
10. Chosing option to Pick Up Order at the Shop.
11. Picking a date and time of the arrival.
12. Chosing Payment at Pick Up as the payment method.
13. Asserting that the Checkout Success page is visible.

Ordering two products as a user
Test is located in BakerOnlineProductsTests TestClass PBI2, under the TestMethod TCID4.
Steps:
1. Opening the Login page.
2. Inputing the login info and attempting to log in.
3. Asserting that the page was redirected to the Home page and that the user is logged in.
4. Searching for a product in the search field.
5. Asserting that the product appears on the Search page.
6. Adding the product to the Cart.
7. Asserting that the Cart contains 1 product.
4. Searching for a second product in the search field.
5. Asserting that the product appears on the Search page.
6. Adding the product to the Cart.
7. Asserting that the Cart contains 2 products.
8. Clicking on the Cart.
7. Asserting that the Cart contains chosen products.
8. Ordering products.
9. Assert Checkout page is visible.
10. Chosing option to Pick Up Order at the Shop.
11. Picking a date and time of the arrival.
12. Chosing Payment at Pick Up as the payment method.
13. Asserting that the Checkout Success page is visible.
