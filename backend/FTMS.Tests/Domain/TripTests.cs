using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;

namespace FTMS.Tests.Domain;

public class TripTests
{
    private Trip CreateTrip()
    {
        return Trip.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());
    }


    [Fact]
    public void Create_ShouldSetStatusToReady()
    {
        var trip = CreateTrip();

        trip.Status.Should()
            .Be(TripStatusEnum.Ready);
    }


    [Fact]
    public void Start_ShouldChangeStatusToEnRoute()
    {
        var trip = CreateTrip();

        var startTime = DateTimeOffset.UtcNow;

        trip.Start(startTime);

        trip.Status.Should()
            .Be(TripStatusEnum.EnRoute);

        trip.StartedAt.Should()
            .Be(startTime);
    }


    [Fact]
    public void Finish_ShouldChangeStatusToCompleted()
    {
        var trip = CreateTrip();

        var startTime = DateTimeOffset.UtcNow;
        var finishTime = startTime.AddHours(1);

        trip.Start(startTime);
        trip.Finish(finishTime);


        trip.Status.Should()
            .Be(TripStatusEnum.Completed);

        trip.CompletedAt.Should()
            .Be(finishTime);
    }


    [Fact]
    public void Cancel_ShouldChangeStatusToCancelled()
    {
        var trip = CreateTrip();

        trip.Cancel("Vehicle problem");


        trip.Status.Should()
            .Be(TripStatusEnum.Cancelled);

        trip.CancellationReason.Should()
            .Be("Vehicle problem");
    }


    [Fact]
    public void Finish_BeforeStart_ShouldThrowException()
    {
        var trip = CreateTrip();

        var act = () =>
            trip.Finish(DateTimeOffset.UtcNow);


        act.Should()
            .Throw<Exception>();
    }
}