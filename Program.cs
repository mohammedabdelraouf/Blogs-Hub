using BlogsAPI.Data;
using BlogsAPI.Services;
using Microsoft.EntityFrameworkCore; 
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BlogsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") // to allow react
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS using the policy you defined
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
