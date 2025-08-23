# 📖 BlogsAPI

A RESTful API built to provide a robust backend for a blogging platform. The API manages **authors, posts, and comments** with full CRUD (Create, Read, Update, Delete) operations.  

---

## 🚀 Project Overview
The **BlogsAPI** is designed to be scalable and easy to integrate with various frontend applications. It ensures full control over blog content and user data.  

Key features:
- Manage **Authors, Posts, and Comments**
- Complete **CRUD operations**
- Structured responses using a `ResultViewModel`
- Built with **ASP.NET Core** and **Entity Framework Core**

---

## 📌 API Endpoints

### 👤 Authors
- `GET /api/Authors` → Get all authors  
- `GET /api/Authors/{id}` → Get author by ID  
- `GET /api/Authors/{id}/Posts` → Get all posts by a specific author  
- `POST /api/Authors/Add` → Create a new author  
- `PUT /api/Authors/Update/{id}` → Update an author  
- `DELETE /api/Authors/Delete/{id}` → Delete an author  

### 📝 Posts
- `GET /api/Posts` → Get all posts  
- `GET /api/Posts/{id}` → Get post by ID  
- `GET /api/Posts/{postId}/Comments` → Get all comments for a post  
- `POST /api/Posts/Add` → Create a new post  
- `PUT /api/Posts/Update/{id}` → Update a post  
- `DELETE /api/Posts/Delete/{id}` → Delete a post  

### 💬 Comments
- `GET /api/Comments` → Get all comments  
- `GET /api/Comments/{id}` → Get comment by ID  
- `POST /api/Comments/Add` → Create a new comment  
- `PUT /api/Comments/Update/{id}` → Update a comment  
- `DELETE /api/Comments/Delete/{id}` → Delete a comment  

---

## 🗂 Data Models

### Author
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "bio": "Tech blogger",
  "joinDate": "2025-01-01T00:00:00"
}
```

### Post
```json
{
  "id": 1,
  "title": "Introduction to BlogsAPI",
  "content": "This is a sample post...",
  "createdDate": "2025-01-10T00:00:00",
  "updatedDate": "2025-01-12T00:00:00",
  "authorId": 1
}
```

### Comment
```json
{
  "id": 1,
  "content": "Great article!",
  "createdDate": "2025-01-15T00:00:00",
  "postId": 1,
  "authorId": 2
}
```

### ResultViewModel
```json
{
  "isSuccess": true,
  "message": "Request completed successfully",
  "data": {}
}
```


## 🛠️ Technologies Used
- **.NET / ASP.NET Core** → RESTful API framework  
- **Entity Framework Core** → ORM for database operations  
- **SQL Server** → Relational database  
- **Swagger / OpenAPI** → Interactive API documentation and Scalar UI  

---



## ✅ Conclusion
The **BlogsAPI** provides a complete, scalable, and maintainable backend for blogging platforms. It’s ready for frontend integration and future expansion.  
