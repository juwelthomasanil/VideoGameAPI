using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VideoGameAPI.Data;
using VideoGameAPI.Services;
using Xunit;

namespace VideoGameAPI.Tests
{
    internal class VideoGameServiceTests
    {
        private VideoGameDbContext CreateFreshDatabase(string dbNmae)
        {
            // Create a new in-memory database for testing
            var options = new DbContextOptionsBuilder<VideoGameDbContext>()
                .UseInMemoryDatabase(databaseName: dbNmae) // Unique name for isolation
                .Options;
            return new VideoGameDbContext(options);
        }
    }
}
