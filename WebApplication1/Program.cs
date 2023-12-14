using WebApplication1;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async(User loginData, ModelDB db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.EMail == loginData.EMail && p.Password == loginData.Password);
    if (person == null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.EMail!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.EMail
    };
    return Results.Json(response);  
});

app.MapGet("api/priceList", [Authorize] async (ModelDB db) => await db.PriceLists!.ToListAsync());
app.MapGet("api/product", [Authorize] async (ModelDB db) => await db.Products!.ToListAsync());
app.MapGet("api/priceList/{Id:int}", [Authorize] async (ModelDB db, int id) => await db.PriceLists!.Where(u=>u.Id == id).FirstOrDefaultAsync());
app.MapPost("api/priceList", [Authorize] async (PriceList priceList, ModelDB db) =>
{ 
    await db.PriceLists!.AddAsync(priceList);
    await db.SaveChangesAsync();
    return priceList;
});
app.MapPost("api/product", [Authorize] async (Product product, ModelDB db) =>
{
    await db.Products!.AddAsync(product);
    await db.SaveChangesAsync();
    return product;
});
app.MapDelete("api/priceList/{Id:int}", [Authorize] async (int id, ModelDB db) =>
{
    PriceList? priceList = await db.PriceLists.Where(u => u.Id == id).FirstOrDefaultAsync();
    if (priceList == null) return Results.NotFound(new { message = "Поставка не найдена" });
    db.PriceLists.Remove(priceList);
    await db.SaveChangesAsync();
    return Results.Json(priceList);
});
app.MapDelete("api/product/{Id:int}", [Authorize] async (int Id, ModelDB db) =>
{
    Product? product = await db.Products.Where(u => u.Id == Id).FirstOrDefaultAsync();
    if (product == null) return Results.NotFound(new { message = "Продажа не найдена" });
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.Json(product);
});
app.MapPut("api/priceList", [Authorize] async (PriceList priceList, ModelDB db) =>
{
    PriceList? a = await db.PriceLists.Where(u => u.Id == priceList.Id).FirstOrDefaultAsync();
    if (a == null) return Results.NotFound(new { message = "Пользователь не найден" });
    a.Name = priceList.Name;
    a.Price = priceList.Price;
    db.PriceLists.Update(a);
    await db.SaveChangesAsync();
    return Results.Json(a);
});
app.MapPut("api/product", [Authorize] async (Product product, ModelDB db) =>
{
    Product? s = await db.Products.Where(u => u.Id == product.Id).FirstOrDefaultAsync();
    if (s == null) return Results.NotFound(new { message = "Продажа не найдена" });
    s.SellDate = product.SellDate;
    s.Name = product.Name;
    s.SoldCount = product.SoldCount;
    s.SoldPrice = product.SoldPrice;
    db.Products.Update(s);
    await db.SaveChangesAsync();
    return Results.Json(s);
});
app.Run();