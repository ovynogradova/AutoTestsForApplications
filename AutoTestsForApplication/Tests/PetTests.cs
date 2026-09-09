using apitest.Interfaces.PetStore;
using AutoTestsForApplication.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class PetTests
{
    public IPetIP PetAPI;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };
        services.AddRefitClient<IPetIP>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://petstoreapi.com/v1");
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        var provider = services.BuildServiceProvider(); 
        PetAPI = provider.GetRequiredService<IPetIP>();
    }
    
    [Test]
    public async Task GetAllPetsAsync()
    {
        var pets = await PetAPI.GetAllPetAsync();
        pets.Data.Should().HaveCount(20);
    }

    [Test]
    public async Task GetPetByIdAsync()
    {
        var pets = await PetAPI.GetAllPetAsync();
        pets.Data.Should().HaveCount(20);
        
        var rndId = RandomHelper.GetRandomItem(pets.Data).Id;
        var pet =  await PetAPI.GetPetByIdAsync(rndId);
        var pet2 = pets.Data.Where(p => p.Id == rndId).First();
        
        pet.AgeMonths.Should().Be(pet2.AgeMonths);
        pet.Should().NotBeNull();
        }

    [Test]
    public async Task GetAllPetsByStatusAndLimitAsync()
    {
        var pets = await PetAPI.GetAllPetByStatusAndLimitAsync(12,"ADOPTED");
        pets.Data.Should().HaveCount(12);
    }
}