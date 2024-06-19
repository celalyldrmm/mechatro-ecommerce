# Mechatro E-Commerce Project
This project includes a C# application that simulates a simple e-commerce system.

## About the Project
This project allows registered customers to add desired products to their cart, make purchases, and manage products, brands, categories, and their statuses as a seller. It also enables settings for displaying products on the homepage. Additionally, it uses an MSSQL database to store products, user information, and related tables.

## Getting Started
Before running the project, you need to follow the steps below to complete the necessary setup.

### Requirements
- Visual Studio or a C# compiler
- MSSQL database & SQL Server Management Studio

### Installation

1- Download or clone the project to your computer by clicking the "Code" button located at the top right corner of the GitHub repository's main page.

 ```bash
    git clone https://github.com/hmtcan/mechatro-ecommerce.git
```

2- Open the `mechatro-ecommerce.sln` file in Visual Studio or your preferred C# development environment.

3- Add the `MechatroProject.bak` file located in the project's root directory to SQL Server Management Studio and update your connection settings in the `appsettings.json` file.

  ```json
    {
      "ConnectionString": {
   "MechatroProject": "Server=.;Database=MechatroProject;Trusted_Connection=True;TrustServerCertificate=True;"
      }
    }
  ```
4- Compile and run your project files.

## Usage
When the project is started, you will be greeted with a simple e-commerce interface. Here, you can register on the site, add products to your cart, and make purchases. Access to the admin panel is available from the homepage, where you can log in with "username: admin, password: admin". In the admin panel, you can add, delete, and update categories, brands, products, and statuses, as well as configure the products to be displayed and the fixed seller information in the settings section.

## Contributing
If you would like to contribute to the project, please create a pull request. You can add new features or fix bugs.

