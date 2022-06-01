//Internal

global using RavenShop.Application.Models.Persistence;
global using RavenShop.Application.Models.Product;
global using RavenShop.Application.Data.Repositories;
global using RavenShop.Application.Services.v1;
global using RavenShop.Application.Data.Context;

//External

global using Microsoft.OpenApi.Models;
global using Microsoft.Extensions.Options;
global using System.ComponentModel.DataAnnotations;
global using Newtonsoft.Json;
global using AutoMapper;

global using Raven.Client.Documents;
global using Raven.Client.Documents.Operations;
global using Raven.Client.Exceptions.Database;
global using Raven.Client.ServerWide;
global using Raven.Client.ServerWide.Operations;