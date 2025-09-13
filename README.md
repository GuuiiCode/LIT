# ✅ LIT System

## ⚙️ Technologies Used

| Technology | Version              |
|------------|----------------------|
| .NET       | 7.0                  |
| Angular    | 14.0.0               |
| Node.js    | 16.14.2              |
| Bootstrap  | 5.0                  |
| MongoDB    | Latest version (via Docker) |
| Docker     | Recommended          |

---

## 📁 Project Structure

```
LIT/
├── Back-End/                 # Backend .NET 7 API
│   ├── LIT.Application/
│   ├── LIT.Domain/
│   ├── LIT.Infra/
│   ├── LIT.Tests/
│   ├── LIT.WebAPI/
│   ├── .dockerignore
│   ├── Dockerfile            # Backend Dockerfile located inside the Back-End folder
│   ├── launchSettings.json
│   └── LIT.sln               # Solution file
├── Fron-End/
│   └── lit-web/              # Angular Frontend
│       ├── .angular/
│       ├── .vscode/
│       ├── node_modules/
│       ├── src/
│       ├── Dockerfile        # Frontend Dockerfile located inside the lit-web folder
│       ├── angular.json
│       ├── package.json
│       ├── package-lock.json
│       ├── README.md
│       └── other frontend files
├── docker-compose.yml        # Docker orchestration file
├── docker-compose.override.yml
├── docker-compose.dcproj
└── README.md
```

---

## 🚧 Prerequisites

- Docker installed and running.
- (Optional) Git to clone the repository.
- (Optional) Node.js and npm if you want to run the frontend locally without Docker.
- (Optional) Angular CLI (recommended) to work with the Angular frontend locally.
- (Optional) .NET 7 SDK if you want to build and run the backend API locally without Docker.

---

## ⬇️ How to Clone the Project

```bash
git clone https://github.com/GuuiiCode/LIT.git
cd LIT
```

---

## ▶️ How to Run the Project with Docker

### 🧱 Step 1: Build and Run

```bash
docker-compose up --build
```

This command will:

- Build and run the .NET backend API container
- Start the MongoDB container with the defined credentials
- Build and serve the Angular frontend

---

### 🌐 Available Endpoints

- **Angular Frontend:** [http://localhost:4200](http://localhost:4200)
- **.NET API (Swagger):** [http://localhost:3000/swagger/index.html](http://localhost:3000/swagger/index.html)
- **MongoDB:** Port `27017` (username: `admin`, password: `admin`)

---

## ▶️ Running the Backend (.NET) with Visual Studio (without Docker)

1. Open the solution file `LIT.sln` in **Visual Studio**.

2. In the **Solution Explorer**, locate the project `LIT.WebAPI`.

3. Right-click on `LIT.WebAPI` and select **Set as Startup Project**.

4. Make sure the configuration is set to `Debug` and platform is `Any CPU` (top toolbar).

5. Press **F5** to run with debugging, or **Ctrl + F5** to run without debugging.

---

> ✅ Tip: Visual Studio will open the browser automatically with the Swagger UI if it's configured.

---

## ▶️ Running the Frontend (Angular)

You can run the Angular frontend manually via Command Line (without Docker):

### 💻 Using Command Line (CMD / Terminal)

1. Open your terminal or command prompt.
2. Navigate to the frontend project folder:

```bash
cd Fron-End/lit-web
npm install
npm start
```

The frontend will be available at: [http://localhost:4200](http://localhost:4200)

> ✅ **The application will live-reload when you edit source files.**

> ⚠️ **Ensure that Node.js and optionally Angular CLI are installed globally.**

---

## 🧪 Running Test Coverage (Backend) with Visual Studio

If you're using **Visual Studio**, you can easily run test coverage for the backend project by following these steps:

1. **Open the solution** (`LIT.sln`) in Visual Studio.

2. In the **Test Explorer** window, make sure your tests are discovered. If not, build the solution or run "Run All Tests".

3. To run test coverage:
   - Click on the dropdown arrow next to the **Run All Tests** button.
   - Select **Analyze Code Coverage for All Tests**.

4. Visual Studio will run all tests and show a **Code Coverage Results** window with detailed coverage info per file and line.

5. You can explore the coverage results, see percentages, and identify uncovered code directly inside Visual Studio.

---

> **Note:**  
> If you don't see the "Analyze Code Coverage" option, make sure you're using Visual Studio Enterprise or another edition that supports code coverage.

---


## 🐳 docker-compose.yml File – Overview

```yaml
version: '3.4'

services:
  lit.webapi:
    image: ${DOCKER_REGISTRY-}litwebapi
    container_name: BackEnd 
    build:
      context: .
      dockerfile: Back-End/LIT.WebAPI/Dockerfile
    depends_on: 
      - mongodb
    environment:
      MongoDBSettings__ConnectionString: "mongodb://admin:admin@mongodb:27017"
      ASPNETCORE_ENVIRONMENT: Development
    ports:
      - "3000:80"

  mongodb:
    image: mongo:latest
    container_name: MongoDB
    ports:
      - "27017:27017"
    environment:
      MONGO_INITDB_ROOT_USERNAME: admin
      MONGO_INITDB_ROOT_PASSWORD: admin
    volumes:
      - mongodb_data:/data/db
      
  frontend:
    container_name: FrontEnd
    build:
      context: ./Fron-End/lit-web
      dockerfile: Dockerfile
    ports:
      - "4200:80"
    depends_on:
      - lit.webapi

volumes:
  mongodb_data:
```

---

## 🐳 Dockerfiles

### 🔹 Dockerfile (Backend)

```dockerfile
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

RUN apt-get update && apt-get upgrade -y && apt-get clean && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LIT.WebAPI/LIT.WebAPI.csproj", "LIT.WebAPI/"]
COPY ["LIT.Application/LIT.Application.csproj", "LIT.Application/"]
COPY ["LIT.Domain/LIT.Domain.csproj", "LIT.Domain/"]
COPY ["LIT.Infra/LIT.Infra.csproj", "LIT.Infra/"]
RUN dotnet restore "./LIT.WebAPI/./LIT.WebAPI.csproj"
COPY . .
WORKDIR "/src/LIT.WebAPI"
RUN dotnet build "./LIT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LIT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LIT.WebAPI.dll"]
```

### 🔹 frontend.Dockerfile (Angular)

```dockerfile
FROM node:16.14.2 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Servir com Nginx
FROM nginx:alpine
COPY --from=build /app/dist/lit-web /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

> **Note:** If the frontend Dockerfile is named simply `Dockerfile`, it may conflict with the backend Dockerfile. Rename it to `frontend.Dockerfile` and update the `docker-compose.yml` accordingly.

---

## 🛑 Stop the Application

```bash
docker-compose down
```

---

## 🔧 Troubleshooting Tips

- Make sure Docker is running.
- Ensure ports **3000**, **4200**, and **27017** are not in use.
- To rebuild everything from scratch:

```bash
docker-compose build --no-cache
docker-compose up
```



