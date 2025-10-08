using GovConnect.Application.Requests.Queries;
using GovConnect.Shared.Models;
using GovConnect.Shared.Pagination;

namespace GovConnect.Application.IntegrationTests.Requests.Queries {
    public class GetPaginatedRequestsQueryTests : BaseIntegTest {
        public GetPaginatedRequestsQueryTests() : base() {
            DbContext.Requests.AddRange([
                new() {
                Id = 1,
                Title = "Title 1",
                Description = "Description 1",
                SubcategoryId = 1,
                BarangayId = 1,
                RequestedByUserId = 1,
                PriorityLevelId = 3,
                CreatedAt = new DateTime(2025, 01, 01)
            },
            new() {
                Id = 2,
                Title = "Title 2",
                Description = "Description 2",
                SubcategoryId = 1,
                BarangayId = 1,
                RequestedByUserId = 1,
                PriorityLevelId = 3,
                CreatedAt = new DateTime(2025, 01, 02)
            },
            new() {
                Id = 3,
                Title = "Title 3",
                Description = "Description 3",
                SubcategoryId = 1,
                BarangayId = 1,
                RequestedByUserId = 1,
                PriorityLevelId = 3,
                CreatedAt = new DateTime(2025, 01, 03)
            }
            ]);

            DbContext.SaveChanges();
        }

        [Fact]
        public async void GetPaginatedRequestsQuery_Runs_Successfully() {
            // Arrange
            var requestHandler = new GetPaginatedRequestsQueryHandler(DbContext);
            var query = new GetPaginatedRequestsQuery {
                TakeSize = 3,
                SortDirection = SortDirection.Descending
            };

            // Act
            var result = await requestHandler.HandleAsync(query);

            // Assert
            Assert.True(result.Data is not null);
            Assert.True(result.ResultsCount == 3);
            Assert.True(result.NextCursor is not null);
            Assert.Equal(3, result.Data.FirstOrDefault()?.Id);

            var decodedNextCursor = CursorEncoder.Decode(result.NextCursor);
            _ = DateTime.TryParse(decodedNextCursor, out var nextCursor);

            Assert.Equal(new DateTime(2025, 01, 03), nextCursor);
        }

        [Fact]
        public async void GetPaginatedRequestsQuery_With_SearchValue_Runs_Successfully() {
            // Arrange
            var requestHandler = new GetPaginatedRequestsQueryHandler(DbContext);
            var query = new GetPaginatedRequestsQuery {
                TakeSize = 1,
                SearchValue = "Title 2"
            };

            // Act
            var result = await requestHandler.HandleAsync(query);

            // Assert
            Assert.True(result.Data is not null);
            Assert.True(result.ResultsCount == 1);
            Assert.True(result.NextCursor is not null);
            Assert.Equal(2, result.Data.FirstOrDefault()?.Id);

            var decodedNextCursor = CursorEncoder.Decode(result.NextCursor);
            _ = DateTime.TryParse(decodedNextCursor, out var nextCursor);

            Assert.Equal(new DateTime(2025, 01, 02), nextCursor);
        }

        [Fact]
        public async void GetPaginatedRequestsQuery_With_SortBy_Runs_Successfully() {
            // Arrange
            var requestHandler = new GetPaginatedRequestsQueryHandler(DbContext);
            var query = new GetPaginatedRequestsQuery {
                TakeSize = 1,
                SortBy = nameof(GetPaginatedRequestsQueryRow.Description),
                SortDirection = SortDirection.Descending
            };

            // Act
            var result = await requestHandler.HandleAsync(query);

            // Assert
            Assert.True(result.Data is not null);
            Assert.True(result.ResultsCount == 1);
            Assert.True(result.NextCursor is not null);
            Assert.Equal(3, result.Data.FirstOrDefault()?.Id);

            var decodedNextCursor = CursorEncoder.Decode(result.NextCursor);
            _ = DateTime.TryParse(decodedNextCursor, out var nextCursor);

            Assert.Equal(new DateTime(2025, 01, 03), nextCursor);
        }
    }
}
