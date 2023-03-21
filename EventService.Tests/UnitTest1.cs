using EventService.Features.Event.Commands.Create;
using EventService.Features.Ticket.Commands.SetFreeTickets;

using EventService.Features.Ticket.Commands.IssueATicket;

using EventService.Infrastructure.InterfaceImplements;

using EventService.Models.Interfaces;

using MongoDB.Driver;

using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Tests;

public class Tests
{
#pragma warning disable CS8618
    private IBaseEventService _eventService;
#pragma warning restore CS8618
     
    [SetUp]
    public void Setup()
    {
        _eventService = new EventMongoDbService(new MongoClient("mongodb://127.0.0.1:27017"));
           
    }

    [Test]
    public void WrongDateTime()
    {
        var start=DateTime.UtcNow.AddDays(1);
        var end= DateTime.UtcNow;
        var command = new CreateEventCommand
        {

            Start = start, End = end, Description = "TestDescription", Title = "TestTitle",
            IdImage = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d"),
            IdSpace = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d")
        };

        Assert.Throws(typeof(ScException), () => { var unused = new CreateEventCommandRequestHandler(_eventService).Handle(command, new CancellationToken()).Result; });
    }
    [Test]
    public void RightCreate()
    {
        var start = DateTime.UtcNow;
        var end = DateTime.UtcNow.AddDays(1);
        var command = new CreateEventCommand
        {

            Start = start,
            End = end,
            Description = "TestDescription",
            Title = "TestTitle",
            IdImage = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d"),
            IdSpace = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d")
        };
        var result = new CreateEventCommandRequestHandler(_eventService).Handle(command, new CancellationToken()).Result;

        Assert.AreEqual(result.GetType(), typeof(ScResult<string>));
    }
    [Test]
    public void TicketForNotExistUser()
    {
        var eventDefault=_eventService.GetAllEvents().FirstOrDefault();
        var idNonUser = Guid.Empty;
        if (eventDefault == null) return;
        var command = new IssueATicketCommand
        {
            IdEvent = eventDefault.Id,
            IdOwner = idNonUser
        };

        Assert.Throws(typeof(ScException), () => { var unused = new IssueATicketCommandRequestHandler(_eventService).Handle(command,new CancellationToken()).Result; });
    }

    [Test]
    public void TicketsForNotExistEvent()
    {

        var idNonEvents = Guid.Empty;
        var command = new SetFreeTicketsCommand
        {
            IdEvent = idNonEvents,
            Count = 10,
            IsAutoGeneratePlaces = true
        };

        Assert.Throws(typeof(ScException), () => { var unused = new SetFreeTicketsCommandRequestHandler(_eventService).Handle(command, new CancellationToken()).Result; });
    }

    [Test]
    public void TicketsForExistEvent()
    {
       
        var eventDefault = _eventService.GetAllEvents().FirstOrDefault();
        if (eventDefault == null) return;
        var command = new SetFreeTicketsCommand
        {
            IdEvent = eventDefault.Id,
            Count = 10,
            IsAutoGeneratePlaces = true
        };
        var result = new SetFreeTicketsCommandRequestHandler(_eventService).Handle(command, new CancellationToken()).Result;

        Assert.AreEqual(result.GetType(), typeof(ScResult<string>));
    }

}