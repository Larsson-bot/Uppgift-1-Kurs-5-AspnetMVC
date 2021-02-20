using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uppgift_1_Kurs_5_AspnetMVC.Data;

[assembly: HostingStartup(typeof(Uppgift_1_Kurs_5_AspnetMVC.Areas.Identity.IdentityHostingStartup))]
namespace Uppgift_1_Kurs_5_AspnetMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}