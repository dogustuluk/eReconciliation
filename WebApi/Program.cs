using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//autofac config. en yukarda belirt
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBussinessModule()));

// Add services to the container.
IConfiguration configuration = builder.Configuration;


builder.Services.AddControllers();

//cors=> baglantimizin ayarini yaptigimiz yer yani web apimize ulasacak apileri burada belirliyoruz. apinin ne yetkileri olacagini, apiye ulastiktan sonra neler yapilabilecegini belirtiriz.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7286")); //with origins ile izin verecegimiz apiyi yazariz. tum apilere izin icin allow ile gecebiliriz.
});

var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

//token options
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false, //token"imin dusmesini istemiyorum, sistemde kalmasini istiyorum o yuzden false olarak verdim. eger true olarak verirsem islem esnasinda sistemden dusmem gibi sorunlar meydana gelebilmektedir.
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//cors"u ekle
app.UseCors(builder => builder.WithOrigins("https://localhost:7286").AllowAnyHeader());//AllowAnyHeader => bu adresten gelen tum istekleri karsila anlaminda kullanilir.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
