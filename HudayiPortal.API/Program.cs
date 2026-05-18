using HudayiPortal.API.Middlewares; // YENİ: Global Exception Handler için
using HudayiPortal.Application;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Application.Settings;
using HudayiPortal.Infrastructure;
using HudayiPortal.Infrastructure.Hubs;
using HudayiPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// YENİ: Modern Global Exception Handler Kayıtları
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings.Issuer,
		ValidAudience = jwtSettings.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddAuthorization();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
	options.AddPolicy("SignalRPolicy", policy =>
	{
		policy.SetIsOriginAllowed(_ => true) // Test için tüm adreslere izin ver
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials(); // SignalR'ın çalışması için bu şarttır!
	});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// YENİ: Eski middleware yerine yeni IExceptionHandler sistemini kullanıyoruz
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("SignalRPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");
// ─── Seed Data: IslemKategorileri ───
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<HudayiPortal.Infrastructure.Persistence.ApplicationDbContext>();
	if (!db.IslemKategorileri.Any())
	{
		db.IslemKategorileri.AddRange(
			// Gelir kategorileri (YonId=1)
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Aidat", YonId = 1, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Bağış", YonId = 1, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Kira Geliri", YonId = 1, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			// Gider kategorileri (YonId=2)
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Maaş", YonId = 2, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Mutfak", YonId = 2, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Fatura", YonId = 2, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Bakım-Onarım", YonId = 2, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.IslemKategorileri { KategoriAdi = "Kırtasiye", YonId = 2, OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false }
		);
		await db.SaveChangesAsync();
	}
}
// ─── Seed Data: YoklamaTurleri ───
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<HudayiPortal.Infrastructure.Persistence.ApplicationDbContext>();
	if (!db.YoklamaTurleri.Any())
	{
		db.YoklamaTurleri.AddRange(
			new HudayiPortal.Domain.Entities.YoklamaTuru { TurAdi = "Gece Yoklaması", OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false },
			new HudayiPortal.Domain.Entities.YoklamaTuru { TurAdi = "Sabah Namazı", OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false }
		);
		await db.SaveChangesAsync();
	}

	// ─── Seed Data: SohbetGruplari ───
	if (!db.SohbetGruplari.Any())
	{
		db.SohbetGruplari.AddRange(
			new HudayiPortal.Domain.Entities.SohbetGrubu { GrupAdi = "Genel Sohbet Grubu", OlusturulmaTarihi = DateTime.UtcNow, SilindiMi = false }
		);
		await db.SaveChangesAsync();
	}

	// ─── Seed Data: OgrenciSohbetGruplari ───
	if (!db.OgrenciSohbetGruplari.Any())
	{
		var gruplar = db.SohbetGruplari.Where(g => g.SilindiMi != true).OrderBy(g => g.Id).ToList();
		var ogrenciler = db.Kullanicilar.Where(k => k.RolId == 1 && k.AktifMi == true && k.SilindiMi != true).OrderBy(k => k.Id).ToList();
		if (gruplar.Count > 0 && ogrenciler.Count > 0)
		{
			for (int i = 0; i < ogrenciler.Count; i++)
			{
				db.OgrenciSohbetGruplari.Add(new HudayiPortal.Domain.Entities.OgrenciSohbetGrubu
				{
					KullaniciId = ogrenciler[i].Id,
					SohbetGrupId = gruplar[i % gruplar.Count].Id,
					AtanmaTarihi = DateTime.UtcNow,
					SilindiMi = false
				});
			}
			await db.SaveChangesAsync();
		}
	}
}

app.Run();