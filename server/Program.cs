using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

//cors
  builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        //swager
        builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), 
    new MySqlServerVersion(new Version(9, 0, 2)))); // 
 
var app = builder.Build();

app.UseCors("MyPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapPost("/add", async(ToDoDbContext db, Item item) =>{
    item.IsComplete=false;
await db.Items.AddAsync(item);
db.SaveChanges();
return "added success!";
});

app.MapPut("/put/{id}",async(ToDoDbContext db,int id,Item item)=>
{
   
   var x=await db.Items.FindAsync(id);
//    if(x is null)
//         return Results.NotFound();
   if(x!=null){
        if(item.Name!=null)
            x.Name=item.Name;
        if(item.IsComplete!=null)
            x.IsComplete=item.IsComplete;
    db.Items.Update(x);
 
    db.SaveChanges();
   
   }
    return "put!!!!!!!!!!!";

 });

 app.MapDelete("/remove/{id}", async (int id, ToDoDbContext db) =>
{
    var x=await db.Items.FindAsync(id);
    if (x!=null)
    {
        db.Items.Remove(x);
        await db.SaveChangesAsync();
    }

    return "delete";
});
app.Run();
