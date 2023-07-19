using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.EventDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IEventService
    {
        void AddEvent(Event _event);
        List<ShowEvents> GetAllEvents();
        List<ShowEvents> GetAllEvents(int pagesize);
        List<BOShowEvents> BOGetAllEvents();
        List<BOShowEvents> BOGetAllEvents(int pagesize);
        void UpdateEvent(Event _event);
        ShowEvents GetEventById(int Id);
        Event GetEvent(int Id);
        BOShowEventsImages ShowEventImages(int Id);
    }
}
