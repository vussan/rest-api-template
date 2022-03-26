using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Rest_API_Template.Extensions;
using Rest_API_Template.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using Utilities;

namespace Rest_API_Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogConfig();
            try
            {
                Builder(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "API failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void Builder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TemplateDBContext>(x => x.UseSqlServer(Config.ConnectionString));
            builder.Services.AddDependency();
            builder.Services.AddJWTAuth();
            builder.Services.AddSwaggerExt();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseJwt();
            app.UseSwaggerExt();
            app.UseHttpsRedirection();
            app.UseMiddleware<SerilogMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapControllers();
            app.Run();
        }

        private static void SerilogConfig()
        {
            var columnOptions = new ColumnOptions()
            {
                AdditionalColumns = new Collection<SqlColumn>()
                    {
                        new SqlColumn("Endpoint",System.Data.SqlDbType.VarChar,dataLength:100),
                        new SqlColumn("Method",System.Data.SqlDbType.VarChar,dataLength:5),
                        new SqlColumn("QueryString",System.Data.SqlDbType.VarChar,dataLength:100),
                        new SqlColumn("RequestBody",System.Data.SqlDbType.VarChar),
                        new SqlColumn("UserId",System.Data.SqlDbType.UniqueIdentifier)
                    }
            };
            columnOptions.Store.Remove(StandardColumn.Message);
            columnOptions.Store.Remove(StandardColumn.Properties);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: Config.ConnectionString,
                    sinkOptions: new MSSqlServerSinkOptions()
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true,
                    },
                    columnOptions: columnOptions
                    )
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .CreateLogger();
        }

    }
}

