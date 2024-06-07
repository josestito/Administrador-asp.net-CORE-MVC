using Proyecto_De_Titulo_Organizado.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ServicioTipoEmpleado, ServicioTipoEmpleado>();
builder.Services.AddTransient<ServicioEmpleado, ServicioEmpleado>();
builder.Services.AddTransient<ServicioEspacio, ServicioEspacio>();
builder.Services.AddTransient<ServicioHorario, ServicioHorario>();
builder.Services.AddTransient<ServicioRegistro, ServicioRegistro>();
//IMPLEMENTAR EL SERVICIO DE SESIONES
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//PODER USAR EL SERVICIO DE SESIONES
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
