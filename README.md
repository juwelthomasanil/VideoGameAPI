🎮 Video Game CRUD API (.NET 9 + EF Core + SQL Server)

A simple CRUD Web API built with ASP.NET Core 9, Entity Framework Core (Code-First), and SQL Server.
This project demonstrates how to create, read, update, and delete video game records using a RESTful API.

🧱 Features

✅ ASP.NET Core 9 Web API

✅ Entity Framework Core (Code-First Approach)

✅ SQL Server Database Integration

✅ Dependency Injection for DbContext

✅ Async CRUD Operations

✅ Data Seeding with OnModelCreating

✅ RESTful API Design with proper HTTP Status Codes

⚙️ Tech Stack
Layer	Technology
Backend	.NET 9 (C#, ASP.NET Core Web API)
ORM	Entity Framework Core
Database	SQL Server / SQL Express
Migrations	EF Core Code-First
IDE	Visual Studio 2022 / VS Code

📂 Project Structure
VideoGameAPI/
│
├── Controllers/
│   └── VideoGameController.cs
│
├── Models/
│   └── VideoGame.cs
│
├── Data/
│   └── VideoGameDbContext.cs
│
├── Migrations/
│   └── (Auto-generated migration files)
│
├── appsettings.json
├── Program.cs
└── README.md

🧠 How It Works
1️⃣ Define the Model
public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Platform { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
}

2️⃣ Configure the DbContext
public class VideoGameDbContext : DbContext
{
    public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options) { }

    public DbSet<VideoGame> VideoGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<VideoGame>().HasData(
            new VideoGame
            {
                Id = 1,
                Title = "Spiderman",
                Platform = "PS5",
                Developer = "Insomniac",
                Publisher = "Sony"
            }
        );
    }
}

3️⃣ Register DbContext in Program.cs
builder.Services.AddDbContext<VideoGameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

4️⃣ Configure Connection String
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLExpress;Database=VideoGameDb;Trusted_Connection=true;TrustServerCertificate=true;"
}

🧩 CRUD Endpoints
Method	Endpoint	Description
GET	/api/VideoGame	Get all video games
GET	/api/VideoGame/{id}	Get video game by ID
POST	/api/VideoGame	Add a new video game
PUT	/api/VideoGame/{id}	Update existing video game
DELETE	/api/VideoGame/{id}	Delete a video game
🧰 Setup & Run Locally
🔹 Prerequisites

.NET 9 SDK

SQL Server or SQL Express

Visual Studio 2022 or VS Code

🔹 Steps

Clone the repository

git clone https://github.com/yourusername/VideoGameAPI.git
cd VideoGameAPI


Update the connection string

Open appsettings.json

Change Server and Database values if needed (e.g., Server=YOURSERVERNAME)

Run EF Core migrations

Add-Migration InitialCreate
Update-Database


Run the project

dotnet run


The API will start on:

https://localhost:5001  or  http://localhost:5000

🧪 Example JSON Body (POST)
{
  "title": "God of War",
  "platform": "PS5",
  "developer": "Santa Monica Studio",
  "publisher": "Sony"
}

📬 API Response Example

GET /api/VideoGame/1

{
  "id": 1,
  "title": "Spiderman",
  "platform": "PS5",
  "developer": "Insomniac",
  "publisher": "Sony"
}

🧠 Understanding Code-First Migrations

Add-Migration <Name> → Creates migration scripts from your model classes.

Update-Database → Applies those migrations to your actual SQL Server DB.

You don’t upload the DB to GitHub — only your migration files.

Anyone cloning your project can rebuild the same DB by running:

Update-Database

💡 Future Enhancements

Add validation with DataAnnotations

Implement Repository Pattern

Add Swagger UI (OpenAPI Documentation)

Integrate JWT Authentication

Deploy to Azure / AWS

👤 Author

Juwel Thomas Anil
💼 Software Engineer (.NET Core, Microservices, AWS)
📧 juwelthomasanil.job@gmail.com
