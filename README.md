# Product CRUD – ASP.NET MVC 5
A beginner-friendly Product management app using C#, ADO.NET, SQL Server, and Bootstrap.

---

## Project Structure

```
ProductCRUD/
├── Database/
│   └── CreateTable.sql             ← Run this first in SQL Server
├── Models/
│   └── Product.cs                  ← Product data model
├── Repository/
│   └── ProductRepository.cs        ← All DB operations (ADO.NET)
├── Controllers/
│   └── ProductsController.cs       ← Handles HTTP requests
├── Views/
│   ├── _ViewStart.cshtml           ← Wires up the layout
│   ├── Shared/
│   │   └── _Layout.cshtml          ← Master page (navbar, Bootstrap)
│   └── Products/
│       ├── Index.cshtml            ← Product list
│       ├── Create.cshtml           ← Add product form
│       ├── Edit.cshtml             ← Edit product form
│       ├── Details.cshtml          ← View product info
│       └── Delete.cshtml           ← Delete confirmation
└── Web.config                      ← Connection string lives here
```

---

## Steps to Run

### Step 1 – Create the Project in Visual Studio

1. Open **Visual Studio 2019 or 2022**.
2. Click **Create a new project**.
3. Select **ASP.NET Web Application (.NET Framework)** → click **Next**.
4. Name the project **ProductCRUD**, choose a folder, click **Create**.
5. Select the **MVC** template → click **Create**.

> **Important**: Choose **.NET Framework 4.7.2** (or 4.8) from the dropdown.

---

### Step 2 – Set Up the Database

1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to your SQL Server instance.
3. Open a **New Query** window.
4. Copy and paste the contents of **`Database/CreateTable.sql`** into the query window.
5. Click **Execute** (or press **F5**).

This creates:
- The `ProductDB` database (if you uncommented those lines)
- The `Products` table with sample data

---

### Step 3 – Update the Connection String

Open `Web.config` and find the `<connectionStrings>` section.

Update `Data Source` to match **your** SQL Server instance:

| Your Setup                     | Data Source value            |
|--------------------------------|------------------------------|
| Visual Studio (LocalDB)        | `(localdb)\MSSQLLocalDB`     |
| SQL Server Express             | `.\SQLEXPRESS`               |
| Default local SQL Server       | `localhost` or `.`           |
| Named instance                 | `SERVER_NAME\INSTANCE_NAME`  |

Example for SQL Server Express:
```xml
<add name="ProductDB"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=ProductDB;Integrated Security=True;"
     providerName="System.Data.SqlClient" />
```

---

### Step 4 – Add the Files to Your Project

Copy the provided files into your project folder, matching the structure above:

| File | Destination in Visual Studio |
|------|------------------------------|
| `Models/Product.cs` | `Models/` folder |
| `Repository/ProductRepository.cs` | Create a `Repository/` folder |
| `Controllers/ProductsController.cs` | `Controllers/` folder |
| `Views/Shared/_Layout.cshtml` | Replace the default layout |
| `Views/Products/*.cshtml` | Create a `Views/Products/` folder |
| `Views/_ViewStart.cshtml` | `Views/` folder |
| `Web.config` | Merge the `<connectionStrings>` section |

> **Tip**: Right-click a folder in Solution Explorer → **Add → Existing Item** to add files.

---

### Step 5 – Verify the Namespace

All files use the namespace `ProductCRUD`. If your project has a different name,
do a **Find & Replace** (Ctrl+H) in Visual Studio:

- Find: `ProductCRUD`
- Replace: `YourProjectName`

---

### Step 6 – Run the App

1. Press **F5** (or click the green **Run** button) in Visual Studio.
2. The browser opens. Navigate to:
   ```
   http://localhost:{port}/Products
   ```
3. You should see the product list with the sample data.

---

## CRUD Operations

| Action  | URL                       | Description              |
|---------|---------------------------|--------------------------|
| List    | `/Products`               | View all products        |
| Create  | `/Products/Create`        | Add a new product        |
| Details | `/Products/Details/{id}`  | View a specific product  |
| Edit    | `/Products/Edit/{id}`     | Update a product         |
| Delete  | `/Products/Delete/{id}`   | Delete confirmation page |

---

## Key Concepts Used

| Concept | Where Used |
|---------|------------|
| `SqlConnection` | Opens a connection to SQL Server |
| `SqlCommand` | Executes SQL queries |
| `SqlDataReader` | Reads rows returned by SELECT |
| `ExecuteNonQuery()` | Runs INSERT/UPDATE/DELETE |
| `cmd.Parameters.AddWithValue()` | Prevents SQL injection |
| `[ValidateAntiForgeryToken]` | Prevents CSRF attacks on forms |
| `TempData["Success"]` | Shows one-time success messages |
| Bootstrap 5 | Responsive table, forms, badges |

---

## Troubleshooting

**"Cannot open database ProductDB"**
→ Check your connection string `Data Source` name.
→ Make sure you ran `CreateTable.sql` first.

**"A network-related error occurred"**
→ SQL Server service may not be running.
→ Open **Services** (Win+R → `services.msc`) and start **SQL Server**.

**Page not found / 404**
→ Make sure you navigate to `/Products`, not just `/`.
→ Or update `RouteConfig.cs` to set the default controller to `Products`.
