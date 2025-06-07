# DemoShopApi

## Giới thiệu

DemoShopApi là một hệ thống Web API quản lý người dùng, phân quyền và sản phẩm, xây dựng bằng **ASP.NET Core** với kiến trúc nhiều tầng và sử dụng **Entity Framework Core Code First** để quản lý cơ sở dữ liệu SQL Server.

## Tính năng nổi bật

* Đăng ký, đăng nhập, phân quyền qua JWT
* Mã hóa mật khẩu với BCrypt
* Quản lý người dùng và vai trò (Admin/User)
* Quản lý sản phẩm: CRUD sản phẩm gắn với người tạo
* Đầy đủ API lấy danh sách user, role, product
* Tự động sinh database và bảng từ code (Code First, migration)
* Mapping Entity ↔ DTO với AutoMapper
* Định nghĩa Unit of Work & Repository cho thao tác dữ liệu an toàn
* Tài liệu API với Swagger

## Kiến trúc & Công nghệ

* **ASP.NET Core Web API** (.NET 9.0)
* **Entity Framework Core (EF Core) – Code First**
* **AutoMapper** (mapping entity/DTO)
* **JWT Bearer Authentication**
* **BCrypt.Net** (hash password)
* **Swagger / Swashbuckle** (API document)

**Kiến trúc nhiều tầng:**

* **Domain**: Entities, Enums
* **Infrastructure**: DbContext, Repository, UnitOfWork
* **Application**: DTOs, Services, Interfaces, Mapping
* **API**: Controllers, Config, Middleware

## Mô hình cơ sở dữ liệu (Code First)

* **Role** (`Id`, `Name`)
* **User** (`Id`, `Username`, `PasswordHash`, `RoleId`)
* **Product** (`Id`, `Name`, `Description`, `Price`, `UserId`)

**Quan hệ:**

* Mỗi User thuộc một Role
* Mỗi Product gắn với một User (là người tạo)

Dữ liệu Role được seed tự động (`Admin` = 0, `User` = 1) từ Enum.

## Hướng dẫn cài đặt & chạy

### 1. Clone & cấu hình repo

```bash
git clone https://github.com/YOUR-USERNAME/DemoShopApi.git
cd DemoShopApi
```

### 2. Tạo file cấu hình

* Copy `appsettings.json.example` → `appsettings.json`
* **Không** public file chứa key thật lên GitHub
* Chỉnh sửa connection string & JWT key cho phù hợp

### 3. Cài đặt dependencies

```bash
dotnet restore
```

### 4. Tạo database bằng migration (EF Core Code First)

```bash
dotnet ef database update \
  --project ./DemoShopApi.Infrastructure \
  --startup-project ./DemoShopApi.Api
```

### 5. Chạy ứng dụng

```bash
dotnet run --project ./DemoShopApi.Api
```

Truy cập Swagger tại: `https://localhost:7008/swagger/index.html` (hoặc cổng khác tùy cấu hình).

## Ví dụ response API

### Đăng ký user

**Endpoint:** `POST /api/auth/register`

**Request body:**

```json
{
  "username": "khoidev",
  "password": "123456",
  "roleId": 1
}
```

**Response:**

```json
"Register success"
```

### Đăng nhập

**Endpoint:** `POST /api/auth/login`

**Request body:**

```json
{
  "username": "khoidev",
  "password": "123456"
}
```

**Response:**

```json
"<JWT Token>"
```

### Lấy tất cả sản phẩm

**Endpoint:** `GET /api/product`

**Response:**

```json
[
  { "id": 1, "name": "iPhone 16", "description": "Hàng mới", "price": 500, "userId": 2 }
]
```

## Cách hoạt động Code First

* Định nghĩa entity (class C#)
* Tạo migration mới
* Cập nhật database tự động
* Thay đổi model → migration → update database
* Dữ liệu seed (Role) sẽ được áp dụng khi migration chạy

## Một số lệnh dev nhanh

* Tạo migration mới:

  ```bash
  dotnet ef migrations add YourMigrationName \
    --project ./DemoShopApi.Infrastructure \
    --startup-project ./DemoShopApi.Api
  ```
* Cập nhật database:

  ```bash
  dotnet ef database update \
    --project ./DemoShopApi.Infrastructure \
    --startup-project ./DemoShopApi.Api
  ```

## Lưu ý bảo mật

* Không push file `appsettings.json` thật lên GitHub – chỉ dùng file mẫu và biến môi trường.
* JWT key và connection string nên được quản lý qua User Secrets hoặc biến môi trường.

## Tác giả

* **Name:** Pham-Dang-Khoi
* **GitHub:** https://github.com/PDK-23/
